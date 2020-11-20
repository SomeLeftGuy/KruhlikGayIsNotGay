using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace KursachCollege    
{
    public partial class Registration : Form
    {
       // private MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        public Registration()
        {
            InitializeComponent();
        }
        static string connectionString =
                "Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=KursachCollege;" +
                "Integrated Security=True";
        public MySqlConnection con = new MySqlConnection(connectionString);
        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            MySqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText ="select * from Users where username = '" + textBox1.Text+"'and password'" + textBox2.Text + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {
                if (radioButton1.Checked)
                {
                    InsertAuto(textBox1.Text, textBox2.Text);
                }
                else
                {
                    MessageBox.Show("Неправильно указано имя пользователя или пароль");
                }
            }
            else
            {
               // this.Hide();
              //  Form1 form = new Form1();
               // form.Show();
            }
        }
        public void InsertAuto(string username, string password)
        {
            con.Open();
            // Оператор SQL
            string sql = string.Format("Insert Into Users" +
                   "(username, password) Values(@username, @password)");

            using (MySqlCommand cmd = new MySqlCommand(sql, this.con))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
        }
        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
