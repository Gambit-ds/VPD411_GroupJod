using DBQwery;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Storage
{
    public partial class ServerForm : Form
    {
        private TcpListener? _listener;
        private CancellationTokenSource? _stopSource;
        private bool _serverStarted;

        private readonly string _connectionString =
            @"Data Source=DESKTOP-DF3LQAG\SQLEXPRESS;Initial Catalog=Storag;Integrated Security=true;Connect Timeout=30;TrustServerCertificate=true";

        public ServerForm()
        {
            InitializeComponent();
            FormClosing += ServerForm_FormClosing;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serverStarted)
                {
                    AddLog("Сервер уже запущен.");
                    return;
                }

                if (!int.TryParse(textBoxPort.Text.Trim(), out int port))
                {
                    AddLog("Порт указан неверно.");
                    return;
                }

                _stopSource = new CancellationTokenSource();
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();

                _serverStarted = true;
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                textBoxPort.Enabled = false;

                AddLog($"Сервер запущен на 0.0.0.0:{port}");

                _ = Task.Run(() => AcceptClientsAsync(_stopSource.Token));
            }
            catch (Exception ex)
            {
                AddLog("Ошибка запуска сервера: " + ex.Message);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private async Task AcceptClientsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient? client = null;

                try
                {
                    if (_listener == null)
                        break;

                    client = await _listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    AddLog("Ошибка приема клиента: " + ex.Message);

                    try
                    {
                        client?.Close();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
        {
            string remotePoint = client.Client.RemoteEndPoint?.ToString() ?? "неизвестный узел";

            try
            {
                AddLog("Подключен клиент: " + remotePoint);

                using (client)
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, false, 4096, true))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, 4096, true))
                {
                    writer.AutoFlush = true;
                    writer.NewLine = "\r\n";

                    await writer.WriteLineAsync("OK:CONNECTED");

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        string? request = await reader.ReadLineAsync();

                        if (request == null)
                        {
                            AddLog("Клиент закрыл соединение: " + remotePoint);
                            break;
                        }

                        request = request.Trim();

                        if (request.Length == 0)
                            continue;

                        AddLog("Получено от " + remotePoint + ": " + request);

                        string response = await ProcessRequestAsync(request);

                        await writer.WriteLineAsync(response);

                        AddLog("Отправлено " + remotePoint + ": " + response);

                        if (string.Equals(request, "QUIT", StringComparison.OrdinalIgnoreCase))
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog("Ошибка при работе с клиентом " + remotePoint + ": " + ex.Message);
            }
            finally
            {
                AddLog("Отключен клиент: " + remotePoint);
            }
        }

        private async Task<string> ProcessRequestAsync(string request)
        {
            if (string.Equals(request, "PING", StringComparison.OrdinalIgnoreCase))
                return "OK:PONG";

            if (string.Equals(request, "TIME", StringComparison.OrdinalIgnoreCase))
                return "OK:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.Equals(request, "QUIT", StringComparison.OrdinalIgnoreCase))
                return "OK:BYE";

            string[] parts = request.Split('|');
            if (parts.Length == 0)
                return "ERR:EMPTY_REQUEST";

            string command = parts[0].ToUpperInvariant();

            switch (command)
            {
                case "AUTH":
                    return await HandleAuthAsync(parts);

                case "ROLES_GET_ALL":
                    return await HandleRolesGetAllAsync();

                case "USERS_GET_ALL":
                    return await HandleUsersGetAllAsync();

                case "USER_ADD":
                    return await HandleUserAddAsync(parts);

                case "USER_UPDATE":
                    return await HandleUserUpdateAsync(parts);

                case "USER_DELETE":
                    return await HandleUserDeleteAsync(parts);

                case "CATEGORIES_GET_ALL":
                    return await HandleCategoriesGetAllAsync();

                case "CATEGORY_ADD":
                    return await HandleCategoryAddAsync(parts);

                case "CATEGORY_UPDATE":
                    return await HandleCategoryUpdateAsync(parts);

                case "CATEGORY_DELETE":
                    return await HandleCategoryDeleteAsync(parts);

                case "PRODUCTS_GET_ALL":
                    return await HandleProductsGetAllAsync();

                case "PRODUCTS_SHORT_GET_ALL":
                    return await HandleProductsShortGetAllAsync();

                case "PRODUCT_ADD":
                    return await HandleProductAddAsync(parts);

                case "PRODUCT_UPDATE":
                    return await HandleProductUpdateAsync(parts);

                case "PRODUCT_DELETE":
                    return await HandleProductDeleteAsync(parts);

                case "CLIENTS_GET_ALL":
                    return await HandleClientsGetAllAsync();

                case "REPORT_STOCK":
                    return await HandleReportStockAsync();

                case "REPORT_MOVEMENT":
                    return await HandleReportMovementAsync(parts);

                case "REPORT_ORDERS_BY_DATE":
                    return await HandleReportOrdersByDateAsync(parts);

                case "REPORT_ORDERS_BY_CLIENT":
                    return await HandleReportOrdersByClientAsync(parts);

                case "REPORT_ORDERS_BY_PRODUCT":
                    return await HandleReportOrdersByProductAsync(parts);

                case "STORES_GET_ALL":
                    return await HandleStoresGetAllAsync();

                case "SUPPLIERS_GET_ALL":
                    return await HandleSuppliersGetAllAsync();

                case "STOCK_GET_ALL":
                    return await HandleStockGetAllAsync(parts);

                case "ORDERHEADS_GET_ALL":
                    return await HandleOrderHeadsGetAllAsync();

                case "ORDERSPECS_GET_BY_HEAD":
                    return await HandleOrderSpecsGetByHeadAsync(parts);

                case "ORDERHEAD_ADD":
                    return await HandleOrderHeadAddAsync(parts);

                case "ORDERSPEC_ADD":
                    return await HandleOrderSpecAddAsync(parts);

                case "ORDERSPEC_DELETE":
                    return await HandleOrderSpecDeleteAsync(parts);

                case "ORDERHEAD_ACCEPT":
                    return await HandleOrderHeadAcceptAsync(parts);

                case "SALEHEADS_GET_ALL":
                    return await HandleSaleHeadsGetAllAsync();

                case "SALESPECS_GET_BY_HEAD":
                    return await HandleSaleSpecsGetByHeadAsync(parts);

                case "SALEHEAD_ADD":
                    return await HandleSaleHeadAddAsync(parts);

                case "SALESPEC_ADD":
                    return await HandleSaleSpecAddAsync(parts);

                case "SALESPEC_DELETE":
                    return await HandleSaleSpecDeleteAsync(parts);

                case "SALEHEAD_PROCESS":
                    return await HandleSaleHeadProcessAsync(parts);

                case "TRANSFERHEADS_GET_ALL":
                    return await HandleTransferHeadsGetAllAsync();

                case "TRANSFERSPECS_GET_BY_HEAD":
                    return await HandleTransferSpecsGetByHeadAsync(parts);

                case "TRANSFERHEAD_ADD":
                    return await HandleTransferHeadAddAsync(parts);

                case "TRANSFERSPEC_ADD":
                    return await HandleTransferSpecAddAsync(parts);

                case "TRANSFERSPEC_DELETE":
                    return await HandleTransferSpecDeleteAsync(parts);

                case "TRANSFERHEAD_SEND":
                    return await HandleTransferHeadSendAsync(parts);

                case "TRANSFERHEAD_ACCEPT":
                    return await HandleTransferHeadAcceptAsync(parts);

                case "MANAGER_STOCK_GET":
                    return await HandleManagerStockGetAsync(parts);

                case "MANAGER_SALEHEADS_GET":
                    return await HandleManagerSaleHeadsGetAsync(parts);

                case "MANAGER_SALESPECS_GET":
                    return await HandleManagerSaleSpecsGetAsync(parts);

                case "MANAGER_SALEHEAD_ADD":
                    return await HandleManagerSaleHeadAddAsync(parts);

                case "MANAGER_SALESPEC_ADD":
                    return await HandleManagerSalespecAddAsync(parts);

                case "MANAGER_SALESPEC_DELETE":
                    return await HandleManagerSalespecDeleteAsync(parts);

                case "MANAGER_SALEHEAD_CLOSE":
                    return await HandleManagerSaleHeadCloseAsync(parts);

                default:
                    return "ERR:UNKNOWN_COMMAND";
            }
        }

        private async Task<string> HandleAuthAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:AUTH_FORMAT";

            try
            {
                DBConnection db = CreateDb();

                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.AuthUser",
                    new SqlParameter("@Login", parts[1]),
                    new SqlParameter("@Password", parts[2]));

                if (dt.Rows.Count == 0)
                    return "ERR:AUTH";

                DataRow row = dt.Rows[0];

                string userId = row["userid"]?.ToString() ?? "";
                string roleCode = row["rolecode"]?.ToString() ?? "";
                string longName = row["longname"]?.ToString() ?? "";

                longName = longName.Replace("|", "/");

                return $"OK:AUTH|{userId}|{roleCode}|{longName}";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка авторизации: " + ex.Message);
                return "ERR:SERVER_AUTH";
            }
        }

        private async Task<string> HandleRolesGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.RolesGetAll", "OK:ROLES|", "ERR:ROLES|");
        }

        private async Task<string> HandleUsersGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.UsersGetAll", "OK:USERS|", "ERR:USERS|");
        }

        private async Task<string> HandleUserAddAsync(string[] parts)
        {
            if (parts.Length < 7)
                return "ERR:USER_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.UserAdd",
                "userid",
                "OK:USER_ADD|",
                "ERR:USER_ADD|",
                new SqlParameter("@Name", parts[1]),
                new SqlParameter("@Sname", ToDbNullableString(parts[2])),
                new SqlParameter("@Fname", ToDbNullableString(parts[3])),
                new SqlParameter("@RoleId", ParseIntRequired(parts[4])),
                new SqlParameter("@Login", parts[5]),
                new SqlParameter("@Password", parts[6]));
        }

        private async Task<string> HandleUserUpdateAsync(string[] parts)
        {
            if (parts.Length < 8)
                return "ERR:USER_UPDATE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.UserUpdate",
                "OK:USER_UPDATE",
                "ERR:USER_UPDATE|",
                new SqlParameter("@UserId", ParseIntRequired(parts[1])),
                new SqlParameter("@Name", parts[2]),
                new SqlParameter("@Sname", ToDbNullableString(parts[3])),
                new SqlParameter("@Fname", ToDbNullableString(parts[4])),
                new SqlParameter("@RoleId", ParseIntRequired(parts[5])),
                new SqlParameter("@Login", parts[6]),
                new SqlParameter("@Password", ToDbNullableString(parts[7])));
        }

        private async Task<string> HandleUserDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:USER_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.UserDelete",
                "OK:USER_DELETE",
                "ERR:USER_DELETE|",
                new SqlParameter("@UserId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleCategoriesGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.CategoriesGetAll", "OK:CATEGORIES|", "ERR:CATEGORIES|");
        }

        private async Task<string> HandleCategoryAddAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:CATEGORY_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.CategoryAdd",
                "categoryid",
                "OK:CATEGORY_ADD|",
                "ERR:CATEGORY_ADD|",
                new SqlParameter("@Code", parts[1]));
        }

        private async Task<string> HandleCategoryUpdateAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:CATEGORY_UPDATE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.CategoryUpdate",
                "OK:CATEGORY_UPDATE",
                "ERR:CATEGORY_UPDATE|",
                new SqlParameter("@CategoryId", ParseIntRequired(parts[1])),
                new SqlParameter("@Code", parts[2]));
        }

        private async Task<string> HandleCategoryDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:CATEGORY_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.CategoryDelete",
                "OK:CATEGORY_DELETE",
                "ERR:CATEGORY_DELETE|",
                new SqlParameter("@CategoryId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleProductsGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.ProductsGetAll", "OK:PRODUCTS|", "ERR:PRODUCTS|");
        }

        private async Task<string> HandleProductsShortGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.ProductsShortGetAll", "OK:PRODUCTS_SHORT|", "ERR:PRODUCTS_SHORT|");
        }

        private async Task<string> HandleProductAddAsync(string[] parts)
        {
            if (parts.Length < 6)
                return "ERR:PRODUCT_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.ProductAdd",
                "productid",
                "OK:PRODUCT_ADD|",
                "ERR:PRODUCT_ADD|",
                new SqlParameter("@CategoryId", ParseIntRequired(parts[1])),
                new SqlParameter("@Code", parts[2]),
                new SqlParameter("@Description", ToDbNullableString(parts[3])),
                new SqlParameter("@Weight", ToDbNullableInt(parts[4])),
                new SqlParameter("@Size", ToDbNullableInt(parts[5])));
        }

        private async Task<string> HandleProductUpdateAsync(string[] parts)
        {
            if (parts.Length < 7)
                return "ERR:PRODUCT_UPDATE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.ProductUpdate",
                "OK:PRODUCT_UPDATE",
                "ERR:PRODUCT_UPDATE|",
                new SqlParameter("@ProductId", ParseIntRequired(parts[1])),
                new SqlParameter("@CategoryId", ParseIntRequired(parts[2])),
                new SqlParameter("@Code", parts[3]),
                new SqlParameter("@Description", ToDbNullableString(parts[4])),
                new SqlParameter("@Weight", ToDbNullableInt(parts[5])),
                new SqlParameter("@Size", ToDbNullableInt(parts[6])));
        }

        private async Task<string> HandleProductDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:PRODUCT_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.ProductDelete",
                "OK:PRODUCT_DELETE",
                "ERR:PRODUCT_DELETE|",
                new SqlParameter("@ProductId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleClientsGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.ClientsGetAll", "OK:CLIENTS|", "ERR:CLIENTS|");
        }

        private async Task<string> HandleReportStockAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.ReportStock", "OK:REPORT|", "ERR:REPORT_STOCK|");
        }

        private async Task<string> HandleReportMovementAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:REPORT_MOVEMENT_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.ReportMovement",
                "OK:REPORT|",
                "ERR:REPORT_MOVEMENT|",
                new SqlParameter("@DateFrom", ToDbNullableDate(parts[1])),
                new SqlParameter("@DateTo", ToDbNullableDate(parts[2])));
        }

        private async Task<string> HandleReportOrdersByDateAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:REPORT_ORDERS_BY_DATE_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.ReportOrdersByDate",
                "OK:REPORT|",
                "ERR:REPORT_ORDERS_BY_DATE|",
                new SqlParameter("@DateFrom", ToDbNullableDate(parts[1])),
                new SqlParameter("@DateTo", ToDbNullableDate(parts[2])));
        }

        private async Task<string> HandleReportOrdersByClientAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:REPORT_ORDERS_BY_CLIENT_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.ReportOrdersByClient",
                "OK:REPORT|",
                "ERR:REPORT_ORDERS_BY_CLIENT|",
                new SqlParameter("@ClientId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleReportOrdersByProductAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:REPORT_ORDERS_BY_PRODUCT_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.ReportOrdersByProduct",
                "OK:REPORT|",
                "ERR:REPORT_ORDERS_BY_PRODUCT|",
                new SqlParameter("@ProductId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleStoresGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.StoresGetAll", "OK:STORES|", "ERR:STORES|");
        }

        private async Task<string> HandleSuppliersGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.SuppliersGetAll", "OK:SUPPLIERS|", "ERR:SUPPLIERS|");
        }

        private async Task<string> HandleStockGetAllAsync(string[] parts)
        {
            object storeId = DBNull.Value;
            object productId = DBNull.Value;

            if (parts.Length > 1)
                storeId = ToDbNullableInt(parts[1]);

            if (parts.Length > 2)
                productId = ToDbNullableInt(parts[2]);

            return await QueryProcedureAsJsonAsync(
                "dbo.StockGetAll",
                "OK:STOCK|",
                "ERR:STOCK|",
                new SqlParameter("@StoreId", storeId),
                new SqlParameter("@ProductId", productId));
        }

        private async Task<string> HandleOrderHeadsGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.OrderHeadsGetAll", "OK:ORDERHEADS|", "ERR:ORDERHEADS|");
        }

        private async Task<string> HandleOrderSpecsGetByHeadAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:ORDERSPECS_GET_BY_HEAD_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.OrderSpecsGetByHead",
                "OK:ORDERSPECS|",
                "ERR:ORDERSPECS|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleOrderHeadAddAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:ORDERHEAD_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.OrderHeadAdd",
                "orderheadid",
                "OK:ORDERHEAD_ADD|",
                "ERR:ORDERHEAD_ADD|",
                new SqlParameter("@StoreId", ParseIntRequired(parts[1])),
                new SqlParameter("@SupplierId", ParseIntRequired(parts[2])));
        }

        private async Task<string> HandleOrderSpecAddAsync(string[] parts)
        {
            if (parts.Length < 5)
                return "ERR:ORDERSPEC_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.OrderSpecAdd",
                "orderspecid",
                "OK:ORDERSPEC_ADD|",
                "ERR:ORDERSPEC_ADD|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])),
                new SqlParameter("@NomenId", ParseIntRequired(parts[2])),
                new SqlParameter("@Quant", ParseDecimalRequired(parts[3])),
                new SqlParameter("@Price", ParseDecimalRequired(parts[4])));
        }

        private async Task<string> HandleOrderSpecDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:ORDERSPEC_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.OrderSpecDelete",
                "OK:ORDERSPEC_DELETE",
                "ERR:ORDERSPEC_DELETE|",
                new SqlParameter("@SpecId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleOrderHeadAcceptAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:ORDERHEAD_ACCEPT_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.OrderHeadAccept",
                "OK:ORDERHEAD_ACCEPT",
                "ERR:ORDERHEAD_ACCEPT|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleSaleHeadsGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.SaleHeadsGetAll", "OK:SALEHEADS|", "ERR:SALEHEADS|");
        }

        private async Task<string> HandleSaleSpecsGetByHeadAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:SALESPECS_GET_BY_HEAD_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.SaleSpecsGetByHead",
                "OK:SALESPECS|",
                "ERR:SALESPECS|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleSaleHeadAddAsync(string[] parts)
        {
            if (parts.Length < 4)
                return "ERR:SALEHEAD_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.SaleHeadAdd",
                "saleheadid",
                "OK:SALEHEAD_ADD|",
                "ERR:SALEHEAD_ADD|",
                new SqlParameter("@StoreId", ParseIntRequired(parts[1])),
                new SqlParameter("@ManagerId", ParseIntRequired(parts[2])),
                new SqlParameter("@ClientId", ParseIntRequired(parts[3])));
        }

        private async Task<string> HandleSaleSpecAddAsync(string[] parts)
        {
            if (parts.Length < 4)
                return "ERR:SALESPEC_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.SalespecAdd",
                "salespecid",
                "OK:SALESPEC_ADD|",
                "ERR:SALESPEC_ADD|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])),
                new SqlParameter("@NomenId", ParseIntRequired(parts[2])),
                new SqlParameter("@Quant", ParseDecimalRequired(parts[3])),
                new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output });
        }

        private async Task<string> HandleSaleSpecDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:SALESPEC_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.SaleSpecDelete",
                "OK:SALESPEC_DELETE",
                "ERR:SALESPEC_DELETE|",
                new SqlParameter("@SpecId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleSaleHeadProcessAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:SALEHEAD_PROCESS_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.SaleHeadProcess",
                "OK:SALEHEAD_PROCESS",
                "ERR:SALEHEAD_PROCESS|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleTransferHeadsGetAllAsync()
        {
            return await QueryProcedureAsJsonAsync("dbo.TransferHeadsGetAll", "OK:TRANSFERHEADS|", "ERR:TRANSFERHEADS|");
        }

        private async Task<string> HandleTransferSpecsGetByHeadAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:TRANSFERSPECS_GET_BY_HEAD_FORMAT";

            return await QueryProcedureAsJsonAsync(
                "dbo.TransferSpecsGetByHead",
                "OK:TRANSFERSPECS|",
                "ERR:TRANSFERSPECS|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleTransferHeadAddAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:TRANSFERHEAD_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.TransferHeadAdd",
                "transferheadid",
                "OK:TRANSFERHEAD_ADD|",
                "ERR:TRANSFERHEAD_ADD|",
                new SqlParameter("@StoreOutId", ParseIntRequired(parts[1])),
                new SqlParameter("@StoreInId", ParseIntRequired(parts[2])));
        }

        private async Task<string> HandleTransferSpecAddAsync(string[] parts)
        {
            if (parts.Length < 4)
                return "ERR:TRANSFERSPEC_ADD_FORMAT";

            return await ExecuteProcedureReturningIdAsync(
                "dbo.TransferSpecAdd",
                "transferspecid",
                "OK:TRANSFERSPEC_ADD|",
                "ERR:TRANSFERSPEC_ADD|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])),
                new SqlParameter("@NomenId", ParseIntRequired(parts[2])),
                new SqlParameter("@Quant", ParseDecimalRequired(parts[3])));
        }

        private async Task<string> HandleTransferSpecDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:TRANSFERSPEC_DELETE_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.TransferSpecDelete",
                "OK:TRANSFERSPEC_DELETE",
                "ERR:TRANSFERSPEC_DELETE|",
                new SqlParameter("@SpecId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleTransferHeadSendAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:TRANSFERHEAD_SEND_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.TransferHeadSend",
                "OK:TRANSFERHEAD_SEND",
                "ERR:TRANSFERHEAD_SEND|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleTransferHeadAcceptAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:TRANSFERHEAD_ACCEPT_FORMAT";

            return await ExecuteProcedureAsync(
                "dbo.TransferHeadAccept",
                "OK:TRANSFERHEAD_ACCEPT",
                "ERR:TRANSFERHEAD_ACCEPT|",
                new SqlParameter("@HeadId", ParseIntRequired(parts[1])));
        }

        private async Task<string> HandleManagerStockGetAsync(string[] parts)
        {
            try
            {
                string? storePart = parts.Length > 1 ? parts[1] : null;
                string? productPart = parts.Length > 2 ? parts[2] : null;

                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ManagerStockGet",
                    new SqlParameter("@StoreId", ToDbNullableInt(storePart)),
                    new SqlParameter("@ProductId", ToDbNullableInt(productPart)));

                return "OK:MANAGER_STOCK|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения остатков менеджера: " + ex.Message);
                return "ERR:MANAGER_STOCK|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSaleHeadsGetAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:MANAGER_SALEHEADS_GET_FORMAT";

            try
            {
                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ManagerSaleHeadsGetByManager",
                    new SqlParameter("@ManagerId", ParseIntRequired(parts[1])));

                return "OK:MANAGER_SALEHEADS|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения заказов менеджера: " + ex.Message);
                return "ERR:MANAGER_SALEHEADS|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSaleSpecsGetAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:MANAGER_SALESPECS_GET_FORMAT";

            try
            {
                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ManagerSaleSpecsGetByHead",
                    new SqlParameter("@HeadId", ParseIntRequired(parts[1])));

                return "OK:MANAGER_SALESPECS|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения строк заказа менеджера: " + ex.Message);
                return "ERR:MANAGER_SALESPECS|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSaleHeadAddAsync(string[] parts)
        {
            if (parts.Length < 4)
                return "ERR:MANAGER_SALEHEAD_ADD_FORMAT";

            try
            {
                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ManagerSaleHeadAdd",
                    new SqlParameter("@StoreId", ParseIntRequired(parts[1])),
                    new SqlParameter("@ManagerId", ParseIntRequired(parts[2])),
                    new SqlParameter("@ClientId", ParseIntRequired(parts[3])));

                string saleHeadId = dt.Rows.Count > 0 ? dt.Rows[0]["saleheadid"]?.ToString() ?? "" : "";
                return "OK:MANAGER_SALEHEAD_ADD|" + saleHeadId;
            }
            catch (Exception ex)
            {
                AddLog("Ошибка создания заказа менеджера: " + ex.Message);
                return "ERR:MANAGER_SALEHEAD_ADD|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSalespecAddAsync(string[] parts)
        {
            if (parts.Length < 4)
                return "ERR:MANAGER_SALESPEC_ADD_FORMAT";

            try
            {
                decimal quant = decimal.Parse(parts[3], CultureInfo.InvariantCulture);

                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ManagerSalespecAdd",
                    new SqlParameter("@HeadId", ParseIntRequired(parts[1])),
                    new SqlParameter("@NomenId", ParseIntRequired(parts[2])),
                    new SqlParameter("@Quant", quant));

                string specId = dt.Rows.Count > 0 ? dt.Rows[0]["salespecid"]?.ToString() ?? "" : "";
                return "OK:MANAGER_SALESPEC_ADD|" + specId;
            }
            catch (Exception ex)
            {
                AddLog("Ошибка добавления строки заказа менеджера: " + ex.Message);
                return "ERR:MANAGER_SALESPEC_ADD|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSalespecDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:MANAGER_SALESPEC_DELETE_FORMAT";

            try
            {
                DBConnection db = CreateDb();
                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.ManagerSalespecDelete",
                    new SqlParameter("@SpecId", ParseIntRequired(parts[1])));

                return "OK:MANAGER_SALESPEC_DELETE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка удаления строки заказа менеджера: " + ex.Message);
                return "ERR:MANAGER_SALESPEC_DELETE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleManagerSaleHeadCloseAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:MANAGER_SALEHEAD_CLOSE_FORMAT";

            try
            {
                DBConnection db = CreateDb();
                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.ManagerSaleHeadClose",
                    new SqlParameter("@HeadId", ParseIntRequired(parts[1])));

                return "OK:MANAGER_SALEHEAD_CLOSE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка закрытия заказа менеджера: " + ex.Message);
                return "ERR:MANAGER_SALEHEAD_CLOSE|" + NormalizeMessage(ex.Message);
            }
        }

        private DBConnection CreateDb()
        {
            return new DBConnection(_connectionString);
        }

        private async Task<string> QueryProcedureAsJsonAsync(
            string procedureName,
            string successPrefix,
            string errorPrefix,
            params SqlParameter[] parameters)
        {
            try
            {
                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(procedureName, parameters);
                return successPrefix + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog($"Ошибка процедуры {procedureName}: {ex.Message}");
                return errorPrefix + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> ExecuteProcedureAsync(
            string procedureName,
            string successResponse,
            string errorPrefix,
            params SqlParameter[] parameters)
        {
            try
            {
                DBConnection db = CreateDb();
                await db.ExecuteProcedureNonQueryAsync(procedureName, parameters);
                return successResponse;
            }
            catch (Exception ex)
            {
                AddLog($"Ошибка процедуры {procedureName}: {ex.Message}");
                return errorPrefix + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> ExecuteProcedureReturningIdAsync(
            string procedureName,
            string idColumnName,
            string successPrefix,
            string errorPrefix,
            params SqlParameter[] parameters)
        {
            try
            {
                DBConnection db = CreateDb();
                DataTable dt = await db.ExecuteProcedureQueryAsync(procedureName, parameters);

                string idValue = "";
                if (dt.Rows.Count > 0 && dt.Columns.Contains(idColumnName))
                    idValue = dt.Rows[0][idColumnName]?.ToString() ?? "";

                return successPrefix + idValue;
            }
            catch (Exception ex)
            {
                AddLog($"Ошибка процедуры {procedureName}: {ex.Message}");
                return errorPrefix + NormalizeMessage(ex.Message);
            }
        }

        private object ToDbNullableString(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
        }

        private object ToDbNullableInt(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            if (int.TryParse(value, out int number))
                return number;

            throw new Exception("Ожидалось целое число.");
        }

        private object ToDbNullableDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            if (DateTime.TryParse(value, out DateTime dt))
                return dt;

            throw new Exception("Некорректная дата.");
        }

        private int ParseIntRequired(string? value)
        {
            if (!int.TryParse(value, out int result))
                throw new Exception("Ожидалось целое число.");

            return result;
        }

        private decimal ParseDecimalRequired(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Ожидалось число.");

            string normalized = value.Replace(',', '.');

            if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                return result;

            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result))
                return result;

            throw new Exception("Ожидалось число.");
        }

        private string TableToJson(DataTable table)
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                foreach (DataColumn col in table.Columns)
                    item[col.ColumnName] = row[col]?.ToString() ?? "";

                rows.Add(item);
            }

            return JsonSerializer.Serialize(rows);
        }

        private string NormalizeMessage(string message)
        {
            return message.Replace("\r", " ").Replace("\n", " ");
        }

        private void StopServer()
        {
            try
            {
                if (!_serverStarted)
                    return;

                _stopSource?.Cancel();
                _listener?.Stop();

                _stopSource?.Dispose();
                _stopSource = null;
                _listener = null;

                _serverStarted = false;
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                textBoxPort.Enabled = true;

                AddLog("Сервер остановлен.");
            }
            catch (Exception ex)
            {
                AddLog("Ошибка остановки сервера: " + ex.Message);
            }
        }

        private void AddLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddLog), message);
                return;
            }

            textBoxLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        private void ServerForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }
}
