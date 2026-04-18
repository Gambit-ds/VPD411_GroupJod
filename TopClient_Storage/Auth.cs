using System.Net.Sockets;
using System.Text;

namespace TopClient_Storage
{
    public partial class Auth : Form
    {
        private const string ServerHost = "127.0.0.1";
        private const int ServerPort = 5000;

        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public Auth()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Пустое поле логина.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пустое поле пароля.");
                return;
            }

            try
            {
                await EnsureConnectedAsync();

                string? response = await SendCommandAsync($"AUTH|{login}|{password}");

                if (string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show("Сервер не вернул ответ.");
                    return;
                }

                if (response == "ERR:AUTH")
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }

                if (!response.StartsWith("OK:AUTH|", StringComparison.Ordinal))
                {
                    MessageBox.Show("Ошибка авторизации: " + response);
                    return;
                }

                string[] parts = response.Split('|');

                if (parts.Length < 4)
                {
                    MessageBox.Show("Сервер вернул неполный ответ.");
                    return;
                }

                string userId = parts[1];
                string role = parts[2];
                string longName = parts[3];

                OpenRoleForm(role, longName, userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка связи с сервером: " + ex.Message);
            }
        }

        private void OpenRoleForm(string role, string longName, string userId)
        {
            if (_client == null || _reader == null || _writer == null)
            {
                MessageBox.Show("Нет активного соединения с сервером.");
                return;
            }

            Form nextForm;

            switch (role)
            {
                case "admin":
                    nextForm = new Administrator(_client, _reader, _writer);
                    break;

                case "manager":
                    nextForm = new Manager(_client, _reader, _writer);
                    break;

                case "user":
                    if (!int.TryParse(userId, out int employeeId))
                    {
                        MessageBox.Show("Сервер вернул неверный идентификатор пользователя.");
                        return;
                    }

                    nextForm = new StorageEmployee(_client, _reader, _writer, employeeId);
                    break;

                default:
                    MessageBox.Show("Неизвестная роль: " + role);
                    return;
            }

            nextForm.Text = $"{nextForm.Text} — {longName}";

            _client = null;
            _reader = null;
            _writer = null;

            nextForm.FormClosed += (s, e) => Close();

            Hide();
            nextForm.Show();
        }

        private async Task EnsureConnectedAsync()
        {
            if (_client != null && _client.Connected && _reader != null && _writer != null)
                return;

            _client = new TcpClient();
            await _client.ConnectAsync(ServerHost, ServerPort);

            NetworkStream stream = _client.GetStream();

            _reader = new StreamReader(stream, Encoding.UTF8, false, 4096, true);
            _writer = new StreamWriter(stream, Encoding.UTF8, 4096, true)
            {
                AutoFlush = true,
                NewLine = "\r\n"
            };

            string? hello = await _reader.ReadLineAsync();

            if (hello != "OK:CONNECTED")
                throw new Exception("Сервер вернул неверное приветствие: " + hello);
        }

        private async Task<string?> SendCommandAsync(string command)
        {
            if (_client == null || !_client.Connected || _reader == null || _writer == null)
                throw new Exception("Нет подключения к серверу.");

            await _writer.WriteLineAsync(command);
            return await _reader.ReadLineAsync();
        }
    }
}
