namespace TopClient_Storage
{
    partial class Administrator
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освобождение используемых ресурсов.
        /// </summary>
        /// <param name="disposing">Признак освобождения управляемых ресурсов.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм

        /// <summary>
        /// Метод поддержки конструктора.
        /// Не изменяй содержимое этого метода вручную в редакторе кода.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxUsersRead = new GroupBox();
            btnRefresh = new Button();
            dgvUsers = new DataGridView();
            groupBoxUserCreate = new GroupBox();
            btnCreateClear = new Button();
            btnCreate = new Button();
            cmbCreateRole = new ComboBox();
            lblCreateRole = new Label();
            txtCreatePassword = new TextBox();
            lblCreatePassword = new Label();
            txtCreateLogin = new TextBox();
            lblCreateLogin = new Label();
            txtCreateFname = new TextBox();
            lblCreateFname = new Label();
            txtCreateSname = new TextBox();
            lblCreateSname = new Label();
            txtCreateName = new TextBox();
            lblCreateName = new Label();
            groupBoxUserUpdate = new GroupBox();
            lblUpdateHint = new Label();
            lblUpdateSelected = new Label();
            btnUpdateClear = new Button();
            btnUpdate = new Button();
            cmbUpdateRole = new ComboBox();
            lblUpdateRole = new Label();
            txtUpdatePassword = new TextBox();
            lblUpdatePassword = new Label();
            txtUpdateLogin = new TextBox();
            lblUpdateLogin = new Label();
            txtUpdateFname = new TextBox();
            lblUpdateFname = new Label();
            txtUpdateSname = new TextBox();
            lblUpdateSname = new Label();
            txtUpdateName = new TextBox();
            lblUpdateName = new Label();
            groupBoxUserDelete = new GroupBox();
            lblDeleteHint = new Label();
            lblDeleteSelected = new Label();
            btnDelete = new Button();
            groupBoxCategories = new GroupBox();
            lblCategorySelected = new Label();
            btnCategoryDelete = new Button();
            btnCategoryUpdate = new Button();
            btnCategoryAdd = new Button();
            btnCategoryClear = new Button();
            btnCategoryRefresh = new Button();
            txtCategoryCode = new TextBox();
            lblCategoryCode = new Label();
            dgvCategories = new DataGridView();
            groupBoxProducts = new GroupBox();
            lblProductSelected = new Label();
            btnProductDelete = new Button();
            btnProductUpdate = new Button();
            btnProductAdd = new Button();
            btnProductClear = new Button();
            btnProductRefresh = new Button();
            cmbProductCategory = new ComboBox();
            lblProductCategory = new Label();
            txtProductSize = new TextBox();
            lblProductSize = new Label();
            txtProductWeight = new TextBox();
            lblProductWeight = new Label();
            txtProductDescription = new TextBox();
            lblProductDescription = new Label();
            txtProductCode = new TextBox();
            lblProductCode = new Label();
            dgvProducts = new DataGridView();
            groupBoxReports = new GroupBox();
            dgvReports = new DataGridView();
            btnReportShow = new Button();
            cmbReportProduct = new ComboBox();
            lblReportProduct = new Label();
            cmbReportClient = new ComboBox();
            lblReportClient = new Label();
            dtpDateTo = new DateTimePicker();
            lblDateTo = new Label();
            dtpDateFrom = new DateTimePicker();
            lblDateFrom = new Label();
            cmbReportType = new ComboBox();
            lblReportType = new Label();
            groupBoxUsersRead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            groupBoxUserCreate.SuspendLayout();
            groupBoxUserUpdate.SuspendLayout();
            groupBoxUserDelete.SuspendLayout();
            groupBoxCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            groupBoxProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            groupBoxReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // groupBoxUsersRead
            // 
            groupBoxUsersRead.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxUsersRead.Controls.Add(btnRefresh);
            groupBoxUsersRead.Controls.Add(dgvUsers);
            groupBoxUsersRead.Location = new Point(10, 9);
            groupBoxUsersRead.Margin = new Padding(3, 2, 3, 2);
            groupBoxUsersRead.Name = "groupBoxUsersRead";
            groupBoxUsersRead.Padding = new Padding(3, 2, 3, 2);
            groupBoxUsersRead.Size = new Size(1365, 210);
            groupBoxUsersRead.TabIndex = 0;
            groupBoxUsersRead.TabStop = false;
            groupBoxUsersRead.Text = "Просмотр пользователей";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(1222, 20);
            btnRefresh.Margin = new Padding(3, 2, 3, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(128, 26);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(14, 50);
            dgvUsers.Margin = new Padding(3, 2, 3, 2);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.RowTemplate.Height = 29;
            dgvUsers.Size = new Size(1335, 148);
            dgvUsers.TabIndex = 0;
            // 
            // groupBoxUserCreate
            // 
            groupBoxUserCreate.Controls.Add(btnCreateClear);
            groupBoxUserCreate.Controls.Add(btnCreate);
            groupBoxUserCreate.Controls.Add(cmbCreateRole);
            groupBoxUserCreate.Controls.Add(lblCreateRole);
            groupBoxUserCreate.Controls.Add(txtCreatePassword);
            groupBoxUserCreate.Controls.Add(lblCreatePassword);
            groupBoxUserCreate.Controls.Add(txtCreateLogin);
            groupBoxUserCreate.Controls.Add(lblCreateLogin);
            groupBoxUserCreate.Controls.Add(txtCreateFname);
            groupBoxUserCreate.Controls.Add(lblCreateFname);
            groupBoxUserCreate.Controls.Add(txtCreateSname);
            groupBoxUserCreate.Controls.Add(lblCreateSname);
            groupBoxUserCreate.Controls.Add(txtCreateName);
            groupBoxUserCreate.Controls.Add(lblCreateName);
            groupBoxUserCreate.Location = new Point(10, 224);
            groupBoxUserCreate.Margin = new Padding(3, 2, 3, 2);
            groupBoxUserCreate.Name = "groupBoxUserCreate";
            groupBoxUserCreate.Padding = new Padding(3, 2, 3, 2);
            groupBoxUserCreate.Size = new Size(446, 191);
            groupBoxUserCreate.TabIndex = 1;
            groupBoxUserCreate.TabStop = false;
            groupBoxUserCreate.Text = "Добавление пользователя";
            // 
            // btnCreateClear
            // 
            btnCreateClear.Location = new Point(14, 152);
            btnCreateClear.Margin = new Padding(3, 2, 3, 2);
            btnCreateClear.Name = "btnCreateClear";
            btnCreateClear.Size = new Size(147, 27);
            btnCreateClear.TabIndex = 13;
            btnCreateClear.Text = "Очистить";
            btnCreateClear.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(285, 152);
            btnCreate.Margin = new Padding(3, 2, 3, 2);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(147, 27);
            btnCreate.TabIndex = 12;
            btnCreate.Text = "Добавить";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // cmbCreateRole
            // 
            cmbCreateRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCreateRole.FormattingEnabled = true;
            cmbCreateRole.Location = new Point(295, 107);
            cmbCreateRole.Margin = new Padding(3, 2, 3, 2);
            cmbCreateRole.Name = "cmbCreateRole";
            cmbCreateRole.Size = new Size(138, 23);
            cmbCreateRole.TabIndex = 11;
            // 
            // lblCreateRole
            // 
            lblCreateRole.AutoSize = true;
            lblCreateRole.Location = new Point(295, 90);
            lblCreateRole.Name = "lblCreateRole";
            lblCreateRole.Size = new Size(34, 15);
            lblCreateRole.TabIndex = 10;
            lblCreateRole.Text = "Роль";
            // 
            // txtCreatePassword
            // 
            txtCreatePassword.Location = new Point(152, 107);
            txtCreatePassword.Margin = new Padding(3, 2, 3, 2);
            txtCreatePassword.Name = "txtCreatePassword";
            txtCreatePassword.PasswordChar = '*';
            txtCreatePassword.Size = new Size(127, 23);
            txtCreatePassword.TabIndex = 9;
            // 
            // lblCreatePassword
            // 
            lblCreatePassword.AutoSize = true;
            lblCreatePassword.Location = new Point(152, 90);
            lblCreatePassword.Name = "lblCreatePassword";
            lblCreatePassword.Size = new Size(49, 15);
            lblCreatePassword.TabIndex = 8;
            lblCreatePassword.Text = "Пароль";
            // 
            // txtCreateLogin
            // 
            txtCreateLogin.Location = new Point(14, 107);
            txtCreateLogin.Margin = new Padding(3, 2, 3, 2);
            txtCreateLogin.Name = "txtCreateLogin";
            txtCreateLogin.Size = new Size(127, 23);
            txtCreateLogin.TabIndex = 7;
            // 
            // lblCreateLogin
            // 
            lblCreateLogin.AutoSize = true;
            lblCreateLogin.Location = new Point(14, 90);
            lblCreateLogin.Name = "lblCreateLogin";
            lblCreateLogin.Size = new Size(41, 15);
            lblCreateLogin.TabIndex = 6;
            lblCreateLogin.Text = "Логин";
            // 
            // txtCreateFname
            // 
            txtCreateFname.Location = new Point(295, 56);
            txtCreateFname.Margin = new Padding(3, 2, 3, 2);
            txtCreateFname.Name = "txtCreateFname";
            txtCreateFname.Size = new Size(138, 23);
            txtCreateFname.TabIndex = 5;
            // 
            // lblCreateFname
            // 
            lblCreateFname.AutoSize = true;
            lblCreateFname.Location = new Point(295, 38);
            lblCreateFname.Name = "lblCreateFname";
            lblCreateFname.Size = new Size(58, 15);
            lblCreateFname.TabIndex = 4;
            lblCreateFname.Text = "Отчество";
            // 
            // txtCreateSname
            // 
            txtCreateSname.Location = new Point(152, 56);
            txtCreateSname.Margin = new Padding(3, 2, 3, 2);
            txtCreateSname.Name = "txtCreateSname";
            txtCreateSname.Size = new Size(127, 23);
            txtCreateSname.TabIndex = 3;
            // 
            // lblCreateSname
            // 
            lblCreateSname.AutoSize = true;
            lblCreateSname.Location = new Point(152, 38);
            lblCreateSname.Name = "lblCreateSname";
            lblCreateSname.Size = new Size(58, 15);
            lblCreateSname.TabIndex = 2;
            lblCreateSname.Text = "Фамилия";
            // 
            // txtCreateName
            // 
            txtCreateName.Location = new Point(14, 56);
            txtCreateName.Margin = new Padding(3, 2, 3, 2);
            txtCreateName.Name = "txtCreateName";
            txtCreateName.Size = new Size(127, 23);
            txtCreateName.TabIndex = 1;
            // 
            // lblCreateName
            // 
            lblCreateName.AutoSize = true;
            lblCreateName.Location = new Point(14, 38);
            lblCreateName.Name = "lblCreateName";
            lblCreateName.Size = new Size(31, 15);
            lblCreateName.TabIndex = 0;
            lblCreateName.Text = "Имя";
            // 
            // groupBoxUserUpdate
            // 
            groupBoxUserUpdate.Controls.Add(lblUpdateHint);
            groupBoxUserUpdate.Controls.Add(lblUpdateSelected);
            groupBoxUserUpdate.Controls.Add(btnUpdateClear);
            groupBoxUserUpdate.Controls.Add(btnUpdate);
            groupBoxUserUpdate.Controls.Add(cmbUpdateRole);
            groupBoxUserUpdate.Controls.Add(lblUpdateRole);
            groupBoxUserUpdate.Controls.Add(txtUpdatePassword);
            groupBoxUserUpdate.Controls.Add(lblUpdatePassword);
            groupBoxUserUpdate.Controls.Add(txtUpdateLogin);
            groupBoxUserUpdate.Controls.Add(lblUpdateLogin);
            groupBoxUserUpdate.Controls.Add(txtUpdateFname);
            groupBoxUserUpdate.Controls.Add(lblUpdateFname);
            groupBoxUserUpdate.Controls.Add(txtUpdateSname);
            groupBoxUserUpdate.Controls.Add(lblUpdateSname);
            groupBoxUserUpdate.Controls.Add(txtUpdateName);
            groupBoxUserUpdate.Controls.Add(lblUpdateName);
            groupBoxUserUpdate.Location = new Point(470, 224);
            groupBoxUserUpdate.Margin = new Padding(3, 2, 3, 2);
            groupBoxUserUpdate.Name = "groupBoxUserUpdate";
            groupBoxUserUpdate.Padding = new Padding(3, 2, 3, 2);
            groupBoxUserUpdate.Size = new Size(446, 191);
            groupBoxUserUpdate.TabIndex = 2;
            groupBoxUserUpdate.TabStop = false;
            groupBoxUserUpdate.Text = "Изменение пользователя";
            // 
            // lblUpdateHint
            // 
            lblUpdateHint.AutoSize = true;
            lblUpdateHint.ForeColor = SystemColors.GrayText;
            lblUpdateHint.Location = new Point(14, 136);
            lblUpdateHint.Name = "lblUpdateHint";
            lblUpdateHint.Size = new Size(259, 15);
            lblUpdateHint.TabIndex = 15;
            lblUpdateHint.Text = "Если пароль пустой, в базе останется старый.";
            // 
            // lblUpdateSelected
            // 
            lblUpdateSelected.AutoSize = true;
            lblUpdateSelected.Location = new Point(14, 18);
            lblUpdateSelected.Name = "lblUpdateSelected";
            lblUpdateSelected.Size = new Size(215, 15);
            lblUpdateSelected.TabIndex = 14;
            lblUpdateSelected.Text = "Выбранный пользователь: не выбран";
            // 
            // btnUpdateClear
            // 
            btnUpdateClear.Location = new Point(14, 152);
            btnUpdateClear.Margin = new Padding(3, 2, 3, 2);
            btnUpdateClear.Name = "btnUpdateClear";
            btnUpdateClear.Size = new Size(147, 27);
            btnUpdateClear.TabIndex = 13;
            btnUpdateClear.Text = "Сбросить выбор";
            btnUpdateClear.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(285, 152);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(147, 27);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Изменить";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // cmbUpdateRole
            // 
            cmbUpdateRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUpdateRole.FormattingEnabled = true;
            cmbUpdateRole.Location = new Point(295, 107);
            cmbUpdateRole.Margin = new Padding(3, 2, 3, 2);
            cmbUpdateRole.Name = "cmbUpdateRole";
            cmbUpdateRole.Size = new Size(138, 23);
            cmbUpdateRole.TabIndex = 11;
            // 
            // lblUpdateRole
            // 
            lblUpdateRole.AutoSize = true;
            lblUpdateRole.Location = new Point(295, 90);
            lblUpdateRole.Name = "lblUpdateRole";
            lblUpdateRole.Size = new Size(34, 15);
            lblUpdateRole.TabIndex = 10;
            lblUpdateRole.Text = "Роль";
            // 
            // txtUpdatePassword
            // 
            txtUpdatePassword.Location = new Point(152, 107);
            txtUpdatePassword.Margin = new Padding(3, 2, 3, 2);
            txtUpdatePassword.Name = "txtUpdatePassword";
            txtUpdatePassword.PasswordChar = '*';
            txtUpdatePassword.Size = new Size(127, 23);
            txtUpdatePassword.TabIndex = 9;
            // 
            // lblUpdatePassword
            // 
            lblUpdatePassword.AutoSize = true;
            lblUpdatePassword.Location = new Point(152, 90);
            lblUpdatePassword.Name = "lblUpdatePassword";
            lblUpdatePassword.Size = new Size(49, 15);
            lblUpdatePassword.TabIndex = 8;
            lblUpdatePassword.Text = "Пароль";
            // 
            // txtUpdateLogin
            // 
            txtUpdateLogin.Location = new Point(14, 107);
            txtUpdateLogin.Margin = new Padding(3, 2, 3, 2);
            txtUpdateLogin.Name = "txtUpdateLogin";
            txtUpdateLogin.Size = new Size(127, 23);
            txtUpdateLogin.TabIndex = 7;
            // 
            // lblUpdateLogin
            // 
            lblUpdateLogin.AutoSize = true;
            lblUpdateLogin.Location = new Point(14, 90);
            lblUpdateLogin.Name = "lblUpdateLogin";
            lblUpdateLogin.Size = new Size(41, 15);
            lblUpdateLogin.TabIndex = 6;
            lblUpdateLogin.Text = "Логин";
            // 
            // txtUpdateFname
            // 
            txtUpdateFname.Location = new Point(295, 56);
            txtUpdateFname.Margin = new Padding(3, 2, 3, 2);
            txtUpdateFname.Name = "txtUpdateFname";
            txtUpdateFname.Size = new Size(138, 23);
            txtUpdateFname.TabIndex = 5;
            // 
            // lblUpdateFname
            // 
            lblUpdateFname.AutoSize = true;
            lblUpdateFname.Location = new Point(295, 38);
            lblUpdateFname.Name = "lblUpdateFname";
            lblUpdateFname.Size = new Size(58, 15);
            lblUpdateFname.TabIndex = 4;
            lblUpdateFname.Text = "Отчество";
            // 
            // txtUpdateSname
            // 
            txtUpdateSname.Location = new Point(152, 56);
            txtUpdateSname.Margin = new Padding(3, 2, 3, 2);
            txtUpdateSname.Name = "txtUpdateSname";
            txtUpdateSname.Size = new Size(127, 23);
            txtUpdateSname.TabIndex = 3;
            // 
            // lblUpdateSname
            // 
            lblUpdateSname.AutoSize = true;
            lblUpdateSname.Location = new Point(152, 38);
            lblUpdateSname.Name = "lblUpdateSname";
            lblUpdateSname.Size = new Size(58, 15);
            lblUpdateSname.TabIndex = 2;
            lblUpdateSname.Text = "Фамилия";
            // 
            // txtUpdateName
            // 
            txtUpdateName.Location = new Point(14, 56);
            txtUpdateName.Margin = new Padding(3, 2, 3, 2);
            txtUpdateName.Name = "txtUpdateName";
            txtUpdateName.Size = new Size(127, 23);
            txtUpdateName.TabIndex = 1;
            // 
            // lblUpdateName
            // 
            lblUpdateName.AutoSize = true;
            lblUpdateName.Location = new Point(14, 38);
            lblUpdateName.Name = "lblUpdateName";
            lblUpdateName.Size = new Size(31, 15);
            lblUpdateName.TabIndex = 0;
            lblUpdateName.Text = "Имя";
            // 
            // groupBoxUserDelete
            // 
            groupBoxUserDelete.Controls.Add(lblDeleteHint);
            groupBoxUserDelete.Controls.Add(lblDeleteSelected);
            groupBoxUserDelete.Controls.Add(btnDelete);
            groupBoxUserDelete.Location = new Point(929, 224);
            groupBoxUserDelete.Margin = new Padding(3, 2, 3, 2);
            groupBoxUserDelete.Name = "groupBoxUserDelete";
            groupBoxUserDelete.Padding = new Padding(3, 2, 3, 2);
            groupBoxUserDelete.Size = new Size(446, 191);
            groupBoxUserDelete.TabIndex = 3;
            groupBoxUserDelete.TabStop = false;
            groupBoxUserDelete.Text = "Удаление пользователя";
            // 
            // lblDeleteHint
            // 
            lblDeleteHint.AutoSize = true;
            lblDeleteHint.ForeColor = SystemColors.GrayText;
            lblDeleteHint.Location = new Point(14, 62);
            lblDeleteHint.Name = "lblDeleteHint";
            lblDeleteHint.Size = new Size(253, 15);
            lblDeleteHint.TabIndex = 2;
            lblDeleteHint.Text = "Для удаления сначала выбери строку выше.";
            // 
            // lblDeleteSelected
            // 
            lblDeleteSelected.AutoSize = true;
            lblDeleteSelected.Location = new Point(14, 38);
            lblDeleteSelected.Name = "lblDeleteSelected";
            lblDeleteSelected.Size = new Size(224, 15);
            lblDeleteSelected.TabIndex = 1;
            lblDeleteSelected.Text = "Пользователь для удаления: не выбран";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(14, 91);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(418, 32);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "Удалить выбранного пользователя";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // groupBoxCategories
            // 
            groupBoxCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxCategories.Controls.Add(lblCategorySelected);
            groupBoxCategories.Controls.Add(btnCategoryDelete);
            groupBoxCategories.Controls.Add(btnCategoryUpdate);
            groupBoxCategories.Controls.Add(btnCategoryAdd);
            groupBoxCategories.Controls.Add(btnCategoryClear);
            groupBoxCategories.Controls.Add(btnCategoryRefresh);
            groupBoxCategories.Controls.Add(txtCategoryCode);
            groupBoxCategories.Controls.Add(lblCategoryCode);
            groupBoxCategories.Controls.Add(dgvCategories);
            groupBoxCategories.Location = new Point(10, 422);
            groupBoxCategories.Margin = new Padding(3, 2, 3, 2);
            groupBoxCategories.Name = "groupBoxCategories";
            groupBoxCategories.Padding = new Padding(3, 2, 3, 2);
            groupBoxCategories.Size = new Size(368, 574);
            groupBoxCategories.TabIndex = 4;
            groupBoxCategories.TabStop = false;
            groupBoxCategories.Text = "Управление категориями";
            // 
            // lblCategorySelected
            // 
            lblCategorySelected.AutoSize = true;
            lblCategorySelected.Location = new Point(14, 180);
            lblCategorySelected.Name = "lblCategorySelected";
            lblCategorySelected.Size = new Size(197, 15);
            lblCategorySelected.TabIndex = 8;
            lblCategorySelected.Text = "Выбранная категория: не выбрана";
            // 
            // btnCategoryDelete
            // 
            btnCategoryDelete.Location = new Point(246, 249);
            btnCategoryDelete.Margin = new Padding(3, 2, 3, 2);
            btnCategoryDelete.Name = "btnCategoryDelete";
            btnCategoryDelete.Size = new Size(108, 27);
            btnCategoryDelete.TabIndex = 7;
            btnCategoryDelete.Text = "Удалить";
            btnCategoryDelete.UseVisualStyleBackColor = true;
            // 
            // btnCategoryUpdate
            // 
            btnCategoryUpdate.Location = new Point(133, 249);
            btnCategoryUpdate.Margin = new Padding(3, 2, 3, 2);
            btnCategoryUpdate.Name = "btnCategoryUpdate";
            btnCategoryUpdate.Size = new Size(108, 27);
            btnCategoryUpdate.TabIndex = 6;
            btnCategoryUpdate.Text = "Изменить";
            btnCategoryUpdate.UseVisualStyleBackColor = true;
            // 
            // btnCategoryAdd
            // 
            btnCategoryAdd.Location = new Point(20, 249);
            btnCategoryAdd.Margin = new Padding(3, 2, 3, 2);
            btnCategoryAdd.Name = "btnCategoryAdd";
            btnCategoryAdd.Size = new Size(108, 27);
            btnCategoryAdd.TabIndex = 5;
            btnCategoryAdd.Text = "Добавить";
            btnCategoryAdd.UseVisualStyleBackColor = true;
            // 
            // btnCategoryClear
            // 
            btnCategoryClear.Location = new Point(246, 208);
            btnCategoryClear.Margin = new Padding(3, 2, 3, 2);
            btnCategoryClear.Name = "btnCategoryClear";
            btnCategoryClear.Size = new Size(108, 27);
            btnCategoryClear.TabIndex = 4;
            btnCategoryClear.Text = "Очистить";
            btnCategoryClear.UseVisualStyleBackColor = true;
            // 
            // btnCategoryRefresh
            // 
            btnCategoryRefresh.Location = new Point(133, 208);
            btnCategoryRefresh.Margin = new Padding(3, 2, 3, 2);
            btnCategoryRefresh.Name = "btnCategoryRefresh";
            btnCategoryRefresh.Size = new Size(108, 27);
            btnCategoryRefresh.TabIndex = 3;
            btnCategoryRefresh.Text = "Обновить";
            btnCategoryRefresh.UseVisualStyleBackColor = true;
            // 
            // txtCategoryCode
            // 
            txtCategoryCode.Location = new Point(14, 214);
            txtCategoryCode.Margin = new Padding(3, 2, 3, 2);
            txtCategoryCode.Name = "txtCategoryCode";
            txtCategoryCode.Size = new Size(114, 23);
            txtCategoryCode.TabIndex = 2;
            // 
            // lblCategoryCode
            // 
            lblCategoryCode.AutoSize = true;
            lblCategoryCode.Location = new Point(14, 197);
            lblCategoryCode.Name = "lblCategoryCode";
            lblCategoryCode.Size = new Size(86, 15);
            lblCategoryCode.TabIndex = 1;
            lblCategoryCode.Text = "Код категории";
            // 
            // dgvCategories
            // 
            dgvCategories.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(14, 20);
            dgvCategories.Margin = new Padding(3, 2, 3, 2);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.RowTemplate.Height = 29;
            dgvCategories.Size = new Size(340, 150);
            dgvCategories.TabIndex = 0;
            // 
            // groupBoxProducts
            // 
            groupBoxProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxProducts.Controls.Add(lblProductSelected);
            groupBoxProducts.Controls.Add(btnProductDelete);
            groupBoxProducts.Controls.Add(btnProductUpdate);
            groupBoxProducts.Controls.Add(btnProductAdd);
            groupBoxProducts.Controls.Add(btnProductClear);
            groupBoxProducts.Controls.Add(btnProductRefresh);
            groupBoxProducts.Controls.Add(cmbProductCategory);
            groupBoxProducts.Controls.Add(lblProductCategory);
            groupBoxProducts.Controls.Add(txtProductSize);
            groupBoxProducts.Controls.Add(lblProductSize);
            groupBoxProducts.Controls.Add(txtProductWeight);
            groupBoxProducts.Controls.Add(lblProductWeight);
            groupBoxProducts.Controls.Add(txtProductDescription);
            groupBoxProducts.Controls.Add(lblProductDescription);
            groupBoxProducts.Controls.Add(txtProductCode);
            groupBoxProducts.Controls.Add(lblProductCode);
            groupBoxProducts.Controls.Add(dgvProducts);
            groupBoxProducts.Location = new Point(384, 422);
            groupBoxProducts.Margin = new Padding(3, 2, 3, 2);
            groupBoxProducts.Name = "groupBoxProducts";
            groupBoxProducts.Padding = new Padding(3, 2, 3, 2);
            groupBoxProducts.Size = new Size(587, 266);
            groupBoxProducts.TabIndex = 0;
            groupBoxProducts.TabStop = false;
            groupBoxProducts.Text = "Товары";
            // 
            // lblProductSelected
            // 
            lblProductSelected.AutoSize = true;
            lblProductSelected.Location = new Point(14, 153);
            lblProductSelected.Name = "lblProductSelected";
            lblProductSelected.Size = new Size(171, 15);
            lblProductSelected.TabIndex = 16;
            lblProductSelected.Text = "Выбранный товар: не выбран";
            // 
            // btnProductDelete
            // 
            btnProductDelete.Location = new Point(452, 220);
            btnProductDelete.Margin = new Padding(3, 2, 3, 2);
            btnProductDelete.Name = "btnProductDelete";
            btnProductDelete.Size = new Size(107, 26);
            btnProductDelete.TabIndex = 15;
            btnProductDelete.Text = "Удалить";
            btnProductDelete.UseVisualStyleBackColor = true;
            // 
            // btnProductUpdate
            // 
            btnProductUpdate.Location = new Point(340, 220);
            btnProductUpdate.Margin = new Padding(3, 2, 3, 2);
            btnProductUpdate.Name = "btnProductUpdate";
            btnProductUpdate.Size = new Size(107, 26);
            btnProductUpdate.TabIndex = 14;
            btnProductUpdate.Text = "Изменить";
            btnProductUpdate.UseVisualStyleBackColor = true;
            // 
            // btnProductAdd
            // 
            btnProductAdd.Location = new Point(228, 220);
            btnProductAdd.Margin = new Padding(3, 2, 3, 2);
            btnProductAdd.Name = "btnProductAdd";
            btnProductAdd.Size = new Size(107, 26);
            btnProductAdd.TabIndex = 13;
            btnProductAdd.Text = "Добавить";
            btnProductAdd.UseVisualStyleBackColor = true;
            // 
            // btnProductClear
            // 
            btnProductClear.Location = new Point(116, 220);
            btnProductClear.Margin = new Padding(3, 2, 3, 2);
            btnProductClear.Name = "btnProductClear";
            btnProductClear.Size = new Size(107, 26);
            btnProductClear.TabIndex = 12;
            btnProductClear.Text = "Очистить";
            btnProductClear.UseVisualStyleBackColor = true;
            // 
            // btnProductRefresh
            // 
            btnProductRefresh.Location = new Point(14, 220);
            btnProductRefresh.Margin = new Padding(3, 2, 3, 2);
            btnProductRefresh.Name = "btnProductRefresh";
            btnProductRefresh.Size = new Size(96, 26);
            btnProductRefresh.TabIndex = 11;
            btnProductRefresh.Text = "Обновить";
            btnProductRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbProductCategory
            // 
            cmbProductCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductCategory.FormattingEnabled = true;
            cmbProductCategory.Location = new Point(363, 184);
            cmbProductCategory.Margin = new Padding(3, 2, 3, 2);
            cmbProductCategory.Name = "cmbProductCategory";
            cmbProductCategory.Size = new Size(196, 23);
            cmbProductCategory.TabIndex = 10;
            // 
            // lblProductCategory
            // 
            lblProductCategory.AutoSize = true;
            lblProductCategory.Location = new Point(363, 166);
            lblProductCategory.Name = "lblProductCategory";
            lblProductCategory.Size = new Size(63, 15);
            lblProductCategory.TabIndex = 9;
            lblProductCategory.Text = "Категория";
            // 
            // txtProductSize
            // 
            txtProductSize.Location = new Point(256, 184);
            txtProductSize.Margin = new Padding(3, 2, 3, 2);
            txtProductSize.Name = "txtProductSize";
            txtProductSize.Size = new Size(93, 23);
            txtProductSize.TabIndex = 8;
            // 
            // lblProductSize
            // 
            lblProductSize.AutoSize = true;
            lblProductSize.Location = new Point(256, 167);
            lblProductSize.Name = "lblProductSize";
            lblProductSize.Size = new Size(47, 15);
            lblProductSize.TabIndex = 7;
            lblProductSize.Text = "Размер";
            // 
            // txtProductWeight
            // 
            txtProductWeight.Location = new Point(154, 184);
            txtProductWeight.Margin = new Padding(3, 2, 3, 2);
            txtProductWeight.Name = "txtProductWeight";
            txtProductWeight.Size = new Size(88, 23);
            txtProductWeight.TabIndex = 6;
            // 
            // lblProductWeight
            // 
            lblProductWeight.AutoSize = true;
            lblProductWeight.Location = new Point(154, 167);
            lblProductWeight.Name = "lblProductWeight";
            lblProductWeight.Size = new Size(26, 15);
            lblProductWeight.TabIndex = 5;
            lblProductWeight.Text = "Вес";
            // 
            // txtProductDescription
            // 
            txtProductDescription.Location = new Point(14, 184);
            txtProductDescription.Margin = new Padding(3, 2, 3, 2);
            txtProductDescription.Name = "txtProductDescription";
            txtProductDescription.Size = new Size(126, 23);
            txtProductDescription.TabIndex = 4;
            // 
            // lblProductDescription
            // 
            lblProductDescription.AutoSize = true;
            lblProductDescription.Location = new Point(14, 167);
            lblProductDescription.Name = "lblProductDescription";
            lblProductDescription.Size = new Size(62, 15);
            lblProductDescription.TabIndex = 3;
            lblProductDescription.Text = "Описание";
            // 
            // txtProductCode
            // 
            txtProductCode.Location = new Point(452, 148);
            txtProductCode.Margin = new Padding(3, 2, 3, 2);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.Size = new Size(107, 23);
            txtProductCode.TabIndex = 2;
            // 
            // lblProductCode
            // 
            lblProductCode.AutoSize = true;
            lblProductCode.Location = new Point(452, 130);
            lblProductCode.Name = "lblProductCode";
            lblProductCode.Size = new Size(67, 15);
            lblProductCode.TabIndex = 1;
            lblProductCode.Text = "Код товара";
            // 
            // dgvProducts
            // 
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(14, 20);
            dgvProducts.Margin = new Padding(3, 2, 3, 2);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 29;
            dgvProducts.Size = new Size(559, 101);
            dgvProducts.TabIndex = 0;
            // 
            // groupBoxReports
            // 
            groupBoxReports.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBoxReports.Controls.Add(dgvReports);
            groupBoxReports.Controls.Add(btnReportShow);
            groupBoxReports.Controls.Add(cmbReportProduct);
            groupBoxReports.Controls.Add(lblReportProduct);
            groupBoxReports.Controls.Add(cmbReportClient);
            groupBoxReports.Controls.Add(lblReportClient);
            groupBoxReports.Controls.Add(dtpDateTo);
            groupBoxReports.Controls.Add(lblDateTo);
            groupBoxReports.Controls.Add(dtpDateFrom);
            groupBoxReports.Controls.Add(lblDateFrom);
            groupBoxReports.Controls.Add(cmbReportType);
            groupBoxReports.Controls.Add(lblReportType);
            groupBoxReports.Location = new Point(985, 422);
            groupBoxReports.Margin = new Padding(3, 2, 3, 2);
            groupBoxReports.Name = "groupBoxReports";
            groupBoxReports.Padding = new Padding(3, 2, 3, 2);
            groupBoxReports.Size = new Size(647, 550);
            groupBoxReports.TabIndex = 1;
            groupBoxReports.TabStop = false;
            groupBoxReports.Text = "Просмотр отчетов";
            // 
            // dgvReports
            // 
            dgvReports.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Location = new Point(14, 208);
            dgvReports.Margin = new Padding(3, 2, 3, 2);
            dgvReports.Name = "dgvReports";
            dgvReports.RowHeadersWidth = 51;
            dgvReports.RowTemplate.Height = 29;
            dgvReports.Size = new Size(619, 330);
            dgvReports.TabIndex = 11;
            // 
            // btnReportShow
            // 
            btnReportShow.Location = new Point(246, 131);
            btnReportShow.Margin = new Padding(3, 2, 3, 2);
            btnReportShow.Name = "btnReportShow";
            btnReportShow.Size = new Size(108, 22);
            btnReportShow.TabIndex = 10;
            btnReportShow.Text = "Показать";
            btnReportShow.UseVisualStyleBackColor = true;
            btnReportShow.Click += btnReportShow_Click_1;
            // 
            // cmbReportProduct
            // 
            cmbReportProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportProduct.FormattingEnabled = true;
            cmbReportProduct.Location = new Point(186, 83);
            cmbReportProduct.Margin = new Padding(3, 2, 3, 2);
            cmbReportProduct.Name = "cmbReportProduct";
            cmbReportProduct.Size = new Size(170, 23);
            cmbReportProduct.TabIndex = 9;
            // 
            // lblReportProduct
            // 
            lblReportProduct.AutoSize = true;
            lblReportProduct.Location = new Point(186, 66);
            lblReportProduct.Name = "lblReportProduct";
            lblReportProduct.Size = new Size(39, 15);
            lblReportProduct.TabIndex = 8;
            lblReportProduct.Text = "Товар";
            // 
            // cmbReportClient
            // 
            cmbReportClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportClient.FormattingEnabled = true;
            cmbReportClient.Location = new Point(14, 83);
            cmbReportClient.Margin = new Padding(3, 2, 3, 2);
            cmbReportClient.Name = "cmbReportClient";
            cmbReportClient.Size = new Size(159, 23);
            cmbReportClient.TabIndex = 7;
            // 
            // lblReportClient
            // 
            lblReportClient.AutoSize = true;
            lblReportClient.Location = new Point(14, 66);
            lblReportClient.Name = "lblReportClient";
            lblReportClient.Size = new Size(46, 15);
            lblReportClient.TabIndex = 6;
            lblReportClient.Text = "Клиент";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Format = DateTimePickerFormat.Short;
            dtpDateTo.Location = new Point(186, 38);
            dtpDateTo.Margin = new Padding(3, 2, 3, 2);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(170, 23);
            dtpDateTo.TabIndex = 5;
            // 
            // lblDateTo
            // 
            lblDateTo.AutoSize = true;
            lblDateTo.Location = new Point(186, 21);
            lblDateTo.Name = "lblDateTo";
            lblDateTo.Size = new Size(49, 15);
            lblDateTo.TabIndex = 4;
            lblDateTo.Text = "Дата по";
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Format = DateTimePickerFormat.Short;
            dtpDateFrom.Location = new Point(14, 38);
            dtpDateFrom.Margin = new Padding(3, 2, 3, 2);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(159, 23);
            dtpDateFrom.TabIndex = 3;
            // 
            // lblDateFrom
            // 
            lblDateFrom.AutoSize = true;
            lblDateFrom.Location = new Point(14, 21);
            lblDateFrom.Name = "lblDateFrom";
            lblDateFrom.Size = new Size(41, 15);
            lblDateFrom.TabIndex = 2;
            lblDateFrom.Text = "Дата с";
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.FormattingEnabled = true;
            cmbReportType.Location = new Point(14, 130);
            cmbReportType.Margin = new Padding(3, 2, 3, 2);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(218, 23);
            cmbReportType.TabIndex = 1;
            // 
            // lblReportType
            // 
            lblReportType.AutoSize = true;
            lblReportType.Location = new Point(14, 112);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(66, 15);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "Тип отчета";
            // 
            // Administrator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1819, 1011);
            Controls.Add(groupBoxReports);
            Controls.Add(groupBoxProducts);
            Controls.Add(groupBoxCategories);
            Controls.Add(groupBoxUserDelete);
            Controls.Add(groupBoxUserUpdate);
            Controls.Add(groupBoxUserCreate);
            Controls.Add(groupBoxUsersRead);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1404, 766);
            Name = "Administrator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Administrator";
            groupBoxUsersRead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            groupBoxUserCreate.ResumeLayout(false);
            groupBoxUserCreate.PerformLayout();
            groupBoxUserUpdate.ResumeLayout(false);
            groupBoxUserUpdate.PerformLayout();
            groupBoxUserDelete.ResumeLayout(false);
            groupBoxUserDelete.PerformLayout();
            groupBoxCategories.ResumeLayout(false);
            groupBoxCategories.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            groupBoxProducts.ResumeLayout(false);
            groupBoxProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            groupBoxReports.ResumeLayout(false);
            groupBoxReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxUsersRead;
        private Button btnRefresh;
        private DataGridView dgvUsers;

        private GroupBox groupBoxUserCreate;
        private Button btnCreateClear;
        private Button btnCreate;
        private ComboBox cmbCreateRole;
        private Label lblCreateRole;
        private TextBox txtCreatePassword;
        private Label lblCreatePassword;
        private TextBox txtCreateLogin;
        private Label lblCreateLogin;
        private TextBox txtCreateFname;
        private Label lblCreateFname;
        private TextBox txtCreateSname;
        private Label lblCreateSname;
        private TextBox txtCreateName;
        private Label lblCreateName;

        private GroupBox groupBoxUserUpdate;
        private Label lblUpdateHint;
        private Label lblUpdateSelected;
        private Button btnUpdateClear;
        private Button btnUpdate;
        private ComboBox cmbUpdateRole;
        private Label lblUpdateRole;
        private TextBox txtUpdatePassword;
        private Label lblUpdatePassword;
        private TextBox txtUpdateLogin;
        private Label lblUpdateLogin;
        private TextBox txtUpdateFname;
        private Label lblUpdateFname;
        private TextBox txtUpdateSname;
        private Label lblUpdateSname;
        private TextBox txtUpdateName;
        private Label lblUpdateName;

        private GroupBox groupBoxUserDelete;
        private Label lblDeleteHint;
        private Label lblDeleteSelected;
        private Button btnDelete;

        private GroupBox groupBoxCategories;
        private Label lblCategorySelected;
        private Button btnCategoryDelete;
        private Button btnCategoryUpdate;
        private Button btnCategoryAdd;
        private Button btnCategoryClear;
        private Button btnCategoryRefresh;
        private TextBox txtCategoryCode;
        private Label lblCategoryCode;
        private DataGridView dgvCategories;
        private GroupBox groupBoxProducts;
        private Label lblProductSelected;
        private Button btnProductDelete;
        private Button btnProductUpdate;
        private Button btnProductAdd;
        private Button btnProductClear;
        private Button btnProductRefresh;
        private ComboBox cmbProductCategory;
        private Label lblProductCategory;
        private TextBox txtProductSize;
        private Label lblProductSize;
        private TextBox txtProductWeight;
        private Label lblProductWeight;
        private TextBox txtProductDescription;
        private Label lblProductDescription;
        private TextBox txtProductCode;
        private Label lblProductCode;
        private DataGridView dgvProducts;
        private GroupBox groupBoxReports;
        private DataGridView dgvReports;
        private Button btnReportShow;
        private ComboBox cmbReportProduct;
        private Label lblReportProduct;
        private ComboBox cmbReportClient;
        private Label lblReportClient;
        private DateTimePicker dtpDateTo;
        private Label lblDateTo;
        private DateTimePicker dtpDateFrom;
        private Label lblDateFrom;
        private ComboBox cmbReportType;
        private Label lblReportType;
    }
}