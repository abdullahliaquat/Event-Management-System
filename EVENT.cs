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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FInal_DB__Project
{
    public partial class EVENT : Form
    {
        OracleConnection con;

        private int participantID;

        // Constructor to receive Participant_Id
        public EVENT(int participantID)
        {
            InitializeComponent();
            this.participantID = participantID;
            this.Load += EVENT_load;
        }
        private void EVENT_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
    string.IsNullOrWhiteSpace(comboBox2.Text) ||
    string.IsNullOrWhiteSpace(comboBox3.Text) ||
    string.IsNullOrWhiteSpace(comboBox4.Text) ||
    dateTimePicker1.Value == null||
    string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();

                    // Generate a random event ID (replace this with a more robust method if needed)
                    Random random = new Random();
                    int randomEventID = random.Next(100000, 999999); 

                    // Insert the event details into the 'Event' table
                    OracleCommand insertEvent = con.CreateCommand();
                    insertEvent.CommandText = "INSERT INTO Event (event_id, event_name, location, speaker, socials, event_date, paid_amount,participant_id) " +
                                              $"VALUES ({randomEventID}, :eventName, :location, :speaker, :socials, :eventDate, :paid_amount, {participantID})";

                    // Use parameters to avoid SQL injection 
                    insertEvent.Parameters.Add(":eventName", OracleDbType.Varchar2).Value = comboBox1.Text;
                    insertEvent.Parameters.Add(":location", OracleDbType.Varchar2).Value = comboBox2.Text;
                    insertEvent.Parameters.Add(":speaker", OracleDbType.Varchar2).Value = comboBox3.Text;
                    insertEvent.Parameters.Add(":socials", OracleDbType.Varchar2).Value = comboBox4.Text;
                    insertEvent.Parameters.Add(":eventDate", OracleDbType.Date).Value = dateTimePicker1.Value;
                    insertEvent.Parameters.Add(":paid_amount", OracleDbType.Int32).Value = textBox1.Text;

                    insertEvent.CommandType = CommandType.Text;

                    int rows = insertEvent.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Event inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void updateGrid()
        {
            try
            {
                con.Open();

                // Assuming you have a table named "YourTableName" in your database
                string query = "SELECT * FROM DEPT";
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

       
        private void EVENT_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}