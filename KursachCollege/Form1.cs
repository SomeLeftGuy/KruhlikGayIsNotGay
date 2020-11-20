using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace KursachCollege
{
    public partial class Form1 : Form
    {
    private BindingSource bindingSource1 = new BindingSource();
    private SqlDataAdapter dataAdapter = new SqlDataAdapter();
    private Button reloadButton = new Button();
    private Button submitButton = new Button();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Dock = DockStyle.Fill;
            reloadButton.Text = "Reload";
            submitButton.Text = "Submit";
            reloadButton.Click += new EventHandler(button1_Click);
            submitButton.Click += new EventHandler(button2_Click);

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true
            };
            panel.Controls.AddRange(new Control[] { reloadButton, submitButton });

            Controls.AddRange(new Control[] { dataGridView1, panel });
            Load += new EventHandler(Form1_Load);
            Text = "Automatization of bus station";
        }
        private void GetData(string selectCommand)
        {
            try
            {
                // Specify a connection string.
                String connectionString =
                    "Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=KursachCollege;" +
                    "Integrated Security=True";

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.Fill);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong. Pleas, try again. " + ex.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Bind the DataGridView to the BindingSource
            // and load the data from the database.
            dataGridView1.DataSource = bindingSource1;
            GetData("select * from Names");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reload the data from the database.
            GetData(dataAdapter.SelectCommand.CommandText);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Update the database with changes.
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
