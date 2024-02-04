using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Oracle.ManagedDataAccess.Client;

namespace FInal_DB__Project
{
    
    public partial class ParticipantMng : Form
    {
        OracleConnection con;
        public ParticipantMng()
        {
            InitializeComponent();
            this.Load += ParticipantMng_load;

        }
        private void ParticipantMng_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }

        private void updateGrid()
        {
            try
            {
                con.Open();

                // Assuming you have a table named "YourTableName" in your database
                string query = "SELECT * FROM MANAGE_PARTICIPANT";
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if at least one option is selected from each group
            if (!IsAnyChecked(radioButton1, radioButton2, radioButton3, radioButton4) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 ||
                comboBox2.SelectedIndex == -1 ||
                !checkBox1.Checked)
            {
                // Show an error message if any of the fields is missing
                MessageBox.Show("Fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Assuming you have a table named "Manage_Participant" in your database
                    string insertQuery = "INSERT INTO Manage_Participant (ParticipantID, P_fname, P_lname, RegStatus, Attendance, PaymentMethod) " +
                        "VALUES (:participantID, :pFname, :pLname, :regStatus, :attendance, :paymentMethod)";

                    // Create OracleCommand and set the parameters
                    OracleCommand cmd = new OracleCommand(insertQuery, con);
                    cmd.Parameters.Add(":participantID", OracleDbType.Int32).Value = GenerateRandomParticipantID(); // Generate random ParticipantID
                    cmd.Parameters.Add(":pFname", OracleDbType.Varchar2, 50).Value = textBox1.Text;
                    cmd.Parameters.Add(":pLname", OracleDbType.Varchar2, 50).Value = textBox2.Text;
                    cmd.Parameters.Add(":regStatus", OracleDbType.Varchar2, 20).Value = comboBox1.SelectedItem.ToString(); // Use the selected registration status from ComboBox
                    cmd.Parameters.Add(":attendance", OracleDbType.Varchar2, 20).Value = comboBox2.SelectedItem.ToString(); // Use the selected attendance status from ComboBox
                    cmd.Parameters.Add(":paymentMethod", OracleDbType.Varchar2, 50).Value = GetSelectedPaymentMethod() ; // Use the selected payment method from RadioButton

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

        
        private int GenerateRandomParticipantID()
        {
            // Generate a random ParticipantID (you might need to adjust the range based on your requirements)
            Random random = new Random();
            return random.Next(1, 1000); // Replace 1000 with the maximum range you want to allow
        }

        private string GetSelectedPaymentMethod()
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


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ParticipantMng_Load_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                // Assuming 'eventName' and 'eventDate' are TextBox and DateTimePicker controls, replace them with your actual controls
                string fname = textBox1.Text;
                string lname = textBox2.Text;

                // Assuming 'CreateEvent' is your table name and 'event_name' and 'event_date' are the corresponding columns
                string deleteQuery = "DELETE FROM Manage_Participant WHERE p_fname = :fname AND p_lname = :lname";

                using (OracleCommand cmd = new OracleCommand(deleteQuery, con))
                {
                    cmd.Parameters.Add(":fname", OracleDbType.Varchar2).Value = fname;
                    cmd.Parameters.Add(":lname", OracleDbType.Varchar2).Value = lname;


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data DELETED Successfully!");
                        // Update your grid or perform any additional actions
                        updateGrid();
                    }
                    else
                    {
                        MessageBox.Show("Participant with the specified fname and lname not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button3_Click(object sender, EventArgs e)
        {
            
                try
                {
                    con.Open();

                    // Assuming 'firstName' and 'lastName' are TextBox controls, replace them with your actual controls
                    string firstName = textBox1.Text;
                    string lastName = textBox2.Text;

                    // Assuming 'Manage_Participant' is your table name
                    string updateQuery = "UPDATE Manage_Participant " +
                                         "SET regStatus = :regStatus, attendance = :attendance, paymentMethod = :paymentMethod " +
                                         "WHERE p_fname = :firstName AND p_lname = :lastName";

                    using (OracleCommand cmd = new OracleCommand(updateQuery, con))
                    {
                        // Assuming 'regStatus', 'attendance', 'paymentMethod', 'firstName', and 'lastName' are parameters
                        cmd.Parameters.Add(":regStatus", OracleDbType.Varchar2, 20).Value = comboBox1.SelectedItem.ToString(); // Use the selected registration status from ComboBox
                        cmd.Parameters.Add(":attendance", OracleDbType.Varchar2, 20).Value = comboBox2.SelectedItem.ToString(); // Use the selected attendance status from ComboBox
                        cmd.Parameters.Add(":paymentMethod", OracleDbType.Varchar2, 50).Value = GetSelectedPaymentMethod(); // Use the selected payment method from RadioButton
                        cmd.Parameters.Add(":firstName", OracleDbType.Varchar2, 50).Value = firstName;
                        cmd.Parameters.Add(":lastName", OracleDbType.Varchar2, 50).Value = lastName;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Update your grid or perform any additional actions
                            updateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Participant with the specified first name and last name not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button4_Click(object sender, EventArgs e)
        {
            AdminManagement form = new AdminManagement();
            this.Hide();
            form.ShowDialog();
        }
    }
}
