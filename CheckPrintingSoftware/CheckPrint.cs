using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CheckPrintingSoftware.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CheckPrintingSoftware
{
    public partial class CheckPrint : Form
    {
        public CheckPrint()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox3.ReadOnly = true;
            dateTimePicker1.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve data from text boxes
            string payTo = textBox2.Text;
            string sumOfTaka = textBox3.Text;
            string amount = textBox5.Text;
            DateTime date = dateTimePicker2.Value;

            // Connection string retrieved from app.config
            string connectionString = Properties.Settings.Default.CheckPrintingSoftwareConnectionString;

            // SQL query to insert data into the Check_Print table
            string query = "INSERT INTO Check_Print (Pay_to, Sum_of_Taka, Amount, [Date]) VALUES (@payTo, @sumOfTaka, @amount, @date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@payTo", payTo);
                command.Parameters.AddWithValue("@sumOfTaka", sumOfTaka);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", date);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully");
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = textBox5.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string numericText = new string(textBox5.Text.Where(char.IsDigit).ToArray());

            string wordValue = ConvertNumberToWords(numericText);

            textBox3.Text = wordValue;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private string ConvertNumberToWords(string numericText)
        {
            if (!string.IsNullOrEmpty(numericText))
            {
                if (decimal.TryParse(numericText, out decimal numericValue))
                {
                    return NumberToWords(numericValue);
                }
                else
                {
                    return "Invalid input";
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private string NumberToWords(decimal number)
        {
            if (number == 0)
            {
                return "Zero";
            }

            if (number < 0)
            {
                return "Minus " + NumberToWords(Math.Abs(number));
            }

            string words = "";

            long wholePart = (long)Math.Truncate(number);
            long decimalPart = (long)((number - wholePart) * 100); // Assuming 2 decimal places

            words += ConvertWholeNumberToWords(wholePart);

            if (decimalPart > 0)
            {
                words += " and " + ConvertWholeNumberToWords(decimalPart) + " Cents";
            }

            return words;
        }

        private string ConvertWholeNumberToWords(long number)
        {
            string words = "";

            if (number >= 1000000)
            {
                words += ConvertWholeNumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if (number >= 1000)
            {
                words += ConvertWholeNumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if (number >= 100)
            {
                words += ConvertWholeNumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += "and ";
                }

                string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
                string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 10)
                {
                    words += units[number];
                }
                else if (number < 20)
                {
                    words += teens[number - 10];
                }
                else
                {
                    words += tens[number / 10];
                    if (number % 10 > 0)
                    {
                        words += "-" + units[number % 10];
                    }
                }
            }

            return words;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string payTo = textBox2.Text;
            string sumOfTaka = textBox3.Text;
            string amount = textBox5.Text;
            DateTime date = dateTimePicker2.Value;

            //// Set parameter values
            //reportDocument.SetParameterValue("payTo", payTo);
            //reportDocument.SetParameterValue("sumOfTaka", sumOfTaka);
            //reportDocument.SetParameterValue("amount", amount);
            //reportDocument.SetParameterValue("date", date);

            //// Show the Crystal Report
            //crystalReportViewerCheque_Print crystalReportViewerForm = new crystalReportViewerCheque_Print();
            ////crystalReportViewerForm.crystalReportViewer.ReportSource = reportDocument;
            //crystalReportViewerForm.Show();

            MessageBox.Show("Successfully");

        }
    }
}
