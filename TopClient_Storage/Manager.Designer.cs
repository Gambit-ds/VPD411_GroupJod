namespace TopClient_Storage
{
    partial class Manager
    {
        private System.ComponentModel.IContainer? components = null;

        private TabControl _tabMain = null!;
        private TabPage _tabOrders = null!;
        private TabPage _tabStock = null!;

        private ComboBox _cmbOrderStore = null!;
        private ComboBox _cmbOrderClient = null!;
        private ComboBox _cmbOrderProduct = null!;
        private NumericUpDown _nudOrderQuant = null!;
        private Button _btnOrderHeadRefresh = null!;
        private Button _btnOrderHeadCreate = null!;
        private Button _btnOrderHeadClose = null!;
        private Button _btnOrderSpecAdd = null!;
        private Button _btnOrderSpecDelete = null!;
        private DataGridView _dgvOrderHeads = null!;
        private DataGridView _dgvOrderSpecs = null!;

        private ComboBox _cmbStockStoreFilter = null!;
        private ComboBox _cmbStockProductFilter = null!;
        private Button _btnStockRefresh = null!;
        private DataGridView _dgvStock = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            System.Windows.Forms.Panel pnlOrdersTop;
            System.Windows.Forms.Label lblOrderStore;
            System.Windows.Forms.Label lblOrderClient;
            System.Windows.Forms.Label lblOrderProduct;
            System.Windows.Forms.Label lblOrderQuant;
            System.Windows.Forms.SplitContainer splitOrders;

            System.Windows.Forms.Panel pnlStockTop;
            System.Windows.Forms.Label lblStockStore;
            System.Windows.Forms.Label lblStockProduct;

            pnlOrdersTop = new System.Windows.Forms.Panel();
            lblOrderStore = new System.Windows.Forms.Label();
            lblOrderClient = new System.Windows.Forms.Label();
            lblOrderProduct = new System.Windows.Forms.Label();
            lblOrderQuant = new System.Windows.Forms.Label();
            splitOrders = new System.Windows.Forms.SplitContainer();

            pnlStockTop = new System.Windows.Forms.Panel();
            lblStockStore = new System.Windows.Forms.Label();
            lblStockProduct = new System.Windows.Forms.Label();

            this._tabMain = new System.Windows.Forms.TabControl();
            this._tabOrders = new System.Windows.Forms.TabPage();
            this._tabStock = new System.Windows.Forms.TabPage();

            this._cmbOrderStore = new System.Windows.Forms.ComboBox();
            this._cmbOrderClient = new System.Windows.Forms.ComboBox();
            this._cmbOrderProduct = new System.Windows.Forms.ComboBox();
            this._nudOrderQuant = new System.Windows.Forms.NumericUpDown();
            this._btnOrderHeadRefresh = new System.Windows.Forms.Button();
            this._btnOrderHeadCreate = new System.Windows.Forms.Button();
            this._btnOrderHeadClose = new System.Windows.Forms.Button();
            this._btnOrderSpecAdd = new System.Windows.Forms.Button();
            this._btnOrderSpecDelete = new System.Windows.Forms.Button();
            this._dgvOrderHeads = new System.Windows.Forms.DataGridView();
            this._dgvOrderSpecs = new System.Windows.Forms.DataGridView();

            this._cmbStockStoreFilter = new System.Windows.Forms.ComboBox();
            this._cmbStockProductFilter = new System.Windows.Forms.ComboBox();
            this._btnStockRefresh = new System.Windows.Forms.Button();
            this._dgvStock = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(splitOrders)).BeginInit();
            splitOrders.Panel1.SuspendLayout();
            splitOrders.Panel2.SuspendLayout();
            splitOrders.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this._nudOrderQuant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvStock)).BeginInit();

            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 850);
            this.Name = "Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Менеджер / кассир";
            this.Load += new System.EventHandler(this.Manager_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Manager_FormClosing);

            this._tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabMain.Location = new System.Drawing.Point(0, 0);
            this._tabMain.Name = "_tabMain";
            this._tabMain.SelectedIndex = 0;
            this._tabMain.Size = new System.Drawing.Size(1400, 850);

            // Вкладка заказов
            this._tabOrders.Location = new System.Drawing.Point(4, 24);
            this._tabOrders.Name = "_tabOrders";
            this._tabOrders.Padding = new System.Windows.Forms.Padding(3);
            this._tabOrders.Size = new System.Drawing.Size(1392, 822);
            this._tabOrders.Text = "Заказы клиентов";
            this._tabOrders.UseVisualStyleBackColor = true;

            pnlOrdersTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlOrdersTop.Height = 118;

            lblOrderStore.AutoSize = true;
            lblOrderStore.Location = new System.Drawing.Point(12, 18);
            lblOrderStore.Text = "Склад";

            this._cmbOrderStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbOrderStore.Location = new System.Drawing.Point(60, 14);
            this._cmbOrderStore.Name = "_cmbOrderStore";
            this._cmbOrderStore.Size = new System.Drawing.Size(220, 23);

            lblOrderClient.AutoSize = true;
            lblOrderClient.Location = new System.Drawing.Point(295, 18);
            lblOrderClient.Text = "Клиент";

            this._cmbOrderClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbOrderClient.Location = new System.Drawing.Point(345, 14);
            this._cmbOrderClient.Name = "_cmbOrderClient";
            this._cmbOrderClient.Size = new System.Drawing.Size(260, 23);

            this._btnOrderHeadRefresh.Location = new System.Drawing.Point(615, 12);
            this._btnOrderHeadRefresh.Name = "_btnOrderHeadRefresh";
            this._btnOrderHeadRefresh.Size = new System.Drawing.Size(145, 28);
            this._btnOrderHeadRefresh.Text = "Обновить заказы";
            this._btnOrderHeadRefresh.UseVisualStyleBackColor = true;
            this._btnOrderHeadRefresh.Click += new System.EventHandler(this.btnOrderHeadRefresh_Click);

            this._btnOrderHeadCreate.Location = new System.Drawing.Point(770, 12);
            this._btnOrderHeadCreate.Name = "_btnOrderHeadCreate";
            this._btnOrderHeadCreate.Size = new System.Drawing.Size(125, 28);
            this._btnOrderHeadCreate.Text = "Создать заказ";
            this._btnOrderHeadCreate.UseVisualStyleBackColor = true;
            this._btnOrderHeadCreate.Click += new System.EventHandler(this.btnOrderHeadCreate_Click);

            this._btnOrderHeadClose.Location = new System.Drawing.Point(905, 12);
            this._btnOrderHeadClose.Name = "_btnOrderHeadClose";
            this._btnOrderHeadClose.Size = new System.Drawing.Size(135, 28);
            this._btnOrderHeadClose.Text = "Закрыть заказ";
            this._btnOrderHeadClose.UseVisualStyleBackColor = true;
            this._btnOrderHeadClose.Click += new System.EventHandler(this.btnOrderHeadClose_Click);

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

            this._btnOrderSpecAdd.Location = new System.Drawing.Point(540, 68);
            this._btnOrderSpecAdd.Name = "_btnOrderSpecAdd";
            this._btnOrderSpecAdd.Size = new System.Drawing.Size(120, 28);
            this._btnOrderSpecAdd.Text = "Добавить строку";
            this._btnOrderSpecAdd.UseVisualStyleBackColor = true;
            this._btnOrderSpecAdd.Click += new System.EventHandler(this.btnOrderSpecAdd_Click);

            this._btnOrderSpecDelete.Location = new System.Drawing.Point(670, 68);
            this._btnOrderSpecDelete.Name = "_btnOrderSpecDelete";
            this._btnOrderSpecDelete.Size = new System.Drawing.Size(120, 28);
            this._btnOrderSpecDelete.Text = "Удалить строку";
            this._btnOrderSpecDelete.UseVisualStyleBackColor = true;
            this._btnOrderSpecDelete.Click += new System.EventHandler(this.btnOrderSpecDelete_Click);

            pnlOrdersTop.Controls.Add(lblOrderStore);
            pnlOrdersTop.Controls.Add(this._cmbOrderStore);
            pnlOrdersTop.Controls.Add(lblOrderClient);
            pnlOrdersTop.Controls.Add(this._cmbOrderClient);
            pnlOrdersTop.Controls.Add(this._btnOrderHeadRefresh);
            pnlOrdersTop.Controls.Add(this._btnOrderHeadCreate);
            pnlOrdersTop.Controls.Add(this._btnOrderHeadClose);
            pnlOrdersTop.Controls.Add(lblOrderProduct);
            pnlOrdersTop.Controls.Add(this._cmbOrderProduct);
            pnlOrdersTop.Controls.Add(lblOrderQuant);
            pnlOrdersTop.Controls.Add(this._nudOrderQuant);
            pnlOrdersTop.Controls.Add(this._btnOrderSpecAdd);
            pnlOrdersTop.Controls.Add(this._btnOrderSpecDelete);

            splitOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            splitOrders.Location = new System.Drawing.Point(3, 121);
            splitOrders.Name = "splitOrders";
            splitOrders.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitOrders.SplitterDistance = 430;

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

            splitOrders.Panel1.Controls.Add(this._dgvOrderHeads);
            splitOrders.Panel2.Controls.Add(this._dgvOrderSpecs);

            this._tabOrders.Controls.Add(splitOrders);
            this._tabOrders.Controls.Add(pnlOrdersTop);

            // Вкладка остатков
            this._tabStock.Location = new System.Drawing.Point(4, 24);
            this._tabStock.Name = "_tabStock";
            this._tabStock.Padding = new System.Windows.Forms.Padding(3);
            this._tabStock.Size = new System.Drawing.Size(1392, 822);
            this._tabStock.Text = "Доступные остатки";
            this._tabStock.UseVisualStyleBackColor = true;

            pnlStockTop.Dock = System.Windows.Forms.DockStyle.Top;
            pnlStockTop.Height = 56;

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
            this._btnStockRefresh.Size = new System.Drawing.Size(155, 28);
            this._btnStockRefresh.Text = "Обновить остатки";
            this._btnStockRefresh.UseVisualStyleBackColor = true;
            this._btnStockRefresh.Click += new System.EventHandler(this.btnStockRefresh_Click);

            this._dgvStock.AllowUserToAddRows = false;
            this._dgvStock.AllowUserToDeleteRows = false;
            this._dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
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

            this._tabMain.Controls.Add(this._tabOrders);
            this._tabMain.Controls.Add(this._tabStock);

            this.Controls.Add(this._tabMain);

            splitOrders.Panel1.ResumeLayout(false);
            splitOrders.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitOrders)).EndInit();
            splitOrders.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this._nudOrderQuant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvOrderSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvStock)).EndInit();

            this.ResumeLayout(false);
        }
    }
}
