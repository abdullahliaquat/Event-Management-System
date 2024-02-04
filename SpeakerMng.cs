using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FInal_DB__Project
{
    public partial class SpeakerMng : Form
    {
        OracleConnection con;
        public SpeakerMng()
        {
            InitializeComponent();
            this.Load += SpeakerMng_load;

        }
        private void SpeakerMng_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }
        private void updateGrid()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                // Assuming you have a table named "YourTableName" in your database
                string query = "SELECT * FROM SPEAKER";
                OracleDataAdapter adapter = new OracleDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                //dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private string GetAvailable()
        {
            // Implement logic to get the selected payment method from the checked RadioButton
            foreach (System.Windows.Forms.RadioButton radioButton in groupBox1.Controls.OfType<System.Windows.Forms.RadioButton>())
            {
                if (radioButton.Checked)
                {
                    // Add debugging statements
                    Console.WriteLine($"Selected RadioButton: {radioButton.Name}, Text: {radioButton.Text}");
                    return radioButton.Text; // Assuming RadioButton text is the payment method
                }
            }
            return string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || !IsAnyChecked(radioButton1, radioButton2) || comboBox1.SelectedIndex == -1)
                {
                    // Show an error message if any of the fields is missing
                    MessageBox.Show("Fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                try
                {
                    con.Open();

                    // Assuming you have a table named "Speaker" in your database
                    string insertQuery = "INSERT INTO Speaker (SpeakerID, Name, EventDate, AssignToSession, Available) VALUES (:speakerID, :name, :eventDate, :assignToSession, :available)";

                    // Create OracleCommand and set the parameters
                    OracleCommand cmd = new OracleCommand(insertQuery, con);
                    cmd.Parameters.Add(":speakerID", OracleDbType.Int32).Value = GenerateRandomSpeakerID(); // Generate random SpeakerID
                    cmd.Parameters.Add(":name", OracleDbType.Varchar2, 100).Value = textBox1.Text;
                    cmd.Parameters.Add(":eventDate", OracleDbType.Date).Value = DateTime.Now; // Set the actual date as per your requirement
                    cmd.Parameters.Add(":assignToSession", OracleDbType.Varchar2, 100).Value = comboBox1.SelectedItem.ToString(); // Use the selected session from the ComboBox
                    cmd.Parameters.Add(":available", OracleDbType.Varchar2, 50).Value = GetAvailable(); // Use the selected payment method from RadioButton

                    // Execute the query
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                }
            }
            

        }

        private int GenerateRandomSpeakerID()
        {
            // Generate a random SpeakerID (you might need to adjust the range based on your requirements)
            Random random = new Random();
            return random.Next(1, 1000); // Replace 1000 with the maximum range you want to allow
        }
        // Helper function to check if at least one radio button in a group is selected
        private bool IsAnyChecked(params System.Windows.Forms.RadioButton[] radioButtons)
        {
            foreach (System.Windows.Forms.RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked)
                {
                    return true;
                }
            }
            return false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || comboBox1.SelectedIndex == -1)
            {
                // Show an error message if any of the required fields is missing
                MessageBox.Show("Enter both Speaker Name and select Session to perform deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Assuming 'speakerName' is your TextBox control and 'sessionComboBox' is your ComboBox control
                    string speakerName = textBox1.Text;
                    // string session = comboBox1.SelectedItem.ToString();

                    // Assuming 'Speaker' is your table name and 'Name' and 'AssignToSession' are the corresponding columns
                    string deleteQuery = "DELETE FROM Speaker WHERE NAME = :speakerName";

                    using (OracleCommand cmd = new OracleCommand(deleteQuery, con))
                    {
                        cmd.Parameters.Add(":speakerName", OracleDbType.Varchar2, 100).Value = speakerName;
                        // cmd.Parameters.Add(":session", OracleDbType.Varchar2, 100).Value = session;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Update your grid or perform any additional actions
                            updateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Speaker with the specified Name and Session not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || comboBox1.SelectedIndex == -1)
            {
                // Show an error message if any of the required fields is missing
                MessageBox.Show("Enter both Speaker Name and select Session to perform deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Assuming 'speakerName' is your TextBox control
                    string speakerName = textBox1.Text;

                    // Assuming 'Speaker' is your table name and 'Name' is the corresponding column
                    string updateQuery = "UPDATE Speaker SET Available = :available WHERE NAME = :speakerName";

                    using (OracleCommand cmd = new OracleCommand(updateQuery, con))
                    {
                        cmd.Parameters.Add(":available", OracleDbType.Varchar2, 50).Value = GetAvailable();
                        cmd.Parameters.Add(":speakerName", OracleDbType.Varchar2, 100).Value = speakerName;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Update your grid or perform any additional actions
                            updateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Speaker with the specified Name not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

        }

        private void SpeakerMng_Load_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminManagement form = new AdminManagement();
            this.Hide();
            form.ShowDialog();
        }
    }
}
