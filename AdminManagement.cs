//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//namespace FInal_DB__Project
//{
//    public partial class AdminManagement : Form
//    {
//        OracleConnection con;
//        public AdminManagement()
//        {
//            InitializeComponent();
//            this.Load += AdminManagement_load;
//        }
//        private void AdminManagement_load(object sender, EventArgs e)
//        {
//            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
//            con = new OracleConnection(conStr);
//            updateGrid();
//            textBox2.PasswordChar = '*';
//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            // Check if any of the required fields is empty
//            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Check other fields as well
//                string.IsNullOrWhiteSpace(textBox2.Text) || // Check other fields as well
//                !checkBox1.Checked)
//            {
//                // Show an error message if any field is missing
//                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            else
//            {
//                // Display the "OK" message
//                MessageBox.Show("OK", "Proceeding", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                // You can add the logic to proceed here

//                loginform loginForm = new loginform();
//                loginForm.ShowDialog();

//                this.Hide();
//            }

//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            // Check if any of the required fields is empty
//            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Check other fields as well
//                string.IsNullOrWhiteSpace(textBox2.Text) || // Check other fields as well
//                !checkBox1.Checked)
//            {
//                // Show an error message if any field is missing
//                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            else
//            {
//                // Display the "OK" message
//                MessageBox.Show("OK", "Proceeding", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                NewEvent NewEvent = new NewEvent();
//                NewEvent.ShowDialog();

//                this.Hide();
//                // You can add the logic to proceed here
//            }
//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            // Check if any of the required fields is empty
//            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Check other fields as well
//                string.IsNullOrWhiteSpace(textBox2.Text) || // Check other fields as well
//                !checkBox1.Checked)
//            {
//                // Show an error message if any field is missing
//                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            else
//            {
//                // Display the "OK" message
//                MessageBox.Show("OK", "Proceeding", MessageBoxButtons.OK, MessageBoxIcon.Information);


//                // You can add the logic to proceed here

//                ParticipantMng participantMng = new ParticipantMng();
//                participantMng.Show();

//                this.Hide();
//            }
//        }

//        private void button4_Click(object sender, EventArgs e)
//        {
//            // Check if any of the required fields is empty
//            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Check other fields as well
//                string.IsNullOrWhiteSpace(textBox2.Text) || // Check other fields as well
//                !checkBox1.Checked)
//            {
//                // Show an error message if any field is missing
//                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            else
//            {
//                // Display the "OK" message
//                MessageBox.Show("OK", "Proceeding", MessageBoxButtons.OK, MessageBoxIcon.Information);


//                // You can add the logic to proceed here

//                SpeakerMng speakerMng = new SpeakerMng();
//                speakerMng.Show();

//                this.Hide();
//            }
//        }

//        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void panel1_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void updateGrid()
//        {
//            try
//            {
//                con.Open();

//                // Assuming you have a table named "YourTableName" in your database
//                string query = "SELECT * FROM DEPT";
//                OracleDataAdapter adapter = new OracleDataAdapter(query, con);
//                DataTable dt = new DataTable();
//                adapter.Fill(dt);

//                //dataGridView1.DataSource = dt;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error updating grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                if (con.State == ConnectionState.Open)
//                {
//                    con.Close();
//                }
//            }
//        }

//        private void AdminManagement_Load_1(object sender, EventArgs e)
//        {

//        }

//        private void textBox2_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void button5_Click(object sender, EventArgs e)
//        {
//            Report report = new Report();
//            this.Hide();
//            report.ShowDialog();
//        }
//    }
//}





using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace FInal_DB__Project
{
    public partial class AdminManagement : Form
    {
        OracleConnection con;

        public AdminManagement()
        {
            InitializeComponent();
            this.Load += AdminManagement_load;
        }

        private void AdminManagement_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            textBox2.PasswordChar = '*';

            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void ProceedIfCredentialsAreCorrect(Form formToOpen)
        {
            // Check if any of the required fields is empty
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                !checkBox1.Checked)
            {
               
                MessageBox.Show("Fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check if the entered credentials are correct
                if (CheckCredentials(textBox1.Text, textBox2.Text))
                {
                    // Display the "OK" message
                    MessageBox.Show("OK", "Proceeding", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // You can add the logic to proceed here
                    this.Hide();
                    formToOpen.ShowDialog();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private bool CheckCredentials(string username, string password)
        {
           
            return username == "Admin" && password == "1234";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            ProceedIfCredentialsAreCorrect(new loginform());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ProceedIfCredentialsAreCorrect(new NewEvent());
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            ProceedIfCredentialsAreCorrect(new ParticipantMng());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            ProceedIfCredentialsAreCorrect(new SpeakerMng());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            ProceedIfCredentialsAreCorrect(new Report());
        }

        private void updateGrid()
        {
            try
            {
                con.Open();

                // We have a table named "YourTableName" in your database
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

        private void AdminManagement_Load_1(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
