using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace TopClient_Storage
{
    public partial class Manager : Form
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        private int _currentUserId;

        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private bool _isClosing;

        private int? _selectedSaleHeadId;
        private int? _selectedSaleSpecId;

        public Manager()
        {
            InitializeComponent();
        }

        public Manager(TcpClient client, StreamReader reader, StreamWriter writer, int currentUserId)
            : this()
        {
            _client = client;
            _reader = reader;
            _writer = writer;
            _currentUserId = currentUserId;
        }

        private bool IsDesignerMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;
        }

        private async void Manager_Load(object sender, EventArgs e)
        {
            if (IsDesignerMode())
                return;

            await SafeRunAsync(InitialLoadAsync);
        }

        private async Task InitialLoadAsync()
        {
            await LoadReferenceDataAsync();
            await LoadStockAsync();
            await LoadOrderHeadsAsync();
        }

        private async Task LoadReferenceDataAsync()
        {
            DataTable stores = await GetTableAsync("STORES_GET_ALL", "OK:STORES|");
            DataTable products = await GetTableAsync("PRODUCTS_SHORT_GET_ALL", "OK:PRODUCTS_SHORT|");
            DataTable clients = await GetTableAsync("CLIENTS_GET_ALL", "OK:CLIENTS|");

            BindCombo(_cmbOrderStore, stores.Copy(), "storeid", "storelongname");
            BindCombo(_cmbStockStoreFilter, CreateFilterTable(stores, "storeid", "storelongname", "Все склады"), "storeid", "storelongname");

            BindCombo(_cmbOrderClient, clients.Copy(), "clientid", "clientname");

            BindCombo(_cmbOrderProduct, products.Copy(), "productid", "productcode");
            BindCombo(_cmbStockProductFilter, CreateFilterTable(products, "productid", "productcode", "Все товары"), "productid", "productcode");
        }

        private async Task LoadStockAsync()
        {
            string storeId = GetComboValueOrEmpty(_cmbStockStoreFilter);
            string productId = GetComboValueOrEmpty(_cmbStockProductFilter);

            DataTable table = await GetTableAsync($"MANAGER_STOCK_GET|{storeId}|{productId}", "OK:MANAGER_STOCK|");
            BindGrid(_dgvStock, table);
        }

        private async Task LoadOrderHeadsAsync()
        {
            int? selectedId = _selectedSaleHeadId;
            DataTable table = await GetTableAsync($"MANAGER_SALEHEADS_GET|{_currentUserId}", "OK:MANAGER_SALEHEADS|");
            BindGrid(_dgvOrderHeads, table);
            RestoreGridSelection(_dgvOrderHeads, "saleheadid", selectedId);
            await OnOrderHeadSelectionChangedAsync();
        }

        private async Task OnOrderHeadSelectionChangedAsync()
        {
            _selectedSaleHeadId = GetSelectedGridId(_dgvOrderHeads, "saleheadid");
            _selectedSaleSpecId = null;

            if (_selectedSaleHeadId == null)
            {
                BindGrid(_dgvOrderSpecs, new DataTable());
                return;
            }

            DataTable table = await GetTableAsync($"MANAGER_SALESPECS_GET|{_selectedSaleHeadId.Value}", "OK:MANAGER_SALESPECS|");
            BindGrid(_dgvOrderSpecs, table);
        }

        private async Task CreateOrderHeadAsync()
        {
            int storeId = RequireComboInt(_cmbOrderStore, "Выберите склад заказа.");
            int clientId = RequireComboInt(_cmbOrderClient, "Выберите клиента.");

            string response = await SendCommandCheckedAsync($"MANAGER_SALEHEAD_ADD|{storeId}|{_currentUserId}|{clientId}");
            int createdId = ParseIdResponse(response, "OK:MANAGER_SALEHEAD_ADD|");

            await LoadOrderHeadsAsync();
            RestoreGridSelection(_dgvOrderHeads, "saleheadid", createdId);
        }

        private async Task AddOrderSpecAsync()
        {
            if (_selectedSaleHeadId == null)
                throw new Exception("Сначала выберите заказ клиента.");

            int productId = RequireComboInt(_cmbOrderProduct, "Выберите товар.");
            decimal quant = _nudOrderQuant.Value;

            await SendCommandCheckedAsync(
                $"MANAGER_SALESPEC_ADD|{_selectedSaleHeadId.Value}|{productId}|{ToProtocolDecimal(quant)}");

            await OnOrderHeadSelectionChangedAsync();
            await LoadStockAsync();
        }

        private async Task DeleteOrderSpecAsync()
        {
            if (_selectedSaleSpecId == null)
                throw new Exception("Сначала выберите строку заказа.");

            await SendCommandCheckedAsync($"MANAGER_SALESPEC_DELETE|{_selectedSaleSpecId.Value}");
            await OnOrderHeadSelectionChangedAsync();
            await LoadStockAsync();
        }

        private async Task CloseOrderHeadAsync()
        {
            if (_selectedSaleHeadId == null)
                throw new Exception("Сначала выберите заказ клиента.");

            await SendCommandCheckedAsync($"MANAGER_SALEHEAD_CLOSE|{_selectedSaleHeadId.Value}");
            await LoadOrderHeadsAsync();
            await LoadStockAsync();
        }

        private async Task<DataTable> GetTableAsync(string command, string okPrefix)
        {
            string response = await SendCommandCheckedAsync(command);
            string json = ExtractPayload(response, okPrefix);
            return JsonToTable(json);
        }

        private async Task<string> SendCommandCheckedAsync(string command)
        {
            string? response = await SendCommandAsync(command);

            if (string.IsNullOrWhiteSpace(response))
                throw new Exception("Сервер не вернул ответ.");

            if (response.StartsWith("ERR:", StringComparison.Ordinal))
            {
                string[] parts = response.Split('|', 2);
                if (parts.Length > 1)
                    throw new Exception(parts[1]);

                throw new Exception(response.Substring(4));
            }

            return response;
        }

        private async Task<string?> SendCommandAsync(string command)
        {
            await _sendLock.WaitAsync();

            try
            {
                if (_client == null || _reader == null || _writer == null)
                    throw new Exception("Соединение с сервером не инициализировано.");

                if (!_client.Connected)
                    throw new Exception("Соединение с сервером разорвано.");

                await _writer.WriteLineAsync(command);
                return await _reader.ReadLineAsync();
            }
            finally
            {
                _sendLock.Release();
            }
        }

        private DataTable JsonToTable(string json)
        {
            DataTable table = new DataTable();
            List<Dictionary<string, string>>? rows = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

            if (rows == null || rows.Count == 0)
                return table;

            foreach (string col in rows[0].Keys)
                table.Columns.Add(col);

            foreach (Dictionary<string, string> item in rows)
            {
                DataRow row = table.NewRow();

                foreach (KeyValuePair<string, string> pair in item)
                    row[pair.Key] = pair.Value ?? "";

                table.Rows.Add(row);
            }

            return table;
        }

        private string ExtractPayload(string response, string okPrefix)
        {
            if (!response.StartsWith(okPrefix, StringComparison.Ordinal))
                throw new Exception("Сервер вернул неожиданный ответ: " + response);

            return response.Substring(okPrefix.Length);
        }

        private int ParseIdResponse(string response, string okPrefix)
        {
            string value = ExtractPayload(response, okPrefix);

            if (!int.TryParse(value, out int id))
                throw new Exception("Сервер вернул неверный идентификатор.");

            return id;
        }

        private void BindGrid(DataGridView grid, DataTable table)
        {
            grid.DataSource = table;
            grid.ClearSelection();
        }

        private void BindCombo(ComboBox combo, DataTable table, string valueMember, string displayMember)
        {
            combo.DataSource = table;
            combo.ValueMember = valueMember;
            combo.DisplayMember = displayMember;

            if (table.Rows.Count > 0)
                combo.SelectedIndex = 0;
        }

        private DataTable CreateFilterTable(DataTable source, string valueColumn, string textColumn, string firstText)
        {
            DataTable table = source.Copy();
            DataRow row = table.NewRow();
            row[valueColumn] = "";
            row[textColumn] = firstText;
            table.Rows.InsertAt(row, 0);
            return table;
        }

        private string GetComboValueOrEmpty(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return "";

            return combo.SelectedValue.ToString() ?? "";
        }

        private int RequireComboInt(ComboBox combo, string errorMessage)
        {
            if (combo.SelectedValue == null || !int.TryParse(combo.SelectedValue.ToString(), out int result))
                throw new Exception(errorMessage);

            return result;
        }

        private int? GetSelectedGridId(DataGridView grid, string columnName)
        {
            if (grid.CurrentRow == null)
                return null;

            if (!grid.Columns.Contains(columnName))
                return null;

            object? value = grid.CurrentRow.Cells[columnName].Value;
            if (value == null)
                return null;

            if (int.TryParse(value.ToString(), out int result))
                return result;

            return null;
        }

        private void RestoreGridSelection(DataGridView grid, string idColumnName, int? id)
        {
            if (id == null || !grid.Columns.Contains(idColumnName))
            {
                if (grid.Rows.Count > 0)
                {
                    grid.Rows[0].Selected = true;
                    if (grid.Rows[0].Cells.Count > 0)
                        grid.CurrentCell = grid.Rows[0].Cells[0];
                }

                return;
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                object? value = row.Cells[idColumnName].Value;
                if (value != null && value.ToString() == id.Value.ToString())
                {
                    row.Selected = true;
                    if (row.Cells.Count > 0)
                        grid.CurrentCell = row.Cells[0];
                    return;
                }
            }

            if (grid.Rows.Count > 0)
            {
                grid.Rows[0].Selected = true;
                if (grid.Rows[0].Cells.Count > 0)
                    grid.CurrentCell = grid.Rows[0].Cells[0];
            }
        }

        private string ToProtocolDecimal(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private async Task SafeRunAsync(Func<Task> action, bool showError = true)
        {
            if (_isClosing)
                return;

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (showError && !_isClosing)
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnStockRefresh_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(LoadStockAsync);
        }

        private async void btnOrderHeadRefresh_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(LoadOrderHeadsAsync);
        }

        private async void btnOrderHeadCreate_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(CreateOrderHeadAsync);
        }

        private async void btnOrderHeadClose_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(CloseOrderHeadAsync);
        }

        private async void btnOrderSpecAdd_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(AddOrderSpecAsync);
        }

        private async void btnOrderSpecDelete_Click(object sender, EventArgs e)
        {
            await SafeRunAsync(DeleteOrderSpecAsync);
        }

        private async void dgvOrderHeads_SelectionChanged(object sender, EventArgs e)
        {
            await SafeRunAsync(OnOrderHeadSelectionChangedAsync, false);
        }

        private void dgvOrderSpecs_SelectionChanged(object sender, EventArgs e)
        {
            _selectedSaleSpecId = GetSelectedGridId(_dgvOrderSpecs, "salespecid");
        }

        private async void Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDesignerMode())
                return;

            _isClosing = true;

            try
            {
                if (_client != null && _reader != null && _writer != null && _client.Connected)
                {
                    await _writer.WriteLineAsync("QUIT");
                    await _reader.ReadLineAsync();
                }
            }
            catch
            {
            }
            finally
            {
                try { _reader?.Dispose(); } catch { }
                try { _writer?.Dispose(); } catch { }
                try { _client?.Close(); } catch { }
            }
        }
    }
}
