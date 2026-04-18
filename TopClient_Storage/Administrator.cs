using System.Data;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace TopClient_Storage
{
    public partial class Administrator : Form
    {
        private readonly TcpClient _client;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        private int? _selectedUserId;
        private string _selectedUserLongName = "";

        private int? _selectedCategoryId;
        private string _selectedCategoryCode = "";

        private int? _selectedProductId;
        private string _selectedProductCode = "";

        private bool _isClosing;

        private sealed class ReportTypeItem
        {
            public string Code { get; set; } = "";
            public string Title { get; set; } = "";
        }

        public Administrator(TcpClient client, StreamReader reader, StreamWriter writer)
        {
            InitializeComponent();

            _client = client;
            _reader = reader;
            _writer = writer;

            this.Load -= Administrator_Load;
            this.Load += Administrator_Load;

            this.FormClosing -= Administrator_FormClosing;
            this.FormClosing += Administrator_FormClosing;

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.SelectionChanged -= dgvUsers_SelectionChanged;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;

            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.SelectionChanged -= dgvCategories_SelectionChanged;
            dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged -= dgvProducts_SelectionChanged;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;

            dgvReports.ReadOnly = true;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnRefresh.Click -= btnRefresh_Click;
            btnRefresh.Click += btnRefresh_Click;

            btnCreate.Click -= btnCreate_Click;
            btnCreate.Click += btnCreate_Click;

            btnCreateClear.Click -= btnCreateClear_Click;
            btnCreateClear.Click += btnCreateClear_Click;

            btnUpdate.Click -= btnUpdate_Click;
            btnUpdate.Click += btnUpdate_Click;

            btnUpdateClear.Click -= btnUpdateClear_Click;
            btnUpdateClear.Click += btnUpdateClear_Click;

            btnDelete.Click -= btnDelete_Click;
            btnDelete.Click += btnDelete_Click;

            btnCategoryRefresh.Click -= btnCategoryRefresh_Click;
            btnCategoryRefresh.Click += btnCategoryRefresh_Click;

            btnCategoryClear.Click -= btnCategoryClear_Click;
            btnCategoryClear.Click += btnCategoryClear_Click;

            btnCategoryAdd.Click -= btnCategoryAdd_Click;
            btnCategoryAdd.Click += btnCategoryAdd_Click;

            btnCategoryUpdate.Click -= btnCategoryUpdate_Click;
            btnCategoryUpdate.Click += btnCategoryUpdate_Click;

            btnCategoryDelete.Click -= btnCategoryDelete_Click;
            btnCategoryDelete.Click += btnCategoryDelete_Click;

            btnProductRefresh.Click -= btnProductRefresh_Click;
            btnProductRefresh.Click += btnProductRefresh_Click;

            btnProductClear.Click -= btnProductClear_Click;
            btnProductClear.Click += btnProductClear_Click;

            btnProductAdd.Click -= btnProductAdd_Click;
            btnProductAdd.Click += btnProductAdd_Click;

            btnProductUpdate.Click -= btnProductUpdate_Click;
            btnProductUpdate.Click += btnProductUpdate_Click;

            btnProductDelete.Click -= btnProductDelete_Click;
            btnProductDelete.Click += btnProductDelete_Click;

            btnReportShow.Click -= btnReportShow_Click;
            btnReportShow.Click += btnReportShow_Click;

            cmbReportType.SelectedIndexChanged -= cmbReportType_SelectedIndexChanged;
            cmbReportType.SelectedIndexChanged += cmbReportType_SelectedIndexChanged;
        }

        private async Task<string?> SendCommandAsync(string command)
        {
            await _sendLock.WaitAsync();

            try
            {
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
            var rows = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

            if (rows == null || rows.Count == 0)
                return table;

            foreach (string col in rows[0].Keys)
                table.Columns.Add(col);

            foreach (var item in rows)
            {
                DataRow row = table.NewRow();

                foreach (var pair in item)
                    row[pair.Key] = pair.Value;

                table.Rows.Add(row);
            }

            return table;
        }

        private bool CheckTextForProtocol(string value)
        {
            return !value.Contains("|");
        }

        private bool ValidateCreateUserFields()
        {
            if (string.IsNullOrWhiteSpace(txtCreateName.Text))
            {
                MessageBox.Show("Введите имя для нового пользователя.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCreateLogin.Text))
            {
                MessageBox.Show("Введите логин для нового пользователя.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCreatePassword.Text))
            {
                MessageBox.Show("Введите пароль для нового пользователя.");
                return false;
            }

            if (cmbCreateRole.SelectedValue == null)
            {
                MessageBox.Show("Выберите роль для нового пользователя.");
                return false;
            }

            if (!CheckTextForProtocol(txtCreateName.Text) ||
                !CheckTextForProtocol(txtCreateSname.Text) ||
                !CheckTextForProtocol(txtCreateFname.Text) ||
                !CheckTextForProtocol(txtCreateLogin.Text) ||
                !CheckTextForProtocol(txtCreatePassword.Text))
            {
                MessageBox.Show("Символ '|' нельзя использовать в полях.");
                return false;
            }

            return true;
        }

        private bool ValidateUpdateUserFields()
        {
            if (_selectedUserId == null)
            {
                MessageBox.Show("Сначала выберите пользователя в таблице.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUpdateName.Text))
            {
                MessageBox.Show("Введите имя пользователя.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUpdateLogin.Text))
            {
                MessageBox.Show("Введите логин пользователя.");
                return false;
            }

            if (cmbUpdateRole.SelectedValue == null)
            {
                MessageBox.Show("Выберите роль пользователя.");
                return false;
            }

            if (!CheckTextForProtocol(txtUpdateName.Text) ||
                !CheckTextForProtocol(txtUpdateSname.Text) ||
                !CheckTextForProtocol(txtUpdateFname.Text) ||
                !CheckTextForProtocol(txtUpdateLogin.Text) ||
                !CheckTextForProtocol(txtUpdatePassword.Text))
            {
                MessageBox.Show("Символ '|' нельзя использовать в полях.");
                return false;
            }

            return true;
        }

        private bool ValidateCategoryFields()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryCode.Text))
            {
                MessageBox.Show("Введите код категории.");
                return false;
            }

            if (!CheckTextForProtocol(txtCategoryCode.Text))
            {
                MessageBox.Show("Символ '|' нельзя использовать в коде категории.");
                return false;
            }

            return true;
        }

        private bool ValidateProductFields(bool requireSelectedProduct)
        {
            if (requireSelectedProduct && _selectedProductId == null)
            {
                MessageBox.Show("Сначала выберите товар в таблице.");
                return false;
            }

            if (cmbProductCategory.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию товара.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                MessageBox.Show("Введите код товара.");
                return false;
            }

            if (!CheckTextForProtocol(txtProductCode.Text) ||
                !CheckTextForProtocol(txtProductDescription.Text))
            {
                MessageBox.Show("Символ '|' нельзя использовать в полях товара.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtProductWeight.Text) &&
                !int.TryParse(txtProductWeight.Text.Trim(), out _))
            {
                MessageBox.Show("Вес должен быть целым числом.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtProductSize.Text) &&
                !int.TryParse(txtProductSize.Text.Trim(), out _))
            {
                MessageBox.Show("Размер должен быть целым числом.");
                return false;
            }

            return true;
        }

        private async Task LoadRolesAsync()
        {
            string? response = await SendCommandAsync("ROLES_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:ROLES|"))
                throw new Exception("Не удалось загрузить роли: " + response);

            string json = response.Substring("OK:ROLES|".Length);
            DataTable dt = JsonToTable(json);

            cmbCreateRole.DataSource = dt.Copy();
            cmbCreateRole.DisplayMember = "rolecode";
            cmbCreateRole.ValueMember = "roleid";

            cmbUpdateRole.DataSource = dt.Copy();
            cmbUpdateRole.DisplayMember = "rolecode";
            cmbUpdateRole.ValueMember = "roleid";
        }

        private async Task LoadUsersAsync()
        {
            string? response = await SendCommandAsync("USERS_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:USERS|"))
                throw new Exception("Не удалось загрузить пользователей: " + response);

            string json = response.Substring("OK:USERS|".Length);
            DataTable dt = JsonToTable(json);

            dgvUsers.DataSource = dt;

            if (dgvUsers.Columns.Contains("userid"))
                dgvUsers.Columns["userid"].Visible = false;

            if (dgvUsers.Columns.Contains("roleid"))
                dgvUsers.Columns["roleid"].Visible = false;
        }

        private async Task LoadCategoriesAsync()
        {
            string? response = await SendCommandAsync("CATEGORIES_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:CATEGORIES|"))
                throw new Exception("Не удалось загрузить категории: " + response);

            string json = response.Substring("OK:CATEGORIES|".Length);
            DataTable dt = JsonToTable(json);

            dgvCategories.DataSource = dt;

            if (dgvCategories.Columns.Contains("categoryid"))
                dgvCategories.Columns["categoryid"].Visible = false;

            cmbProductCategory.DataSource = dt.Copy();
            cmbProductCategory.DisplayMember = "categorycode";
            cmbProductCategory.ValueMember = "categoryid";
        }

        private async Task LoadProductsAsync()
        {
            string? response = await SendCommandAsync("PRODUCTS_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:PRODUCTS|"))
                throw new Exception("Не удалось загрузить товары: " + response);

            string json = response.Substring("OK:PRODUCTS|".Length);
            DataTable dt = JsonToTable(json);

            dgvProducts.DataSource = dt;

            if (dgvProducts.Columns.Contains("productid"))
                dgvProducts.Columns["productid"].Visible = false;

            if (dgvProducts.Columns.Contains("categoryid"))
                dgvProducts.Columns["categoryid"].Visible = false;
        }

        private async Task LoadClientsAsync()
        {
            string? response = await SendCommandAsync("CLIENTS_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:CLIENTS|"))
                throw new Exception("Не удалось загрузить клиентов: " + response);

            string json = response.Substring("OK:CLIENTS|".Length);
            DataTable dt = JsonToTable(json);

            cmbReportClient.DataSource = dt;
            cmbReportClient.DisplayMember = "clientname";
            cmbReportClient.ValueMember = "clientid";
        }

        private async Task LoadReportProductsAsync()
        {
            string? response = await SendCommandAsync("PRODUCTS_SHORT_GET_ALL");

            if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:PRODUCTS_SHORT|"))
                throw new Exception("Не удалось загрузить список товаров для отчетов: " + response);

            string json = response.Substring("OK:PRODUCTS_SHORT|".Length);
            DataTable dt = JsonToTable(json);

            cmbReportProduct.DataSource = dt;
            cmbReportProduct.DisplayMember = "productcode";
            cmbReportProduct.ValueMember = "productid";
        }

        private void InitReportTypeCombo()
        {
            List<ReportTypeItem> items = new List<ReportTypeItem>
            {
                new ReportTypeItem { Code = "REPORT_STOCK", Title = "Остатки по складам" },
                new ReportTypeItem { Code = "REPORT_MOVEMENT", Title = "Движение товаров" },
                new ReportTypeItem { Code = "REPORT_ORDERS_BY_DATE", Title = "Заказы по дате" },
                new ReportTypeItem { Code = "REPORT_ORDERS_BY_CLIENT", Title = "Заказы по клиенту" },
                new ReportTypeItem { Code = "REPORT_ORDERS_BY_PRODUCT", Title = "Заказы по товару" }
            };

            cmbReportType.DataSource = items;
            cmbReportType.DisplayMember = "Title";
            cmbReportType.ValueMember = "Code";
        }

        private void UpdateReportFilterVisibility()
        {
            string reportCode = cmbReportType.SelectedValue?.ToString() ?? "";

            bool needDates = reportCode == "REPORT_MOVEMENT" || reportCode == "REPORT_ORDERS_BY_DATE";
            bool needClient = reportCode == "REPORT_ORDERS_BY_CLIENT";
            bool needProduct = reportCode == "REPORT_ORDERS_BY_PRODUCT";

            lblDateFrom.Visible = needDates;
            lblDateTo.Visible = needDates;
            dtpDateFrom.Visible = needDates;
            dtpDateTo.Visible = needDates;

            lblReportClient.Visible = needClient;
            cmbReportClient.Visible = needClient;

            lblReportProduct.Visible = needProduct;
            cmbReportProduct.Visible = needProduct;
        }

        private void ClearCreateFields()
        {
            txtCreateName.Clear();
            txtCreateSname.Clear();
            txtCreateFname.Clear();
            txtCreateLogin.Clear();
            txtCreatePassword.Clear();

            if (cmbCreateRole.Items.Count > 0)
                cmbCreateRole.SelectedIndex = 0;
        }

        private void ClearUpdateFields()
        {
            txtUpdateName.Clear();
            txtUpdateSname.Clear();
            txtUpdateFname.Clear();
            txtUpdateLogin.Clear();
            txtUpdatePassword.Clear();

            if (cmbUpdateRole.Items.Count > 0)
                cmbUpdateRole.SelectedIndex = 0;

            _selectedUserId = null;
            _selectedUserLongName = "";

            lblUpdateSelected.Text = "Выбранный пользователь: не выбран";
            lblDeleteSelected.Text = "Пользователь для удаления: не выбран";
        }

        private void ClearCategoryFields()
        {
            txtCategoryCode.Clear();
            _selectedCategoryId = null;
            _selectedCategoryCode = "";
            lblCategorySelected.Text = "Выбранная категория: не выбрана";
        }

        private void ClearProductFields()
        {
            txtProductCode.Clear();
            txtProductDescription.Clear();
            txtProductWeight.Clear();
            txtProductSize.Clear();

            if (cmbProductCategory.Items.Count > 0)
                cmbProductCategory.SelectedIndex = 0;

            _selectedProductId = null;
            _selectedProductCode = "";
            lblProductSelected.Text = "Выбранный товар: не выбран";
        }

        private void FillUserBlocksFromGrid()
        {
            if (dgvUsers.CurrentRow == null)
            {
                ClearUpdateFields();
                return;
            }

            DataGridViewRow row = dgvUsers.CurrentRow;

            string userIdText = row.Cells["userid"].Value?.ToString() ?? "";
            if (!int.TryParse(userIdText, out int userId))
            {
                ClearUpdateFields();
                return;
            }

            _selectedUserId = userId;
            _selectedUserLongName = row.Cells["longname"].Value?.ToString() ?? "";

            txtUpdateName.Text = row.Cells["name"].Value?.ToString() ?? "";
            txtUpdateSname.Text = row.Cells["sname"].Value?.ToString() ?? "";
            txtUpdateFname.Text = row.Cells["fname"].Value?.ToString() ?? "";
            txtUpdateLogin.Text = row.Cells["login"].Value?.ToString() ?? "";
            txtUpdatePassword.Clear();

            string roleId = row.Cells["roleid"].Value?.ToString() ?? "";
            if (!string.IsNullOrWhiteSpace(roleId))
                cmbUpdateRole.SelectedValue = roleId;

            lblUpdateSelected.Text = $"Выбранный пользователь: {_selectedUserLongName}";
            lblDeleteSelected.Text = $"Пользователь для удаления: {_selectedUserLongName}";
        }

        private void FillCategoryBlockFromGrid()
        {
            if (dgvCategories.CurrentRow == null)
            {
                ClearCategoryFields();
                return;
            }

            DataGridViewRow row = dgvCategories.CurrentRow;

            string idText = row.Cells["categoryid"].Value?.ToString() ?? "";
            if (!int.TryParse(idText, out int categoryId))
            {
                ClearCategoryFields();
                return;
            }

            _selectedCategoryId = categoryId;
            _selectedCategoryCode = row.Cells["categorycode"].Value?.ToString() ?? "";

            txtCategoryCode.Text = _selectedCategoryCode;
            lblCategorySelected.Text = $"Выбранная категория: {_selectedCategoryCode}";
        }

        private void FillProductBlockFromGrid()
        {
            if (dgvProducts.CurrentRow == null)
            {
                ClearProductFields();
                return;
            }

            DataGridViewRow row = dgvProducts.CurrentRow;

            string idText = row.Cells["productid"].Value?.ToString() ?? "";
            if (!int.TryParse(idText, out int productId))
            {
                ClearProductFields();
                return;
            }

            _selectedProductId = productId;
            _selectedProductCode = row.Cells["productcode"].Value?.ToString() ?? "";

            txtProductCode.Text = row.Cells["productcode"].Value?.ToString() ?? "";
            txtProductDescription.Text = row.Cells["description"].Value?.ToString() ?? "";
            txtProductWeight.Text = row.Cells["weight"].Value?.ToString() ?? "";
            txtProductSize.Text = row.Cells["size"].Value?.ToString() ?? "";

            string categoryId = row.Cells["categoryid"].Value?.ToString() ?? "";
            if (!string.IsNullOrWhiteSpace(categoryId))
                cmbProductCategory.SelectedValue = categoryId;

            lblProductSelected.Text = $"Выбранный товар: {_selectedProductCode}";
        }

        private string EscapeProtocol(string value)
        {
            return value.Replace("|", "/");
        }

        private async void Administrator_Load(object? sender, EventArgs e)
        {
            try
            {
                InitReportTypeCombo();
                UpdateReportFilterVisibility();

                await LoadRolesAsync();
                await LoadUsersAsync();
                await LoadCategoriesAsync();
                await LoadProductsAsync();
                await LoadClientsAsync();
                await LoadReportProductsAsync();

                if (dgvUsers.Rows.Count > 0)
                    FillUserBlocksFromGrid();

                if (dgvCategories.Rows.Count > 0)
                    FillCategoryBlockFromGrid();

                if (dgvProducts.Rows.Count > 0)
                    FillProductBlockFromGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке формы: " + ex.Message);
            }
        }

        private void dgvUsers_SelectionChanged(object? sender, EventArgs e)
        {
            FillUserBlocksFromGrid();
        }

        private void dgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            FillCategoryBlockFromGrid();
        }

        private void dgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            FillProductBlockFromGrid();
        }

        private void cmbReportType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateReportFilterVisibility();
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления списка пользователей: " + ex.Message);
            }
        }

        private void btnCreateClear_Click(object? sender, EventArgs e)
        {
            ClearCreateFields();
        }

        private void btnUpdateClear_Click(object? sender, EventArgs e)
        {
            ClearUpdateFields();
        }

        private async void btnCreate_Click(object? sender, EventArgs e)
        {
            if (!ValidateCreateUserFields())
                return;

            btnCreate.Enabled = false;

            try
            {
                string command =
                    $"USER_ADD|{EscapeProtocol(txtCreateName.Text.Trim())}|{EscapeProtocol(txtCreateSname.Text.Trim())}|{EscapeProtocol(txtCreateFname.Text.Trim())}|{cmbCreateRole.SelectedValue}|{EscapeProtocol(txtCreateLogin.Text.Trim())}|{EscapeProtocol(txtCreatePassword.Text)}";

                string? response = await SendCommandAsync(command);

                if (response != null && response.StartsWith("OK:USER_ADD"))
                {
                    await LoadUsersAsync();
                    ClearCreateFields();
                    MessageBox.Show("Пользователь добавлен.");
                }
                else
                {
                    MessageBox.Show("Ошибка добавления: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message);
            }
            finally
            {
                btnCreate.Enabled = true;
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!ValidateUpdateUserFields())
                return;

            btnUpdate.Enabled = false;

            try
            {
                string command =
                    $"USER_UPDATE|{_selectedUserId}|{EscapeProtocol(txtUpdateName.Text.Trim())}|{EscapeProtocol(txtUpdateSname.Text.Trim())}|{EscapeProtocol(txtUpdateFname.Text.Trim())}|{cmbUpdateRole.SelectedValue}|{EscapeProtocol(txtUpdateLogin.Text.Trim())}|{EscapeProtocol(txtUpdatePassword.Text)}";

                string? response = await SendCommandAsync(command);

                if (response == "OK:USER_UPDATE")
                {
                    await LoadUsersAsync();
                    MessageBox.Show("Пользователь изменен.");
                }
                else
                {
                    MessageBox.Show("Ошибка изменения: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении пользователя: " + ex.Message);
            }
            finally
            {
                btnUpdate.Enabled = true;
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedUserId == null)
            {
                MessageBox.Show("Сначала выберите пользователя.");
                return;
            }

            if (MessageBox.Show(
                $"Удалить пользователя \"{_selectedUserLongName}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnDelete.Enabled = false;

            try
            {
                string? response = await SendCommandAsync($"USER_DELETE|{_selectedUserId}");

                if (response == "OK:USER_DELETE")
                {
                    await LoadUsersAsync();
                    ClearUpdateFields();
                    MessageBox.Show("Пользователь удален.");
                }
                else
                {
                    MessageBox.Show("Ошибка удаления: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении пользователя: " + ex.Message);
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        private async void btnCategoryRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                await LoadCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления категорий: " + ex.Message);
            }
        }

        private void btnCategoryClear_Click(object? sender, EventArgs e)
        {
            ClearCategoryFields();
        }

        private async void btnCategoryAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateCategoryFields())
                return;

            btnCategoryAdd.Enabled = false;

            try
            {
                string command = $"CATEGORY_ADD|{EscapeProtocol(txtCategoryCode.Text.Trim())}";
                string? response = await SendCommandAsync(command);

                if (response != null && response.StartsWith("OK:CATEGORY_ADD"))
                {
                    await LoadCategoriesAsync();
                    await LoadProductsAsync();
                    ClearCategoryFields();
                    MessageBox.Show("Категория добавлена.");
                }
                else
                {
                    MessageBox.Show("Ошибка добавления категории: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении категории: " + ex.Message);
            }
            finally
            {
                btnCategoryAdd.Enabled = true;
            }
        }

        private async void btnCategoryUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedCategoryId == null)
            {
                MessageBox.Show("Сначала выберите категорию.");
                return;
            }

            if (!ValidateCategoryFields())
                return;

            btnCategoryUpdate.Enabled = false;

            try
            {
                string command = $"CATEGORY_UPDATE|{_selectedCategoryId}|{EscapeProtocol(txtCategoryCode.Text.Trim())}";
                string? response = await SendCommandAsync(command);

                if (response == "OK:CATEGORY_UPDATE")
                {
                    await LoadCategoriesAsync();
                    await LoadProductsAsync();
                    MessageBox.Show("Категория изменена.");
                }
                else
                {
                    MessageBox.Show("Ошибка изменения категории: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении категории: " + ex.Message);
            }
            finally
            {
                btnCategoryUpdate.Enabled = true;
            }
        }

        private async void btnCategoryDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedCategoryId == null)
            {
                MessageBox.Show("Сначала выберите категорию.");
                return;
            }

            if (MessageBox.Show(
                $"Удалить категорию \"{_selectedCategoryCode}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnCategoryDelete.Enabled = false;

            try
            {
                string? response = await SendCommandAsync($"CATEGORY_DELETE|{_selectedCategoryId}");

                if (response == "OK:CATEGORY_DELETE")
                {
                    await LoadCategoriesAsync();
                    await LoadProductsAsync();
                    ClearCategoryFields();
                    MessageBox.Show("Категория удалена.");
                }
                else
                {
                    MessageBox.Show("Ошибка удаления категории: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении категории: " + ex.Message);
            }
            finally
            {
                btnCategoryDelete.Enabled = true;
            }
        }

        private async void btnProductRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                await LoadProductsAsync();
                await LoadReportProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления товаров: " + ex.Message);
            }
        }

        private void btnProductClear_Click(object? sender, EventArgs e)
        {
            ClearProductFields();
        }

        private async void btnProductAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateProductFields(false))
                return;

            btnProductAdd.Enabled = false;

            try
            {
                string command =
                    $"PRODUCT_ADD|{cmbProductCategory.SelectedValue}|{EscapeProtocol(txtProductCode.Text.Trim())}|{EscapeProtocol(txtProductDescription.Text.Trim())}|{txtProductWeight.Text.Trim()}|{txtProductSize.Text.Trim()}";

                string? response = await SendCommandAsync(command);

                if (response != null && response.StartsWith("OK:PRODUCT_ADD"))
                {
                    await LoadProductsAsync();
                    await LoadReportProductsAsync();
                    ClearProductFields();
                    MessageBox.Show("Товар добавлен.");
                }
                else
                {
                    MessageBox.Show("Ошибка добавления товара: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении товара: " + ex.Message);
            }
            finally
            {
                btnProductAdd.Enabled = true;
            }
        }

        private async void btnProductUpdate_Click(object? sender, EventArgs e)
        {
            if (!ValidateProductFields(true))
                return;

            btnProductUpdate.Enabled = false;

            try
            {
                string command =
                    $"PRODUCT_UPDATE|{_selectedProductId}|{cmbProductCategory.SelectedValue}|{EscapeProtocol(txtProductCode.Text.Trim())}|{EscapeProtocol(txtProductDescription.Text.Trim())}|{txtProductWeight.Text.Trim()}|{txtProductSize.Text.Trim()}";

                string? response = await SendCommandAsync(command);

                if (response == "OK:PRODUCT_UPDATE")
                {
                    await LoadProductsAsync();
                    await LoadReportProductsAsync();
                    MessageBox.Show("Товар изменен.");
                }
                else
                {
                    MessageBox.Show("Ошибка изменения товара: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении товара: " + ex.Message);
            }
            finally
            {
                btnProductUpdate.Enabled = true;
            }
        }

        private async void btnProductDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedProductId == null)
            {
                MessageBox.Show("Сначала выберите товар.");
                return;
            }

            if (MessageBox.Show(
                $"Удалить товар \"{_selectedProductCode}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            btnProductDelete.Enabled = false;

            try
            {
                string? response = await SendCommandAsync($"PRODUCT_DELETE|{_selectedProductId}");

                if (response == "OK:PRODUCT_DELETE")
                {
                    await LoadProductsAsync();
                    await LoadReportProductsAsync();
                    ClearProductFields();
                    MessageBox.Show("Товар удален.");
                }
                else
                {
                    MessageBox.Show("Ошибка удаления товара: " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении товара: " + ex.Message);
            }
            finally
            {
                btnProductDelete.Enabled = true;
            }
        }

        private async void btnReportShow_Click(object? sender, EventArgs e)
        {
            btnReportShow.Enabled = false;

            try
            {
                string reportCode = cmbReportType.SelectedValue?.ToString() ?? "";
                string command;

                switch (reportCode)
                {
                    case "REPORT_STOCK":
                        command = "REPORT_STOCK";
                        break;

                    case "REPORT_MOVEMENT":
                        command = $"REPORT_MOVEMENT|{dtpDateFrom.Value:yyyy-MM-dd}|{dtpDateTo.Value:yyyy-MM-dd}";
                        break;

                    case "REPORT_ORDERS_BY_DATE":
                        command = $"REPORT_ORDERS_BY_DATE|{dtpDateFrom.Value:yyyy-MM-dd}|{dtpDateTo.Value:yyyy-MM-dd}";
                        break;

                    case "REPORT_ORDERS_BY_CLIENT":
                        if (cmbReportClient.SelectedValue == null)
                        {
                            MessageBox.Show("Выберите клиента.");
                            return;
                        }
                        command = $"REPORT_ORDERS_BY_CLIENT|{cmbReportClient.SelectedValue}";
                        break;

                    case "REPORT_ORDERS_BY_PRODUCT":
                        if (cmbReportProduct.SelectedValue == null)
                        {
                            MessageBox.Show("Выберите товар.");
                            return;
                        }
                        command = $"REPORT_ORDERS_BY_PRODUCT|{cmbReportProduct.SelectedValue}";
                        break;

                    default:
                        MessageBox.Show("Не выбран тип отчета.");
                        return;
                }

                string? response = await SendCommandAsync(command);

                if (string.IsNullOrWhiteSpace(response) || !response.StartsWith("OK:REPORT|"))
                {
                    MessageBox.Show("Ошибка получения отчета: " + response);
                    return;
                }

                string json = response.Substring("OK:REPORT|".Length);
                DataTable dt = JsonToTable(json);
                dgvReports.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при построении отчета: " + ex.Message);
            }
            finally
            {
                btnReportShow.Enabled = true;
            }
        }

        private async void Administrator_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isClosing)
                return;

            _isClosing = true;

            try
            {
                await _sendLock.WaitAsync();

                try
                {
                    if (_client.Connected)
                    {
                        await _writer.WriteLineAsync("QUIT");
                        await _reader.ReadLineAsync();
                    }
                }
                finally
                {
                    _sendLock.Release();
                }
            }
            catch
            {
            }
            finally
            {
                _reader.Dispose();
                _writer.Dispose();
                _client.Close();
                _sendLock.Dispose();
            }
        }

        private void btnCategoryRefresh_Click_1(object sender, EventArgs e)
        {

        }
    }
}