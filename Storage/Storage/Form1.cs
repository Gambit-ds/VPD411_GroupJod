using DBQwery;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace Storage
{
    public partial class Form1 : Form
    {
        private string conn = @"Data Source=DESKTOP-LFJHKRS\SQLEXPRESS; Initial Catalog = Storag; Integrated Security = true; Connect Timeout = 30; TrustServerCertificate = true";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string[] parts = tbQuery.Text.Split(':');
            if (parts[0] == "SELECT") //обычный селект (можно с параметрами)
            {
                DBConnection db = new DBConnection(conn);
                string query = parts[1];
                DataTable dt = await db.ExecuteQueryAsync(query);

                dataGridView1.DataSource = dt;
            }
            else if (parts[0] == "INS") //Операции для изменения записей
            {
                DBConnection db = new DBConnection(conn);
                string query = $"INSERT INTO Roles (code) VALUES (@code)";
                int rows = await db.ExecuteNonQueryAsync(query, new SqlParameter("@code", parts[2]));
                label1.Text = $"Добавлено строк: {rows.ToString()}";
            }
            else if (parts[0] == "UPD")
            {
                DBConnection db = new DBConnection(conn);
                string query = $"UPDATE Roles set name = @name WHERE id = @id";
                int rows = await db.ExecuteNonQueryAsync(query, new SqlParameter("@name", parts[2]), new SqlParameter("@id", parts[3]));
                label1.Text = $"Измененых строк: {rows.ToString()}";
            }
            else if (parts[0] == "DEL")
            {
                DBConnection db = new DBConnection(conn);
                string query = $"DELETE FROM Roles WHERE id = @id";
                int rows = await db.ExecuteNonQueryAsync(query, new SqlParameter("@id", parts[2]));
                label1.Text = $"Удалено строк: {rows.ToString()}";
            }
            else if (parts[0] == "PROC") //Выполнения хранимой процедуры
            {
                DBConnection dbCon = new DBConnection(conn);
                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output //выходной параметр из БД
                };
                await dbCon.ExecuteProcedureNonQueryAsync("CreateUser",
                new SqlParameter("@Name", parts[2]),
                new SqlParameter("@Fname", parts[1]),
                new SqlParameter("@Sname", parts[3]),
                new SqlParameter("@Login", parts[5]),
                new SqlParameter("@Password", parts[6]),
                new SqlParameter("@Roleid", parts[4]),
                idParam);

                int newUserId = (int)idParam.Value;

                label1.Text = $"Добавлен новый пользователь с id = {newUserId}";
            }
            else //Скалярное выражение. Возврашает одно значение.
            {
                DBConnection db = new DBConnection(conn);
                var count = await db.ExecuteScalarAsync("SELECT COUNT(*) FROM Users");
                label1.Text = $"Кол-во пользователей в БД: {count}";
            }
        }
    }
}
