using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckPrintingSoftware
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the entered ID and password from textBox1 and textBox2
            string enteredId = textBox1.Text;
            string enteredPassword = textBox2.Text;

            // Connection string retrieved from app.config
            string connectionString = Properties.Settings.Default.CheckPrintingSoftwareConnectionString;

            // SQL query to fetch the user's password from the database
            string query = "SELECT Password FROM [User] WHERE User_Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", enteredId);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null) // User with entered ID exists
                    {
                        string passwordFromDatabase = result.ToString().Trim();

                        // Check if the entered password matches the one stored in the database
                        if (enteredPassword == passwordFromDatabase)
                        {
                            MessageBox.Show("Login successful");

                            CheckPrint CheckPrint = new CheckPrint();
                            CheckPrint.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

    }
}
