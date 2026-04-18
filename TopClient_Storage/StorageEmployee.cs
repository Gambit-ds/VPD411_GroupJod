using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopClient_Storage
{
    public partial class StorageEmployee : Form
    {
        private readonly TcpClient _client;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;
        public StorageEmployee(TcpClient client, StreamReader reader, StreamWriter writer)
        {
            InitializeComponent();

            _client = client;
            _reader = reader;
            _writer = writer;
        }

        private async void StorageEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_client.Connected)
                {
                    // Корректно сообщаем серверу о завершении сеанса
                    await _writer.WriteLineAsync("QUIT");
                    await _reader.ReadLineAsync();
                }
            }
            catch
            {
                // Если соединение уже разорвано, просто завершаем закрытие
            }
            finally
            {
                _reader.Dispose();
                _writer.Dispose();
                _client.Close();
            }
        }
    }
}
