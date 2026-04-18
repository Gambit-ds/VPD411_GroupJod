namespace TopClient_Storage
{
    partial class StorageEmployee
    {
        private System.ComponentModel.IContainer? components = null;

        private System.Windows.Forms.TabControl _tabMain = null!;

        private System.Windows.Forms.TabPage _tabStock = null!;
        private System.Windows.Forms.TabPage _tabReceipt = null!;
        private System.Windows.Forms.TabPage _tabIssue = null!;
        private System.Windows.Forms.TabPage _tabTransfer = null!;

        private System.Windows.Forms.DataGridView _dgvStock = null!;
        private System.Windows.Forms.ComboBox _cmbStockStoreFilter = null!;
        private System.Windows.Forms.ComboBox _cmbStockProductFilter = null!;
        private System.Windows.Forms.Button _btnStockRefresh = null!;

        private System.Windows.Forms.DataGridView _dgvOrderHeads = null!;
        private System.Windows.Forms.DataGridView _dgvOrderSpecs = null!;
        private System.Windows.Forms.ComboBox _cmbOrderStore = null!;
        private System.Windows.Forms.ComboBox _cmbOrderSupplier = null!;
        private System.Windows.Forms.ComboBox _cmbOrderProduct = null!;
        private System.Windows.Forms.NumericUpDown _nudOrderQuant = null!;
        private System.Windows.Forms.NumericUpDown _nudOrderPrice = null!;
        private System.Windows.Forms.Button _btnOrderHeadRefresh = null!;
        private System.Windows.Forms.Button _btnOrderHeadCreate = null!;
        private System.Windows.Forms.Button _btnOrderHeadAccept = null!;
        private System.Windows.Forms.Button _btnOrderSpecAdd = null!;
        private System.Windows.Forms.Button _btnOrderSpecDelete = null!;

        private System.Windows.Forms.DataGridView _dgvSaleHeads = null!;
        private System.Windows.Forms.DataGridView _dgvSaleSpecs = null!;
        private System.Windows.Forms.ComboBox _cmbSaleStore = null!;
        private System.Windows.Forms.ComboBox _cmbSaleClient = null!;
        private System.Windows.Forms.ComboBox _cmbSaleProduct = null!;
        private System.Windows.Forms.NumericUpDown _nudSaleQuant = null!;
        private System.Windows.Forms.Button _btnSaleHeadRefresh = null!;
        private System.Windows.Forms.Button _btnSaleHeadCreate = null!;
        private System.Windows.Forms.Button _btnSaleHeadProcess = null!;
        private System.Windows.Forms.Button _btnSaleSpecAdd = null!;
        private System.Windows.Forms.Button _btnSaleSpecDelete = null!;

        private System.Windows.Forms.DataGridView _dgvTransferHeads = null!;
        private System.Windows.Forms.DataGridView _dgvTransferSpecs = null!;
        private System.Windows.Forms.ComboBox _cmbTransferStoreOut = null!;
        private System.Windows.Forms.ComboBox _cmbTransferStoreIn = null!;
        private System.Windows.Forms.ComboBox _cmbTransferProduct = null!;
        private System.Windows.Forms.NumericUpDown _nudTransferQuant = null!;
        private System.Windows.Forms.Button _btnTransferHeadRefresh = null!;
        private System.Windows.Forms.Button _btnTransferHeadCreate = null!;
        private System.Windows.Forms.Button _btnTransferHeadSend = null!;
        private System.Windows.Forms.Button _btnTransferHeadAccept = null!;
        private System.Windows.Forms.Button _btnTransferSpecAdd = null!;
        private System.Windows.Forms.Button _btnTransferSpecDelete = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            System.Windows.Forms.Panel pnlStockTop;
            System.Windows.Forms.Label lblStockStore;
            System.Windows.Forms.Label lblStockProduct;

            System.Windows.Forms.Panel pnlOrderTop;
            System.Windows.Forms.Label lblOrderStore;
            System.Windows.Forms.Label lblOrderSupplier;
            System.Windows.Forms.Label lblOrderProduct;
            System.Windows.Forms.Label lblOrderQuant;
            System.Windows.Forms.Label lblOrderPrice;
            System.Windows.Forms.SplitContainer splitOrder;

            System.Windows.Forms.Panel pnlSaleTop;
            System.Windows.Forms.Label lblSaleStore;
            System.Windows.Forms.Label lblSaleClient;
            System.Windows.Forms.Label lblSaleProduct;
            System.Windows.Forms.Label lblSaleQuant;
            System.Windows.Forms.SplitContainer splitSale;

            System.Windows.Forms.Panel pnlTransferTop;
            System.Windows.Forms.Label lblTransferStoreOut;
            System.Windows.Forms.Label lblTransferStoreIn;
            System.Windows.Forms.Label lblTransferProduct;
            System.Windows.Forms.Label lblTransferQuant;
            System.Windows.Forms.SplitContainer splitTransfer;

            pnlStockTop = new System.Windows.Forms.Panel();
            lblStockStore = new System.Windows.Forms.Label();
            lblStockProduct = new System.Windows.Forms.Label();

            pnlOrderTop = new System.Windows.Forms.Panel();
            lblOrderStore = new System.Windows.Forms.Label();
            lblOrderSupplier = new System.Windows.Forms.Label();
            lblOrderProduct = new System.Windows.Forms.Label();
            lblOrderQuant = new System.Windows.Forms.Label();
            lblOrderPrice = new System.Windows.Forms.Label();
            splitOrder = new System.Windows.Forms.SplitContainer();

            pnlSaleTop = new System.Windows.Forms.Panel();
            lblSaleStore = new System.Windows.Forms.Label();
            lblSaleClient = new System.Windows.Forms.Label();
            lblSaleProduct = new System.Windows.Forms.Label();
            lblSaleQuant = new System.Windows.Forms.Label();
            splitSale = new System.Windows.Forms.SplitContainer();

            pnlTransferTop = new System.Windows.Forms.Panel();
            lblTransferStoreOut = new System.Windows.Forms.Label();
            lblTransferStoreIn = new System.Windows.Forms.Label();
            lblTransferProduct = new System.Windows.Forms.Label();
            lblTransferQuant = new System.Windows.Forms.Label();
            splitTransfer = new System.Windows.Forms.SplitContainer();

            this._tabMain = new System.Windows.Forms.TabControl();
            this._tabStock = new System.Windows.Forms.TabPage();
            this._tabReceipt = new System.Windows.Forms.TabPage();
            this._tabIssue = new System.Windows.Forms.TabPage();
            this._tabTransfer = new System.Windows.Forms.TabPage();

            this._cmbStockStoreFilter = new System.Windows.Forms.ComboBox();
            this._cmbStockProductFilter = new System.Windows.Forms.ComboBox();
            this._btnStockRefresh = new System.Windows.Forms.Button();
            this._dgvStock = new System.Windows.Forms.DataGridView();

            this._cmbOrderStore = new System.Windows.Forms.ComboBox();
            this._cmbOrderSupplier = new System.Windows.Forms.ComboBox();
            this._cmbOrderProduct = new System.Windows.Forms.ComboBox();
            this._nudOrderQuant = new System.Windows.Forms.NumericUpDown();
            this._nudOrderPrice = new System.Windows.Forms.NumericUpDown();
            this._btnOrderHeadRefresh = new System.Windows.Forms.Button();
            this._btnOrderHeadCreate = new System.Windows.Forms.Button();
            this._btnOrderHeadAccept = new System.Windows.Forms.Button();
            this._btnOrderSpecAdd = new System.Windows.Forms.Button();
            this._btnOrderSpecDelete = new System.Windows.Forms.Button();
            this._dgvOrderHeads = new System.Windows.Forms.DataGridView();
            this._dgvOrderSpecs = new System.Windows.Forms.DataGridView();

            this._cmbSaleStore = new System.Windows.Forms.ComboBox();
            this._cmbSaleClient = new System.Windows.Forms.ComboBox();
            this._cmbSaleProduct = new System.Windows.Forms.ComboBox();
            this._nudSaleQuant = new System.Windows.Forms.NumericUpDown();
            this._btnSaleHeadRefresh = new System.Windows.Forms.Button();
            this._btnSaleHeadCreate = new System.Windows.Forms.Button();
            this._btnSaleHeadProcess = new System.Windows.Forms.Button();
            this._btnSaleSpecAdd = new System.Windows.Forms.Button();
            this._btnSaleSpecDelete = new System.Windows.Forms.Button();
            this._dgvSaleHeads = new System.Windows.Forms.DataGridView();
            this._dgvSaleSpecs = new System.Windows.Forms.DataGridView();

            this._cmbTransferStoreOut = new System.Windows.Forms.ComboBox();
            this._cmbTransferStoreIn = new System.Windows.Forms.ComboBox();
            this._cmbTransferProduct = new System.Windows.Forms.ComboBox();
            this._nudTransferQuant = new System.Windows.Forms.NumericUpDown();
            this._btnTransferHeadRefresh = new System.Windows.Forms.Button();
            this._btnTransferHeadCreate = new System.Windows.Forms.Button();
            this._btnTransferHeadSend = new System.Windows.Forms.Button();
            this._btnTransferHeadAccept = new System.Windows.Forms.Button();
            this._btnTransferSpecAdd = new System.Windows.Forms.Button();
            this._btnTransferSpecDelete = new System.Windows.Forms.Button();
            this._dgvTransferHeads = new System.Windows.Forms.DataGridView();
            this._dgvTransferSpecs = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(splitOrder)).BeginInit();
            splitOrder.Panel1.SuspendLayout();
            splitOrder.Panel2.SuspendLayout();
            splitOrder.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(splitSale)).BeginInit();
            splitSale.Panel1.SuspendLayout();
            splitSale.Panel2.SuspendLayout();
            splitSale.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(splitTransfer)).BeginInit();
            splitTransfer.Panel1.SuspendLayout();
            splitTransfer.Panel2.SuspendLayout();
            splitTransfer.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this._nudOrderQuant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudOrderPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudSaleQuant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudTransferQuant)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this._dgvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSaleHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSaleSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTransferHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTransferSpecs)).BeginInit();

            this.SuspendLayout();

            // Основная форма
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 850);
            this.Name = "StorageEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Складской сотрудник";
            this.Load += new System.EventHandler(this.StorageEmployee_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageEmployee_FormClosing);

            // Главные вкладки
            this._tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabMain.Location = new System.Drawing.Point(0, 0);
            this._tabMain.Name = "_tabMain";
            this._tabMain.SelectedIndex = 0;
            this._tabMain.Size = new System.Drawing.Size(1400, 850);

            // =========================
            // ВКЛАДКА ОСТАТКИ
            // =========================
            this._tabStock.Location = new System.Drawing.Point(4, 24);
            this._tabStock.Name = "_tabStock";
            this._tabStock.Padding = new System.Windows.Forms.Padding(3);
            this._tabStock.Size = new System.Drawing.Size(1392, 822);
            this._tabStock.Text = "Остатки";
            this._tabStock.UseVisualStyleBackColor = true;

            pnlStockTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlStockTop.Height = 56;
            pnlStockTop.Name = "pnlStockTop";

            lblStockStore.AutoSize = true;
            lblStockStore.Location = new System.Drawing.Point(12, 18);
            lblStockStore.Text = "Склад";

            this._cmbStockStoreFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbStockStoreFilter.Location = new System.Drawing.Point(60, 14);
            this._cmbStockStoreFilter.Name = "_cmbStockStoreFilter";
            this._cmbStockStoreFilter.Size = new System.Drawing.Size(220, 23);

            lblStockProduct.AutoSize = true;
            lblStockProduct.Location = new System.Drawing.Point(300, 18);
            lblStockProduct.Text = "Товар";

            this._cmbStockProductFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbStockProductFilter.Location = new System.Drawing.Point(350, 14);
            this._cmbStockProductFilter.Name = "_cmbStockProductFilter";
            this._cmbStockProductFilter.Size = new System.Drawing.Size(260, 23);

            this._btnStockRefresh.Location = new System.Drawing.Point(630, 12);
            this._btnStockRefresh.Name = "_btnStockRefresh";
            this._btnStockRefresh.Size = new System.Drawing.Size(140, 28);
            this._btnStockRefresh.Text = "Обновить остатки";
            this._btnStockRefresh.UseVisualStyleBackColor = true;
            this._btnStockRefresh.Click += new System.EventHandler(this.btnStockRefresh_Click);

            this._dgvStock.AllowUserToAddRows = false;
            this._dgvStock.AllowUserToDeleteRows = false;
            this._dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvStock.Location = new System.Drawing.Point(3, 59);
            this._dgvStock.MultiSelect = false;
            this._dgvStock.Name = "_dgvStock";
            this._dgvStock.ReadOnly = true;
            this._dgvStock.RowHeadersVisible = false;
            this._dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            pnlStockTop.Controls.Add(lblStockStore);
            pnlStockTop.Controls.Add(this._cmbStockStoreFilter);
            pnlStockTop.Controls.Add(lblStockProduct);
            pnlStockTop.Controls.Add(this._cmbStockProductFilter);
            pnlStockTop.Controls.Add(this._btnStockRefresh);

            this._tabStock.Controls.Add(this._dgvStock);
            this._tabStock.Controls.Add(pnlStockTop);

            // =========================
            // ВКЛАДКА ПРИЕМ ТОВАРОВ
            // =========================
            this._tabReceipt.Location = new System.Drawing.Point(4, 24);
            this._tabReceipt.Name = "_tabReceipt";
            this._tabReceipt.Padding = new System.Windows.Forms.Padding(3);
            this._tabReceipt.Size = new System.Drawing.Size(1392, 822);
            this._tabReceipt.Text = "Прием товаров";
            this._tabReceipt.UseVisualStyleBackColor = true;

            pnlOrderTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlOrderTop.Height = 118;
            pnlOrderTop.Name = "pnlOrderTop";

            lblOrderStore.AutoSize = true;
            lblOrderStore.Location = new System.Drawing.Point(12, 18);
            lblOrderStore.Text = "Склад";

            this._cmbOrderStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbOrderStore.Location = new System.Drawing.Point(60, 14);
            this._cmbOrderStore.Name = "_cmbOrderStore";
            this._cmbOrderStore.Size = new System.Drawing.Size(220, 23);

            lblOrderSupplier.AutoSize = true;
            lblOrderSupplier.Location = new System.Drawing.Point(295, 18);
            lblOrderSupplier.Text = "Поставщик";

            this._cmbOrderSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbOrderSupplier.Location = new System.Drawing.Point(370, 14);
            this._cmbOrderSupplier.Name = "_cmbOrderSupplier";
            this._cmbOrderSupplier.Size = new System.Drawing.Size(260, 23);

            this._btnOrderHeadRefresh.Location = new System.Drawing.Point(640, 12);
            this._btnOrderHeadRefresh.Name = "_btnOrderHeadRefresh";
            this._btnOrderHeadRefresh.Size = new System.Drawing.Size(150, 28);
            this._btnOrderHeadRefresh.Text = "Обновить документы";
            this._btnOrderHeadRefresh.UseVisualStyleBackColor = true;
            this._btnOrderHeadRefresh.Click += new System.EventHandler(this.btnOrderHeadRefresh_Click);

            this._btnOrderHeadCreate.Location = new System.Drawing.Point(800, 12);
            this._btnOrderHeadCreate.Name = "_btnOrderHeadCreate";
            this._btnOrderHeadCreate.Size = new System.Drawing.Size(130, 28);
            this._btnOrderHeadCreate.Text = "Создать приемку";
            this._btnOrderHeadCreate.UseVisualStyleBackColor = true;
            this._btnOrderHeadCreate.Click += new System.EventHandler(this.btnOrderHeadCreate_Click);

            this._btnOrderHeadAccept.Location = new System.Drawing.Point(940, 12);
            this._btnOrderHeadAccept.Name = "_btnOrderHeadAccept";
            this._btnOrderHeadAccept.Size = new System.Drawing.Size(155, 28);
            this._btnOrderHeadAccept.Text = "Подтвердить приемку";
            this._btnOrderHeadAccept.UseVisualStyleBackColor = true;
            this._btnOrderHeadAccept.Click += new System.EventHandler(this.btnOrderHeadAccept_Click);

            lblOrderProduct.AutoSize = true;
            lblOrderProduct.Location = new System.Drawing.Point(12, 74);
            lblOrderProduct.Text = "Товар";

            this._cmbOrderProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbOrderProduct.Location = new System.Drawing.Point(60, 70);
            this._cmbOrderProduct.Name = "_cmbOrderProduct";
            this._cmbOrderProduct.Size = new System.Drawing.Size(260, 23);

            lblOrderQuant.AutoSize = true;
            lblOrderQuant.Location = new System.Drawing.Point(335, 74);
            lblOrderQuant.Text = "Количество";

            this._nudOrderQuant.DecimalPlaces = 3;
            this._nudOrderQuant.Location = new System.Drawing.Point(410, 70);
            this._nudOrderQuant.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this._nudOrderQuant.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            this._nudOrderQuant.Name = "_nudOrderQuant";
            this._nudOrderQuant.Size = new System.Drawing.Size(110, 23);
            this._nudOrderQuant.Value = new decimal(new int[] { 1, 0, 0, 0 });

            lblOrderPrice.AutoSize = true;
            lblOrderPrice.Location = new System.Drawing.Point(540, 74);
            lblOrderPrice.Text = "Цена";

            this._nudOrderPrice.DecimalPlaces = 2;
            this._nudOrderPrice.Location = new System.Drawing.Point(580, 70);
            this._nudOrderPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this._nudOrderPrice.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            this._nudOrderPrice.Name = "_nudOrderPrice";
            this._nudOrderPrice.Size = new System.Drawing.Size(110, 23);
            this._nudOrderPrice.Value = new decimal(new int[] { 1, 0, 0, 0 });

            this._btnOrderSpecAdd.Location = new System.Drawing.Point(705, 68);
            this._btnOrderSpecAdd.Name = "_btnOrderSpecAdd";
            this._btnOrderSpecAdd.Size = new System.Drawing.Size(120, 28);
            this._btnOrderSpecAdd.Text = "Добавить строку";
            this._btnOrderSpecAdd.UseVisualStyleBackColor = true;
            this._btnOrderSpecAdd.Click += new System.EventHandler(this.btnOrderSpecAdd_Click);

            this._btnOrderSpecDelete.Location = new System.Drawing.Point(835, 68);
            this._btnOrderSpecDelete.Name = "_btnOrderSpecDelete";
            this._btnOrderSpecDelete.Size = new System.Drawing.Size(120, 28);
            this._btnOrderSpecDelete.Text = "Удалить строку";
            this._btnOrderSpecDelete.UseVisualStyleBackColor = true;
            this._btnOrderSpecDelete.Click += new System.EventHandler(this.btnOrderSpecDelete_Click);

            pnlOrderTop.Controls.Add(lblOrderStore);
            pnlOrderTop.Controls.Add(this._cmbOrderStore);
            pnlOrderTop.Controls.Add(lblOrderSupplier);
            pnlOrderTop.Controls.Add(this._cmbOrderSupplier);
            pnlOrderTop.Controls.Add(this._btnOrderHeadRefresh);
            pnlOrderTop.Controls.Add(this._btnOrderHeadCreate);
            pnlOrderTop.Controls.Add(this._btnOrderHeadAccept);
            pnlOrderTop.Controls.Add(lblOrderProduct);
            pnlOrderTop.Controls.Add(this._cmbOrderProduct);
            pnlOrderTop.Controls.Add(lblOrderQuant);
            pnlOrderTop.Controls.Add(this._nudOrderQuant);
            pnlOrderTop.Controls.Add(lblOrderPrice);
            pnlOrderTop.Controls.Add(this._nudOrderPrice);
            pnlOrderTop.Controls.Add(this._btnOrderSpecAdd);
            pnlOrderTop.Controls.Add(this._btnOrderSpecDelete);

            splitOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            splitOrder.Location = new System.Drawing.Point(3, 121);
            splitOrder.Name = "splitOrder";
            splitOrder.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitOrder.SplitterDistance = 430;

            this._dgvOrderHeads.AllowUserToAddRows = false;
            this._dgvOrderHeads.AllowUserToDeleteRows = false;
            this._dgvOrderHeads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvOrderHeads.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvOrderHeads.MultiSelect = false;
            this._dgvOrderHeads.Name = "_dgvOrderHeads";
            this._dgvOrderHeads.ReadOnly = true;
            this._dgvOrderHeads.RowHeadersVisible = false;
            this._dgvOrderHeads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvOrderHeads.SelectionChanged += new System.EventHandler(this.dgvOrderHeads_SelectionChanged);

            this._dgvOrderSpecs.AllowUserToAddRows = false;
            this._dgvOrderSpecs.AllowUserToDeleteRows = false;
            this._dgvOrderSpecs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvOrderSpecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvOrderSpecs.MultiSelect = false;
            this._dgvOrderSpecs.Name = "_dgvOrderSpecs";
            this._dgvOrderSpecs.ReadOnly = true;
            this._dgvOrderSpecs.RowHeadersVisible = false;
            this._dgvOrderSpecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvOrderSpecs.SelectionChanged += new System.EventHandler(this.dgvOrderSpecs_SelectionChanged);

            splitOrder.Panel1.Controls.Add(this._dgvOrderHeads);
            splitOrder.Panel2.Controls.Add(this._dgvOrderSpecs);

            this._tabReceipt.Controls.Add(splitOrder);
            this._tabReceipt.Controls.Add(pnlOrderTop);

            // =========================
            // ВКЛАДКА ВЫДАЧА
            // =========================
            this._tabIssue.Location = new System.Drawing.Point(4, 24);
            this._tabIssue.Name = "_tabIssue";
            this._tabIssue.Padding = new System.Windows.Forms.Padding(3);
            this._tabIssue.Size = new System.Drawing.Size(1392, 822);
            this._tabIssue.Text = "Выдача товаров";
            this._tabIssue.UseVisualStyleBackColor = true;

            pnlSaleTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSaleTop.Height = 118;
            pnlSaleTop.Name = "pnlSaleTop";

            lblSaleStore.AutoSize = true;
            lblSaleStore.Location = new System.Drawing.Point(12, 18);
            lblSaleStore.Text = "Склад";

            this._cmbSaleStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbSaleStore.Location = new System.Drawing.Point(60, 14);
            this._cmbSaleStore.Name = "_cmbSaleStore";
            this._cmbSaleStore.Size = new System.Drawing.Size(220, 23);

            lblSaleClient.AutoSize = true;
            lblSaleClient.Location = new System.Drawing.Point(295, 18);
            lblSaleClient.Text = "Клиент";

            this._cmbSaleClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbSaleClient.Location = new System.Drawing.Point(345, 14);
            this._cmbSaleClient.Name = "_cmbSaleClient";
            this._cmbSaleClient.Size = new System.Drawing.Size(260, 23);

            this._btnSaleHeadRefresh.Location = new System.Drawing.Point(615, 12);
            this._btnSaleHeadRefresh.Name = "_btnSaleHeadRefresh";
            this._btnSaleHeadRefresh.Size = new System.Drawing.Size(150, 28);
            this._btnSaleHeadRefresh.Text = "Обновить накладные";
            this._btnSaleHeadRefresh.UseVisualStyleBackColor = true;
            this._btnSaleHeadRefresh.Click += new System.EventHandler(this.btnSaleHeadRefresh_Click);

            this._btnSaleHeadCreate.Location = new System.Drawing.Point(775, 12);
            this._btnSaleHeadCreate.Name = "_btnSaleHeadCreate";
            this._btnSaleHeadCreate.Size = new System.Drawing.Size(140, 28);
            this._btnSaleHeadCreate.Text = "Создать накладную";
            this._btnSaleHeadCreate.UseVisualStyleBackColor = true;
            this._btnSaleHeadCreate.Click += new System.EventHandler(this.btnSaleHeadCreate_Click);

            this._btnSaleHeadProcess.Location = new System.Drawing.Point(925, 12);
            this._btnSaleHeadProcess.Name = "_btnSaleHeadProcess";
            this._btnSaleHeadProcess.Size = new System.Drawing.Size(130, 28);
            this._btnSaleHeadProcess.Text = "Провести выдачу";
            this._btnSaleHeadProcess.UseVisualStyleBackColor = true;
            this._btnSaleHeadProcess.Click += new System.EventHandler(this.btnSaleHeadProcess_Click);

            lblSaleProduct.AutoSize = true;
            lblSaleProduct.Location = new System.Drawing.Point(12, 74);
            lblSaleProduct.Text = "Товар";

            this._cmbSaleProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbSaleProduct.Location = new System.Drawing.Point(60, 70);
            this._cmbSaleProduct.Name = "_cmbSaleProduct";
            this._cmbSaleProduct.Size = new System.Drawing.Size(260, 23);

            lblSaleQuant.AutoSize = true;
            lblSaleQuant.Location = new System.Drawing.Point(335, 74);
            lblSaleQuant.Text = "Количество";

            this._nudSaleQuant.DecimalPlaces = 3;
            this._nudSaleQuant.Location = new System.Drawing.Point(410, 70);
            this._nudSaleQuant.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this._nudSaleQuant.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            this._nudSaleQuant.Name = "_nudSaleQuant";
            this._nudSaleQuant.Size = new System.Drawing.Size(110, 23);
            this._nudSaleQuant.Value = new decimal(new int[] { 1, 0, 0, 0 });

            this._btnSaleSpecAdd.Location = new System.Drawing.Point(540, 68);
            this._btnSaleSpecAdd.Name = "_btnSaleSpecAdd";
            this._btnSaleSpecAdd.Size = new System.Drawing.Size(120, 28);
            this._btnSaleSpecAdd.Text = "Добавить строку";
            this._btnSaleSpecAdd.UseVisualStyleBackColor = true;
            this._btnSaleSpecAdd.Click += new System.EventHandler(this.btnSaleSpecAdd_Click);

            this._btnSaleSpecDelete.Location = new System.Drawing.Point(670, 68);
            this._btnSaleSpecDelete.Name = "_btnSaleSpecDelete";
            this._btnSaleSpecDelete.Size = new System.Drawing.Size(120, 28);
            this._btnSaleSpecDelete.Text = "Удалить строку";
            this._btnSaleSpecDelete.UseVisualStyleBackColor = true;
            this._btnSaleSpecDelete.Click += new System.EventHandler(this.btnSaleSpecDelete_Click);

            pnlSaleTop.Controls.Add(lblSaleStore);
            pnlSaleTop.Controls.Add(this._cmbSaleStore);
            pnlSaleTop.Controls.Add(lblSaleClient);
            pnlSaleTop.Controls.Add(this._cmbSaleClient);
            pnlSaleTop.Controls.Add(this._btnSaleHeadRefresh);
            pnlSaleTop.Controls.Add(this._btnSaleHeadCreate);
            pnlSaleTop.Controls.Add(this._btnSaleHeadProcess);
            pnlSaleTop.Controls.Add(lblSaleProduct);
            pnlSaleTop.Controls.Add(this._cmbSaleProduct);
            pnlSaleTop.Controls.Add(lblSaleQuant);
            pnlSaleTop.Controls.Add(this._nudSaleQuant);
            pnlSaleTop.Controls.Add(this._btnSaleSpecAdd);
            pnlSaleTop.Controls.Add(this._btnSaleSpecDelete);

            splitSale.Dock = System.Windows.Forms.DockStyle.Fill;
            splitSale.Location = new System.Drawing.Point(3, 121);
            splitSale.Name = "splitSale";
            splitSale.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitSale.SplitterDistance = 430;

            this._dgvSaleHeads.AllowUserToAddRows = false;
            this._dgvSaleHeads.AllowUserToDeleteRows = false;
            this._dgvSaleHeads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvSaleHeads.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvSaleHeads.MultiSelect = false;
            this._dgvSaleHeads.Name = "_dgvSaleHeads";
            this._dgvSaleHeads.ReadOnly = true;
            this._dgvSaleHeads.RowHeadersVisible = false;
            this._dgvSaleHeads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvSaleHeads.SelectionChanged += new System.EventHandler(this.dgvSaleHeads_SelectionChanged);

            this._dgvSaleSpecs.AllowUserToAddRows = false;
            this._dgvSaleSpecs.AllowUserToDeleteRows = false;
            this._dgvSaleSpecs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvSaleSpecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvSaleSpecs.MultiSelect = false;
            this._dgvSaleSpecs.Name = "_dgvSaleSpecs";
            this._dgvSaleSpecs.ReadOnly = true;
            this._dgvSaleSpecs.RowHeadersVisible = false;
            this._dgvSaleSpecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvSaleSpecs.SelectionChanged += new System.EventHandler(this.dgvSaleSpecs_SelectionChanged);

            splitSale.Panel1.Controls.Add(this._dgvSaleHeads);
            splitSale.Panel2.Controls.Add(this._dgvSaleSpecs);

            this._tabIssue.Controls.Add(splitSale);
            this._tabIssue.Controls.Add(pnlSaleTop);

            // =========================
            // ВКЛАДКА ПЕРЕМЕЩЕНИЯ
            // =========================
            this._tabTransfer.Location = new System.Drawing.Point(4, 24);
            this._tabTransfer.Name = "_tabTransfer";
            this._tabTransfer.Padding = new System.Windows.Forms.Padding(3);
            this._tabTransfer.Size = new System.Drawing.Size(1392, 822);
            this._tabTransfer.Text = "Внутренние перемещения";
            this._tabTransfer.UseVisualStyleBackColor = true;

            pnlTransferTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlTransferTop.Height = 118;
            pnlTransferTop.Name = "pnlTransferTop";

            lblTransferStoreOut.AutoSize = true;
            lblTransferStoreOut.Location = new System.Drawing.Point(12, 18);
            lblTransferStoreOut.Text = "Со склада";

            this._cmbTransferStoreOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbTransferStoreOut.Location = new System.Drawing.Point(82, 14);
            this._cmbTransferStoreOut.Name = "_cmbTransferStoreOut";
            this._cmbTransferStoreOut.Size = new System.Drawing.Size(220, 23);

            lblTransferStoreIn.AutoSize = true;
            lblTransferStoreIn.Location = new System.Drawing.Point(315, 18);
            lblTransferStoreIn.Text = "На склад";

            this._cmbTransferStoreIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbTransferStoreIn.Location = new System.Drawing.Point(375, 14);
            this._cmbTransferStoreIn.Name = "_cmbTransferStoreIn";
            this._cmbTransferStoreIn.Size = new System.Drawing.Size(220, 23);

            this._btnTransferHeadRefresh.Location = new System.Drawing.Point(605, 12);
            this._btnTransferHeadRefresh.Name = "_btnTransferHeadRefresh";
            this._btnTransferHeadRefresh.Size = new System.Drawing.Size(170, 28);
            this._btnTransferHeadRefresh.Text = "Обновить перемещения";
            this._btnTransferHeadRefresh.UseVisualStyleBackColor = true;
            this._btnTransferHeadRefresh.Click += new System.EventHandler(this.btnTransferHeadRefresh_Click);

            this._btnTransferHeadCreate.Location = new System.Drawing.Point(785, 12);
            this._btnTransferHeadCreate.Name = "_btnTransferHeadCreate";
            this._btnTransferHeadCreate.Size = new System.Drawing.Size(155, 28);
            this._btnTransferHeadCreate.Text = "Создать перемещение";
            this._btnTransferHeadCreate.UseVisualStyleBackColor = true;
            this._btnTransferHeadCreate.Click += new System.EventHandler(this.btnTransferHeadCreate_Click);

            this._btnTransferHeadSend.Location = new System.Drawing.Point(950, 12);
            this._btnTransferHeadSend.Name = "_btnTransferHeadSend";
            this._btnTransferHeadSend.Size = new System.Drawing.Size(90, 28);
            this._btnTransferHeadSend.Text = "Отправить";
            this._btnTransferHeadSend.UseVisualStyleBackColor = true;
            this._btnTransferHeadSend.Click += new System.EventHandler(this.btnTransferHeadSend_Click);

            this._btnTransferHeadAccept.Location = new System.Drawing.Point(1048, 12);
            this._btnTransferHeadAccept.Name = "_btnTransferHeadAccept";
            this._btnTransferHeadAccept.Size = new System.Drawing.Size(82, 28);
            this._btnTransferHeadAccept.Text = "Принять";
            this._btnTransferHeadAccept.UseVisualStyleBackColor = true;
            this._btnTransferHeadAccept.Click += new System.EventHandler(this.btnTransferHeadAccept_Click);

            lblTransferProduct.AutoSize = true;
            lblTransferProduct.Location = new System.Drawing.Point(12, 74);
            lblTransferProduct.Text = "Товар";

            this._cmbTransferProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbTransferProduct.Location = new System.Drawing.Point(60, 70);
            this._cmbTransferProduct.Name = "_cmbTransferProduct";
            this._cmbTransferProduct.Size = new System.Drawing.Size(260, 23);

            lblTransferQuant.AutoSize = true;
            lblTransferQuant.Location = new System.Drawing.Point(335, 74);
            lblTransferQuant.Text = "Количество";

            this._nudTransferQuant.DecimalPlaces = 3;
            this._nudTransferQuant.Location = new System.Drawing.Point(410, 70);
            this._nudTransferQuant.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this._nudTransferQuant.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            this._nudTransferQuant.Name = "_nudTransferQuant";
            this._nudTransferQuant.Size = new System.Drawing.Size(110, 23);
            this._nudTransferQuant.Value = new decimal(new int[] { 1, 0, 0, 0 });

            this._btnTransferSpecAdd.Location = new System.Drawing.Point(540, 68);
            this._btnTransferSpecAdd.Name = "_btnTransferSpecAdd";
            this._btnTransferSpecAdd.Size = new System.Drawing.Size(120, 28);
            this._btnTransferSpecAdd.Text = "Добавить строку";
            this._btnTransferSpecAdd.UseVisualStyleBackColor = true;
            this._btnTransferSpecAdd.Click += new System.EventHandler(this.btnTransferSpecAdd_Click);

            this._btnTransferSpecDelete.Location = new System.Drawing.Point(670, 68);
            this._btnTransferSpecDelete.Name = "_btnTransferSpecDelete";
            this._btnTransferSpecDelete.Size = new System.Drawing.Size(120, 28);
            this._btnTransferSpecDelete.Text = "Удалить строку";
            this._btnTransferSpecDelete.UseVisualStyleBackColor = true;
            this._btnTransferSpecDelete.Click += new System.EventHandler(this.btnTransferSpecDelete_Click);

            pnlTransferTop.Controls.Add(lblTransferStoreOut);
            pnlTransferTop.Controls.Add(this._cmbTransferStoreOut);
            pnlTransferTop.Controls.Add(lblTransferStoreIn);
            pnlTransferTop.Controls.Add(this._cmbTransferStoreIn);
            pnlTransferTop.Controls.Add(this._btnTransferHeadRefresh);
            pnlTransferTop.Controls.Add(this._btnTransferHeadCreate);
            pnlTransferTop.Controls.Add(this._btnTransferHeadSend);
            pnlTransferTop.Controls.Add(this._btnTransferHeadAccept);
            pnlTransferTop.Controls.Add(lblTransferProduct);
            pnlTransferTop.Controls.Add(this._cmbTransferProduct);
            pnlTransferTop.Controls.Add(lblTransferQuant);
            pnlTransferTop.Controls.Add(this._nudTransferQuant);
            pnlTransferTop.Controls.Add(this._btnTransferSpecAdd);
            pnlTransferTop.Controls.Add(this._btnTransferSpecDelete);

            splitTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitTransfer.Location = new System.Drawing.Point(3, 121);
            splitTransfer.Name = "splitTransfer";
            splitTransfer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitTransfer.SplitterDistance = 430;

            this._dgvTransferHeads.AllowUserToAddRows = false;
            this._dgvTransferHeads.AllowUserToDeleteRows = false;
            this._dgvTransferHeads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvTransferHeads.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvTransferHeads.MultiSelect = false;
            this._dgvTransferHeads.Name = "_dgvTransferHeads";
            this._dgvTransferHeads.ReadOnly = true;
            this._dgvTransferHeads.RowHeadersVisible = false;
            this._dgvTransferHeads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvTransferHeads.SelectionChanged += new System.EventHandler(this.dgvTransferHeads_SelectionChanged);

            this._dgvTransferSpecs.AllowUserToAddRows = false;
            this._dgvTransferSpecs.AllowUserToDeleteRows = false;
            this._dgvTransferSpecs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvTransferSpecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvTransferSpecs.MultiSelect = false;
            this._dgvTransferSpecs.Name = "_dgvTransferSpecs";
            this._dgvTransferSpecs.ReadOnly = true;
            this._dgvTransferSpecs.RowHeadersVisible = false;
            this._dgvTransferSpecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvTransferSpecs.SelectionChanged += new System.EventHandler(this.dgvTransferSpecs_SelectionChanged);

            splitTransfer.Panel1.Controls.Add(this._dgvTransferHeads);
            splitTransfer.Panel2.Controls.Add(this._dgvTransferSpecs);

            this._tabTransfer.Controls.Add(splitTransfer);
            this._tabTransfer.Controls.Add(pnlTransferTop);

            // Добавление вкладок
            this._tabMain.Controls.Add(this._tabStock);
            this._tabMain.Controls.Add(this._tabReceipt);
            this._tabMain.Controls.Add(this._tabIssue);
            this._tabMain.Controls.Add(this._tabTransfer);

            this.Controls.Add(this._tabMain);

            splitOrder.Panel1.ResumeLayout(false);
            splitOrder.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitOrder)).EndInit();
            splitOrder.ResumeLayout(false);

            splitSale.Panel1.ResumeLayout(false);
            splitSale.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitSale)).EndInit();
            splitSale.ResumeLayout(false);

            splitTransfer.Panel1.ResumeLayout(false);
            splitTransfer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitTransfer)).EndInit();
            splitTransfer.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this._nudOrderQuant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudOrderPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudSaleQuant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudTransferQuant)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this._dgvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSaleHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSaleSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTransferHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTransferSpecs)).EndInit();

            this.ResumeLayout(false);
        }
    }
}