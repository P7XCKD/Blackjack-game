using Probz_Blackjack;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace blackjax
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Remove form border
            this.WindowState = FormWindowState.Maximized; // Maximize the form
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click if needed
        }

     /*   private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                Form2 ff = new Form2();
                ff.Show();
            }
            else if (e.KeyCode == Keys.S) // Check for 'S' key press
            {
                this.Hide();
                Form2 ff = new Form2();
                ff.Show();
            }
        }
     */
        private void LoadData()
        {
            // Connection string using the absolute path for demonstration
            string dbFilePath = @"C:\Users\DEV\source\repos\blackjax\Database1.mdf"; // Your absolute path
            string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbFilePath};Integrated Security=True;Connect Timeout=30";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT RoundNumber, RoundMessage FROM [dbo].[Table]"; // Adjust to your actual table name
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(dataTable);
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }

                // Set the DataGridView's DataSource
                dataGridView1.DataSource = dataTable;

                // Set the columns to automatically adjust their widths
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; // Adjusts to fit content
                // Alternatively, use DataGridViewAutoSizeColumnsMode.Fill to fill the available space
                // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadData(); // Load data into the DataGridView when the form is loaded
            dataGridView1.Dock = DockStyle.Fill; // Make the DataGridView fill the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ff = new Form2();
            ff.Show();
        }
    }
}
