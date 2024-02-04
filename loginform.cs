using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace FInal_DB__Project
{
    public partial class loginform : Form
    {
        OracleConnection con;
        public loginform()
        {
            InitializeComponent();
            this.Load += loginform_load;
        }
        private void loginform_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            textBox4.PasswordChar = '*';
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
        string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text) ||
        string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Select gender!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Generate a random participant ID (replace this with a more robust method if needed)
                    Random random = new Random();
                    int randomParticipantID = random.Next(100000, 999999); // Adjust the range as needed

                    // Check if the username already exists
                    OracleCommand checkUsernameCommand = new OracleCommand($"SELECT COUNT(*) FROM Participant WHERE username = '{textBox3.Text}'", con);
                    int count = Convert.ToInt32(checkUsernameCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Insert the participant details into the 'participant' table
                        OracleCommand insertParticipant = con.CreateCommand();
                        insertParticipant.CommandText = $"INSERT INTO Participant (participant_id, firstname, lastname, username, gender) " +
                                                        $"VALUES ({randomParticipantID}, '{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{(radioButton1.Checked ? "Male" : (radioButton2.Checked ? "Female" : "Other"))}')";
                        insertParticipant.CommandType = CommandType.Text;

                        int rows = insertParticipant.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //    var eventForm = new EVENT();

                            //    eventForm.Show();
                            //    this.Hide();
                            //
                            var eventForm = new EVENT(randomParticipantID);
                            eventForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            // Update the grid after successfully registering a participant
            updateGrid();
        }
        private void updateGrid()
        {
            try
            {
               
                string query = "SELECT * FROM Participant";
                OracleDataAdapter adapter = new OracleDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                
               // dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
            }
        }

        private void loginform_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming 'con' is your OracleConnection
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                //  'txtUsername' is a TextBox containing the username to delete
                string usernameToDelete = textBox3.Text;

                // 'Participant' is  table name and 'username' is the column to check
                string deleteQuery = $"DELETE FROM Participant WHERE username = '{usernameToDelete}'";

                using (OracleCommand cmd = new OracleCommand(deleteQuery, con))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data DELETED Successfully!");
                        // Update your grid or perform any additional actions
                        updateGrid();
                    }
                    else
                    {
                        MessageBox.Show("Participant with the specified username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Select gender!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Assuming 'txtUsername' is a TextBox containing the username to update
                    string usernameToUpdate = textBox3.Text;

                    // Check if the username exists
                    OracleCommand checkUsernameCommand = new OracleCommand($"SELECT COUNT(*) FROM Participant WHERE username = '{usernameToUpdate}'", con);
                    int count = Convert.ToInt32(checkUsernameCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("Username not found. Please enter a valid username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Update the participant details in the 'participant' table
                        OracleCommand updateParticipant = con.CreateCommand();
                        updateParticipant.CommandText = $"UPDATE Participant " +
                                                        $"SET firstname = '{textBox1.Text}', lastname = '{textBox2.Text}', " +
                                                        $"gender = '{(radioButton1.Checked ? "Male" : (radioButton2.Checked ? "Female" : "Other"))}' " +
                                                        $"WHERE username = '{usernameToUpdate}'";
                        updateParticipant.CommandType = CommandType.Text;

                        int rows = updateParticipant.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Update your grid or perform any additional actions
                            updateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
