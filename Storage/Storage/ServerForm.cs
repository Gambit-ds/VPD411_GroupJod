using DBQwery;
using Microsoft.Data.SqlClient;
using System.Data;
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

        // Строка подключения сервера к базе данных.
        private readonly string _connectionString =
            @"Data Source=DESKTOP-DF3LQAG\SQLEXPRESS;Initial Catalog=Storag;Integrated Security=true;Connect Timeout=30;TrustServerCertificate=true";

        public ServerForm()
        {
            InitializeComponent();
            this.FormClosing += ServerForm_FormClosing;
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
                DBConnection db = new DBConnection(_connectionString);

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
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.RolesGetAll");
                return "OK:ROLES|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения ролей: " + ex.Message);
                return "ERR:ROLES";
            }
        }

        private async Task<string> HandleUsersGetAllAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.UsersGetAll");
                return "OK:USERS|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения пользователей: " + ex.Message);
                return "ERR:USERS";
            }
        }

        private async Task<string> HandleUserAddAsync(string[] parts)
        {
            if (parts.Length < 7)
                return "ERR:USER_ADD_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.UserAdd",
                    new SqlParameter("@Name", parts[1]),
                    new SqlParameter("@Sname", ToDbNullableString(parts[2])),
                    new SqlParameter("@Fname", ToDbNullableString(parts[3])),
                    new SqlParameter("@RoleId", int.Parse(parts[4])),
                    new SqlParameter("@Login", parts[5]),
                    new SqlParameter("@Password", parts[6]));

                string userId = dt.Rows.Count > 0 ? dt.Rows[0]["userid"]?.ToString() ?? "" : "";
                return "OK:USER_ADD|" + userId;
            }
            catch (Exception ex)
            {
                AddLog("Ошибка добавления пользователя: " + ex.Message);
                return "ERR:USER_ADD|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleUserUpdateAsync(string[] parts)
        {
            if (parts.Length < 8)
                return "ERR:USER_UPDATE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.UserUpdate",
                    new SqlParameter("@UserId", int.Parse(parts[1])),
                    new SqlParameter("@Name", parts[2]),
                    new SqlParameter("@Sname", ToDbNullableString(parts[3])),
                    new SqlParameter("@Fname", ToDbNullableString(parts[4])),
                    new SqlParameter("@RoleId", int.Parse(parts[5])),
                    new SqlParameter("@Login", parts[6]),
                    new SqlParameter("@Password", ToDbNullableString(parts[7])));

                return "OK:USER_UPDATE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка изменения пользователя: " + ex.Message);
                return "ERR:USER_UPDATE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleUserDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:USER_DELETE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.UserDelete",
                    new SqlParameter("@UserId", int.Parse(parts[1])));

                return "OK:USER_DELETE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка удаления пользователя: " + ex.Message);
                return "ERR:USER_DELETE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleCategoriesGetAllAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.CategoriesGetAll");
                return "OK:CATEGORIES|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения категорий: " + ex.Message);
                return "ERR:CATEGORIES";
            }
        }

        private async Task<string> HandleCategoryAddAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:CATEGORY_ADD_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.CategoryAdd",
                    new SqlParameter("@Code", parts[1]));

                string categoryId = dt.Rows.Count > 0 ? dt.Rows[0]["categoryid"]?.ToString() ?? "" : "";
                return "OK:CATEGORY_ADD|" + categoryId;
            }
            catch (Exception ex)
            {
                AddLog("Ошибка добавления категории: " + ex.Message);
                return "ERR:CATEGORY_ADD|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleCategoryUpdateAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:CATEGORY_UPDATE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.CategoryUpdate",
                    new SqlParameter("@CategoryId", int.Parse(parts[1])),
                    new SqlParameter("@Code", parts[2]));

                return "OK:CATEGORY_UPDATE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка изменения категории: " + ex.Message);
                return "ERR:CATEGORY_UPDATE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleCategoryDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:CATEGORY_DELETE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.CategoryDelete",
                    new SqlParameter("@CategoryId", int.Parse(parts[1])));

                return "OK:CATEGORY_DELETE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка удаления категории: " + ex.Message);
                return "ERR:CATEGORY_DELETE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleProductsGetAllAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.ProductsGetAll");
                return "OK:PRODUCTS|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения товаров: " + ex.Message);
                return "ERR:PRODUCTS";
            }
        }

        private async Task<string> HandleProductsShortGetAllAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.ProductsShortGetAll");
                return "OK:PRODUCTS_SHORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения краткого списка товаров: " + ex.Message);
                return "ERR:PRODUCTS_SHORT";
            }
        }

        private async Task<string> HandleProductAddAsync(string[] parts)
        {
            if (parts.Length < 6)
                return "ERR:PRODUCT_ADD_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ProductAdd",
                    new SqlParameter("@CategoryId", int.Parse(parts[1])),
                    new SqlParameter("@Code", parts[2]),
                    new SqlParameter("@Description", ToDbNullableString(parts[3])),
                    new SqlParameter("@Weight", ToDbNullableInt(parts[4])),
                    new SqlParameter("@Size", ToDbNullableInt(parts[5])));

                string productId = dt.Rows.Count > 0 ? dt.Rows[0]["productid"]?.ToString() ?? "" : "";
                return "OK:PRODUCT_ADD|" + productId;
            }
            catch (Exception ex)
            {
                AddLog("Ошибка добавления товара: " + ex.Message);
                return "ERR:PRODUCT_ADD|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleProductUpdateAsync(string[] parts)
        {
            if (parts.Length < 7)
                return "ERR:PRODUCT_UPDATE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.ProductUpdate",
                    new SqlParameter("@ProductId", int.Parse(parts[1])),
                    new SqlParameter("@CategoryId", int.Parse(parts[2])),
                    new SqlParameter("@Code", parts[3]),
                    new SqlParameter("@Description", ToDbNullableString(parts[4])),
                    new SqlParameter("@Weight", ToDbNullableInt(parts[5])),
                    new SqlParameter("@Size", ToDbNullableInt(parts[6])));

                return "OK:PRODUCT_UPDATE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка изменения товара: " + ex.Message);
                return "ERR:PRODUCT_UPDATE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleProductDeleteAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:PRODUCT_DELETE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);

                await db.ExecuteProcedureNonQueryAsync(
                    "dbo.ProductDelete",
                    new SqlParameter("@ProductId", int.Parse(parts[1])));

                return "OK:PRODUCT_DELETE";
            }
            catch (Exception ex)
            {
                AddLog("Ошибка удаления товара: " + ex.Message);
                return "ERR:PRODUCT_DELETE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleClientsGetAllAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.ClientsGetAll");
                return "OK:CLIENTS|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка чтения клиентов: " + ex.Message);
                return "ERR:CLIENTS";
            }
        }

        private async Task<string> HandleReportStockAsync()
        {
            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync("dbo.ReportStock");
                return "OK:REPORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка отчета по остаткам: " + ex.Message);
                return "ERR:REPORT_STOCK|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleReportMovementAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:REPORT_MOVEMENT_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ReportMovement",
                    new SqlParameter("@DateFrom", ToDbNullableDate(parts[1])),
                    new SqlParameter("@DateTo", ToDbNullableDate(parts[2])));

                return "OK:REPORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка отчета по движению: " + ex.Message);
                return "ERR:REPORT_MOVEMENT|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleReportOrdersByDateAsync(string[] parts)
        {
            if (parts.Length < 3)
                return "ERR:REPORT_ORDERS_BY_DATE_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ReportOrdersByDate",
                    new SqlParameter("@DateFrom", ToDbNullableDate(parts[1])),
                    new SqlParameter("@DateTo", ToDbNullableDate(parts[2])));

                return "OK:REPORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка отчета по заказам за период: " + ex.Message);
                return "ERR:REPORT_ORDERS_BY_DATE|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleReportOrdersByClientAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:REPORT_ORDERS_BY_CLIENT_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ReportOrdersByClient",
                    new SqlParameter("@ClientId", int.Parse(parts[1])));

                return "OK:REPORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка отчета по клиенту: " + ex.Message);
                return "ERR:REPORT_ORDERS_BY_CLIENT|" + NormalizeMessage(ex.Message);
            }
        }

        private async Task<string> HandleReportOrdersByProductAsync(string[] parts)
        {
            if (parts.Length < 2)
                return "ERR:REPORT_ORDERS_BY_PRODUCT_FORMAT";

            try
            {
                DBConnection db = new DBConnection(_connectionString);
                DataTable dt = await db.ExecuteProcedureQueryAsync(
                    "dbo.ReportOrdersByProduct",
                    new SqlParameter("@ProductId", int.Parse(parts[1])));

                return "OK:REPORT|" + TableToJson(dt);
            }
            catch (Exception ex)
            {
                AddLog("Ошибка отчета по товару: " + ex.Message);
                return "ERR:REPORT_ORDERS_BY_PRODUCT|" + NormalizeMessage(ex.Message);
            }
        }

        private object ToDbNullableString(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
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

        private string TableToJson(DataTable table)
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();

                foreach (DataColumn col in table.Columns)
                {
                    item[col.ColumnName] = row[col]?.ToString() ?? "";
                }

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