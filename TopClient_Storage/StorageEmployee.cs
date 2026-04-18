using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace TopClient_Storage
{
    public partial class StorageEmployee : Form
    {
        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        private int _currentUserId;

        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        private bool _isClosing;

        private int? _selectedOrderHeadId;
        private int? _selectedOrderSpecId;
        private int? _selectedSaleHeadId;
        private int? _selectedSaleSpecId;
        private int? _selectedTransferHeadId;
        private int? _selectedTransferSpecId;

        // Конструктор нужен для визуального конструктора формы.
        public StorageEmployee()
        {
            InitializeComponent();
        }

        // Основной конструктор формы.
        public StorageEmployee(TcpClient client, StreamReader reader, StreamWriter writer, int currentUserId)
            : this()
        {
            _client = client;
            _reader = reader;
            _writer = writer;
            _currentUserId = currentUserId;
        }

        // Проверка режима конструктора.
        private bool IsDesignerMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;
        }

        // Загрузка формы.
        private async void StorageEmployee_Load(object? sender, EventArgs e)
        {
            if (IsDesignerMode())
                return;

            await SafeRunAsync(InitialLoadAsync);
        }

        // Нажатие кнопки обновления остатков.
        private async void btnStockRefresh_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(LoadStockAsync);
        }

        // Нажатие кнопки обновления приемок.
        private async void btnOrderHeadRefresh_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(LoadOrderHeadsAsync);
        }

        // Нажатие кнопки создания приемки.
        private async void btnOrderHeadCreate_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(CreateOrderHeadAsync);
        }

        // Нажатие кнопки подтверждения приемки.
        private async void btnOrderHeadAccept_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(AcceptOrderHeadAsync);
        }

        // Нажатие кнопки добавления строки приемки.
        private async void btnOrderSpecAdd_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(AddOrderSpecAsync);
        }

        // Нажатие кнопки удаления строки приемки.
        private async void btnOrderSpecDelete_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(DeleteOrderSpecAsync);
        }

        // Выбор документа приемки.
        private async void dgvOrderHeads_SelectionChanged(object? sender, EventArgs e)
        {
            await SafeRunAsync(OnOrderHeadSelectionChangedAsync, false);
        }

        // Выбор строки приемки.
        private void dgvOrderSpecs_SelectionChanged(object? sender, EventArgs e)
        {
            _selectedOrderSpecId = GetSelectedGridId(_dgvOrderSpecs, "orderspecid");
        }

        // Нажатие кнопки обновления накладных.
        private async void btnSaleHeadRefresh_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(LoadSaleHeadsAsync);
        }

        // Нажатие кнопки создания накладной.
        private async void btnSaleHeadCreate_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(CreateSaleHeadAsync);
        }

        // Нажатие кнопки проведения выдачи.
        private async void btnSaleHeadProcess_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(ProcessSaleHeadAsync);
        }

        // Нажатие кнопки добавления строки накладной.
        private async void btnSaleSpecAdd_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(AddSaleSpecAsync);
        }

        // Нажатие кнопки удаления строки накладной.
        private async void btnSaleSpecDelete_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(DeleteSaleSpecAsync);
        }

        // Выбор накладной.
        private async void dgvSaleHeads_SelectionChanged(object? sender, EventArgs e)
        {
            await SafeRunAsync(OnSaleHeadSelectionChangedAsync, false);
        }

        // Выбор строки накладной.
        private void dgvSaleSpecs_SelectionChanged(object? sender, EventArgs e)
        {
            _selectedSaleSpecId = GetSelectedGridId(_dgvSaleSpecs, "salespecid");
        }

        // Нажатие кнопки обновления перемещений.
        private async void btnTransferHeadRefresh_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(LoadTransferHeadsAsync);
        }

        // Нажатие кнопки создания перемещения.
        private async void btnTransferHeadCreate_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(CreateTransferHeadAsync);
        }

        // Нажатие кнопки отправки перемещения.
        private async void btnTransferHeadSend_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(SendTransferHeadAsync);
        }

        // Нажатие кнопки приема перемещения.
        private async void btnTransferHeadAccept_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(AcceptTransferHeadAsync);
        }

        // Нажатие кнопки добавления строки перемещения.
        private async void btnTransferSpecAdd_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(AddTransferSpecAsync);
        }

        // Нажатие кнопки удаления строки перемещения.
        private async void btnTransferSpecDelete_Click(object? sender, EventArgs e)
        {
            await SafeRunAsync(DeleteTransferSpecAsync);
        }

        // Выбор документа перемещения.
        private async void dgvTransferHeads_SelectionChanged(object? sender, EventArgs e)
        {
            await SafeRunAsync(OnTransferHeadSelectionChangedAsync, false);
        }

        // Выбор строки перемещения.
        private void dgvTransferSpecs_SelectionChanged(object? sender, EventArgs e)
        {
            _selectedTransferSpecId = GetSelectedGridId(_dgvTransferSpecs, "transferspecid");
        }

        // Начальная загрузка справочников и таблиц.
        private async Task InitialLoadAsync()
        {
            await LoadReferenceDataAsync();
            await LoadStockAsync();
            await LoadOrderHeadsAsync();
            await LoadSaleHeadsAsync();
            await LoadTransferHeadsAsync();
        }

        // Загрузка справочников.
        private async Task LoadReferenceDataAsync()
        {
            DataTable stores = await GetTableAsync("STORES_GET_ALL", "OK:STORES|");
            DataTable suppliers = await GetTableAsync("SUPPLIERS_GET_ALL", "OK:SUPPLIERS|");
            DataTable products = await GetTableAsync("PRODUCTS_SHORT_GET_ALL", "OK:PRODUCTS_SHORT|");
            DataTable clients = await GetTableAsync("CLIENTS_GET_ALL", "OK:CLIENTS|");

            BindCombo(_cmbOrderStore, stores.Copy(), "storeid", "storelongname");
            BindCombo(_cmbSaleStore, stores.Copy(), "storeid", "storelongname");
            BindCombo(_cmbTransferStoreOut, stores.Copy(), "storeid", "storelongname");
            BindCombo(_cmbTransferStoreIn, stores.Copy(), "storeid", "storelongname");

            BindCombo(
                _cmbStockStoreFilter,
                CreateFilterTable(stores, "storeid", "storelongname", "Все склады"),
                "storeid",
                "storelongname");

            BindCombo(_cmbOrderSupplier, suppliers.Copy(), "supplierid", "supplierlongname");

            BindCombo(_cmbOrderProduct, products.Copy(), "productid", "productcode");
            BindCombo(_cmbSaleProduct, products.Copy(), "productid", "productcode");
            BindCombo(_cmbTransferProduct, products.Copy(), "productid", "productcode");

            BindCombo(
                _cmbStockProductFilter,
                CreateFilterTable(products, "productid", "productcode", "Все товары"),
                "productid",
                "productcode");

            BindCombo(_cmbSaleClient, clients.Copy(), "clientid", "clientname");
        }

        // Загрузка остатков.
        private async Task LoadStockAsync()
        {
            string storeId = GetComboValueOrEmpty(_cmbStockStoreFilter);
            string productId = GetComboValueOrEmpty(_cmbStockProductFilter);

            DataTable table = await GetTableAsync($"STOCK_GET_ALL|{storeId}|{productId}", "OK:STOCK|");
            BindGrid(_dgvStock, table);
        }

        // Загрузка документов приемки.
        private async Task LoadOrderHeadsAsync()
        {
            int? selectedId = _selectedOrderHeadId;
            DataTable table = await GetTableAsync("ORDERHEADS_GET_ALL", "OK:ORDERHEADS|");
            BindGrid(_dgvOrderHeads, table);
            RestoreGridSelection(_dgvOrderHeads, "orderheadid", selectedId);
            await OnOrderHeadSelectionChangedAsync();
        }

        // Загрузка строк выбранной приемки.
        private async Task OnOrderHeadSelectionChangedAsync()
        {
            _selectedOrderHeadId = GetSelectedGridId(_dgvOrderHeads, "orderheadid");
            _selectedOrderSpecId = null;

            if (_selectedOrderHeadId == null)
            {
                BindGrid(_dgvOrderSpecs, new DataTable());
                return;
            }

            DataTable table = await GetTableAsync(
                $"ORDERSPECS_GET_BY_HEAD|{_selectedOrderHeadId.Value}",
                "OK:ORDERSPECS|");

            BindGrid(_dgvOrderSpecs, table);
        }

        // Создание документа приемки.
        private async Task CreateOrderHeadAsync()
        {
            int storeId = RequireComboInt(_cmbOrderStore, "Выберите склад для приемки.");
            int supplierId = RequireComboInt(_cmbOrderSupplier, "Выберите поставщика.");

            string response = await SendCommandCheckedAsync($"ORDERHEAD_ADD|{storeId}|{supplierId}");
            int createdId = ParseIdResponse(response, "OK:ORDERHEAD_ADD|");

            await LoadOrderHeadsAsync();
            RestoreGridSelection(_dgvOrderHeads, "orderheadid", createdId);
        }

        // Добавление строки в приемку.
        private async Task AddOrderSpecAsync()
        {
            if (_selectedOrderHeadId == null)
                throw new Exception("Сначала выберите документ приемки.");

            int productId = RequireComboInt(_cmbOrderProduct, "Выберите товар.");
            decimal quant = _nudOrderQuant.Value;
            decimal price = _nudOrderPrice.Value;

            await SendCommandCheckedAsync(
                $"ORDERSPEC_ADD|{_selectedOrderHeadId.Value}|{productId}|{ToProtocolDecimal(quant)}|{ToProtocolDecimal(price)}");

            await OnOrderHeadSelectionChangedAsync();
        }

        // Удаление строки приемки.
        private async Task DeleteOrderSpecAsync()
        {
            if (_selectedOrderSpecId == null)
                throw new Exception("Сначала выберите строку приемки.");

            await SendCommandCheckedAsync($"ORDERSPEC_DELETE|{_selectedOrderSpecId.Value}");
            await OnOrderHeadSelectionChangedAsync();
        }

        // Подтверждение приемки.
        private async Task AcceptOrderHeadAsync()
        {
            if (_selectedOrderHeadId == null)
                throw new Exception("Сначала выберите документ приемки.");

            await SendCommandCheckedAsync($"ORDERHEAD_ACCEPT|{_selectedOrderHeadId.Value}");
            await LoadOrderHeadsAsync();
            await LoadStockAsync();
        }

        // Загрузка накладных.
        private async Task LoadSaleHeadsAsync()
        {
            int? selectedId = _selectedSaleHeadId;
            DataTable table = await GetTableAsync("SALEHEADS_GET_ALL", "OK:SALEHEADS|");
            BindGrid(_dgvSaleHeads, table);
            RestoreGridSelection(_dgvSaleHeads, "saleheadid", selectedId);
            await OnSaleHeadSelectionChangedAsync();
        }

        // Загрузка строк выбранной накладной.
        private async Task OnSaleHeadSelectionChangedAsync()
        {
            _selectedSaleHeadId = GetSelectedGridId(_dgvSaleHeads, "saleheadid");
            _selectedSaleSpecId = null;

            if (_selectedSaleHeadId == null)
            {
                BindGrid(_dgvSaleSpecs, new DataTable());
                return;
            }

            DataTable table = await GetTableAsync(
                $"SALESPECS_GET_BY_HEAD|{_selectedSaleHeadId.Value}",
                "OK:SALESPECS|");

            BindGrid(_dgvSaleSpecs, table);
        }

        // Создание накладной.
        private async Task CreateSaleHeadAsync()
        {
            int storeId = RequireComboInt(_cmbSaleStore, "Выберите склад выдачи.");
            int clientId = RequireComboInt(_cmbSaleClient, "Выберите клиента.");

            string response = await SendCommandCheckedAsync($"SALEHEAD_ADD|{storeId}|{_currentUserId}|{clientId}");
            int createdId = ParseIdResponse(response, "OK:SALEHEAD_ADD|");

            await LoadSaleHeadsAsync();
            RestoreGridSelection(_dgvSaleHeads, "saleheadid", createdId);
        }

        // Добавление строки в накладную.
        private async Task AddSaleSpecAsync()
        {
            if (_selectedSaleHeadId == null)
                throw new Exception("Сначала выберите накладную.");

            int productId = RequireComboInt(_cmbSaleProduct, "Выберите товар.");
            decimal quant = _nudSaleQuant.Value;

            await SendCommandCheckedAsync(
                $"SALESPEC_ADD|{_selectedSaleHeadId.Value}|{productId}|{ToProtocolDecimal(quant)}");

            await OnSaleHeadSelectionChangedAsync();
            await LoadStockAsync();
        }

        // Удаление строки накладной.
        private async Task DeleteSaleSpecAsync()
        {
            if (_selectedSaleSpecId == null)
                throw new Exception("Сначала выберите строку накладной.");

            await SendCommandCheckedAsync($"SALESPEC_DELETE|{_selectedSaleSpecId.Value}");
            await OnSaleHeadSelectionChangedAsync();
            await LoadStockAsync();
        }

        // Проведение выдачи.
        private async Task ProcessSaleHeadAsync()
        {
            if (_selectedSaleHeadId == null)
                throw new Exception("Сначала выберите накладную.");

            await SendCommandCheckedAsync($"SALEHEAD_PROCESS|{_selectedSaleHeadId.Value}");
            await LoadSaleHeadsAsync();
            await LoadStockAsync();
        }

        // Загрузка перемещений.
        private async Task LoadTransferHeadsAsync()
        {
            int? selectedId = _selectedTransferHeadId;
            DataTable table = await GetTableAsync("TRANSFERHEADS_GET_ALL", "OK:TRANSFERHEADS|");
            BindGrid(_dgvTransferHeads, table);
            RestoreGridSelection(_dgvTransferHeads, "transferheadid", selectedId);
            await OnTransferHeadSelectionChangedAsync();
        }

        // Загрузка строк выбранного перемещения.
        private async Task OnTransferHeadSelectionChangedAsync()
        {
            _selectedTransferHeadId = GetSelectedGridId(_dgvTransferHeads, "transferheadid");
            _selectedTransferSpecId = null;

            if (_selectedTransferHeadId == null)
            {
                BindGrid(_dgvTransferSpecs, new DataTable());
                return;
            }

            DataTable table = await GetTableAsync(
                $"TRANSFERSPECS_GET_BY_HEAD|{_selectedTransferHeadId.Value}",
                "OK:TRANSFERSPECS|");

            BindGrid(_dgvTransferSpecs, table);
        }

        // Создание перемещения.
        private async Task CreateTransferHeadAsync()
        {
            int storeOutId = RequireComboInt(_cmbTransferStoreOut, "Выберите склад-отправитель.");
            int storeInId = RequireComboInt(_cmbTransferStoreIn, "Выберите склад-получатель.");

            if (storeOutId == storeInId)
                throw new Exception("Склад отправки и склад получения должны различаться.");

            string response = await SendCommandCheckedAsync($"TRANSFERHEAD_ADD|{storeOutId}|{storeInId}");
            int createdId = ParseIdResponse(response, "OK:TRANSFERHEAD_ADD|");

            await LoadTransferHeadsAsync();
            RestoreGridSelection(_dgvTransferHeads, "transferheadid", createdId);
        }

        // Добавление строки перемещения.
        private async Task AddTransferSpecAsync()
        {
            if (_selectedTransferHeadId == null)
                throw new Exception("Сначала выберите документ перемещения.");

            int productId = RequireComboInt(_cmbTransferProduct, "Выберите товар.");
            decimal quant = _nudTransferQuant.Value;

            await SendCommandCheckedAsync(
                $"TRANSFERSPEC_ADD|{_selectedTransferHeadId.Value}|{productId}|{ToProtocolDecimal(quant)}");

            await OnTransferHeadSelectionChangedAsync();
        }

        // Удаление строки перемещения.
        private async Task DeleteTransferSpecAsync()
        {
            if (_selectedTransferSpecId == null)
                throw new Exception("Сначала выберите строку перемещения.");

            await SendCommandCheckedAsync($"TRANSFERSPEC_DELETE|{_selectedTransferSpecId.Value}");
            await OnTransferHeadSelectionChangedAsync();
        }

        // Отправка перемещения.
        private async Task SendTransferHeadAsync()
        {
            if (_selectedTransferHeadId == null)
                throw new Exception("Сначала выберите документ перемещения.");

            await SendCommandCheckedAsync($"TRANSFERHEAD_SEND|{_selectedTransferHeadId.Value}");
            await LoadTransferHeadsAsync();
            await LoadStockAsync();
        }

        // Подтверждение приема перемещения.
        private async Task AcceptTransferHeadAsync()
        {
            if (_selectedTransferHeadId == null)
                throw new Exception("Сначала выберите документ перемещения.");

            await SendCommandCheckedAsync($"TRANSFERHEAD_ACCEPT|{_selectedTransferHeadId.Value}");
            await LoadTransferHeadsAsync();
            await LoadStockAsync();
        }

        // Получение таблицы от сервера.
        private async Task<DataTable> GetTableAsync(string command, string okPrefix)
        {
            string response = await SendCommandCheckedAsync(command);
            string json = ExtractPayload(response, okPrefix);
            return JsonToTable(json);
        }

        // Отправка команды с проверкой ответа.
        private async Task<string> SendCommandCheckedAsync(string command)
        {
            string? response = await SendCommandAsync(command);

            if (string.IsNullOrWhiteSpace(response))
                throw new Exception("Сервер не вернул ответ.");

            if (response.StartsWith("ERR:", StringComparison.Ordinal))
                throw new Exception(response.Substring(4));

            return response;
        }

        // Отправка команды на сервер.
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

        // Преобразование текста JSON в таблицу.
        private DataTable JsonToTable(string json)
        {
            DataTable table = new DataTable();
            List<Dictionary<string, string>>? rows =
                JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

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

        // Выделение полезной части ответа.
        private string ExtractPayload(string response, string okPrefix)
        {
            if (!response.StartsWith(okPrefix, StringComparison.Ordinal))
                throw new Exception("Сервер вернул неожиданный ответ: " + response);

            return response.Substring(okPrefix.Length);
        }

        // Получение идентификатора из ответа.
        private int ParseIdResponse(string response, string okPrefix)
        {
            string value = ExtractPayload(response, okPrefix);

            if (!int.TryParse(value, out int id))
                throw new Exception("Сервер вернул неверный идентификатор.");

            return id;
        }

        // Привязка таблицы к сетке.
        private void BindGrid(DataGridView grid, DataTable table)
        {
            grid.DataSource = table;
            grid.ClearSelection();
        }

        // Привязка таблицы к выпадающему списку.
        private void BindCombo(ComboBox combo, DataTable table, string valueMember, string displayMember)
        {
            combo.DataSource = table;
            combo.ValueMember = valueMember;
            combo.DisplayMember = displayMember;

            if (table.Rows.Count > 0)
                combo.SelectedIndex = 0;
        }

        // Создание таблицы фильтра с первой строкой "Все ...".
        private DataTable CreateFilterTable(DataTable source, string valueColumn, string textColumn, string firstText)
        {
            DataTable table = source.Copy();
            DataRow row = table.NewRow();
            row[valueColumn] = "";
            row[textColumn] = firstText;
            table.Rows.InsertAt(row, 0);
            return table;
        }

        // Получение значения из списка или пустой строки.
        private string GetComboValueOrEmpty(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return "";

            return combo.SelectedValue.ToString() ?? "";
        }

        // Получение обязательного целого значения из списка.
        private int RequireComboInt(ComboBox combo, string errorMessage)
        {
            if (combo.SelectedValue == null || !int.TryParse(combo.SelectedValue.ToString(), out int result))
                throw new Exception(errorMessage);

            return result;
        }

        // Получение идентификатора из выбранной строки таблицы.
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

        // Восстановление выбранной строки после обновления данных.
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

        // Преобразование числа в строку для протокола.
        private string ToProtocolDecimal(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        // Безопасный запуск действия с единым выводом ошибок.
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

        // Закрытие формы и соединения.
        private async void StorageEmployee_FormClosing(object? sender, FormClosingEventArgs e)
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