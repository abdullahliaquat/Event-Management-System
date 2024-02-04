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
    public partial class NewEvent : Form
    {
        OracleConnection con;
        public NewEvent()
        {
            InitializeComponent();
            this.Load += NewEvent_load;

        }
        private void NewEvent_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Check if any of the required fields is empty
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    comboBox1.SelectedIndex == -1 ||
                    comboBox2.SelectedIndex == -1 ||
                    dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
                {
                    // Show an error message if any field is missing
                    MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();
                    OracleCommand insertEvent = con.CreateCommand();

                    // Generate a random ev_id
                    Random random = new Random();
                    int randomEvId = random.Next(1, 1000); // Adjust the range as needed

                    // we have text boxes named textBox1, textBox2, comboBox1, comboBox2, and dateTimePicker1
                    insertEvent.CommandText = "INSERT INTO CreateEvent (ev_id, event_name, description, location, speaker, event_date) " +
                                              "VALUES (:ev_id, :event_name, :description, :location, :speaker, :event_date)";

                 
                    insertEvent.Parameters.Add(":ev_id", OracleDbType.Int32).Value = randomEvId;
                    insertEvent.Parameters.Add(":event_name", OracleDbType.Varchar2).Value = textBox1.Text;
                    insertEvent.Parameters.Add(":description", OracleDbType.Varchar2).Value = textBox2.Text;
                    insertEvent.Parameters.Add(":location", OracleDbType.Varchar2).Value = comboBox2.Text;
                    insertEvent.Parameters.Add(":speaker", OracleDbType.Varchar2).Value = "-";
                    insertEvent.Parameters.Add(":event_date", OracleDbType.Date).Value = dateTimePicker1.Value;

                    insertEvent.CommandType = CommandType.Text;
                    insertEvent.ExecuteNonQuery();

                    con.Close();

                    // Display the "CREATED" message
                    MessageBox.Show("CREATED", "Event Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form1 form1 = new Form1();
                    //form1.updateGrid();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void updateGrid()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM EVENT";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NewEvent_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                // Assuming 'eventName' and 'eventDate' are TextBox and DateTimePicker controls, replace them with your actual controls
                string eventName = textBox1.Text;
                string eventDes = textBox2.Text;

                // Assuming 'CreateEvent' is your table name and 'event_name' and 'event_date' are the corresponding columns
                string deleteQuery = "DELETE FROM CreateEvent WHERE event_name = :eventName AND description = :eventDes";

                using (OracleCommand cmd = new OracleCommand(deleteQuery, con))
                {
                    cmd.Parameters.Add(":eventName", OracleDbType.Varchar2).Value = eventName;
                    cmd.Parameters.Add(":eventDes", OracleDbType.Varchar2).Value = eventDes;


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data DELETED Successfully!");
                        // Update your grid or perform any additional actions
                        updateGrid();
                    }
                    else
                    {
                        MessageBox.Show("Event with the specified name and date not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { 
            //            //    if (con.State == ConnectionState.Open)
            //    {
            //        con.Close();
            //    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                // Check if any of the required fields is empty
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    comboBox1.SelectedIndex == -1 ||
                    comboBox2.SelectedIndex == -1 ||
                    dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
                {
                    // Show an error message if any field is missing
                    MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    con.Open();

                    // Assuming 'eventName' is TextBox containing the existing event name
                    string eventNameToUpdate = textBox1.Text;

                    // Check if the event name exists
                    OracleCommand checkEventNameCommand = new OracleCommand($"SELECT COUNT(*) FROM CreateEvent WHERE event_name = '{eventNameToUpdate}'", con);
                    int count = Convert.ToInt32(checkEventNameCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("Event name not found. Please enter a valid event name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Update the event details in the 'CreateEvent' table
                        OracleCommand updateEvent = con.CreateCommand();
                        updateEvent.CommandText = $"UPDATE CreateEvent " +
                                                  $"SET description = '{textBox2.Text}', location = '{comboBox2.Text}', speaker = '-', event_date = :event_date " +
                                                  $"WHERE event_name = '{eventNameToUpdate}'";
                        updateEvent.Parameters.Add(":event_date", OracleDbType.Date).Value = dateTimePicker1.Value;
                        updateEvent.CommandType = CommandType.Text;

                        int rows = updateEvent.ExecuteNonQuery();

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

        private void button4_Click(object sender, EventArgs e)
        {
            AdminManagement form = new AdminManagement();
            this.Hide();
            form.ShowDialog();
        }
    }
}