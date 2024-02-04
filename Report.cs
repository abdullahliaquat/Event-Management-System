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
    public partial class Report : Form
    {
        OracleConnection con;
        
        public Report()
        {
            InitializeComponent();
            this.Load += Report_load;
        }

        private void Report_load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=F219632; PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }
        private void updateGrid()
        {
            try
            {
                // Assuming you have a table named "YourTableName" in your database
                string query = "SELECT " +
                    "E.EVENT_ID, " +
                    "E.EVENT_NAME, " +
                    "E.LOCATION, " +
                    "E.SPEAKER, " +
                    "E.SOCIALS, " +
                    "E.EVENT_DATE, " +
                    "E.PAID_AMOUNT, " +
                    "P.PARTICIPANT_ID, " +
                    "P.FIRSTNAME, " +
                    "P.LASTNAME, " +
                    "P.USERNAME, " +
                    "P.GENDER " +
                    "FROM EVENT E " +
                    "JOIN PARTICIPANT P ON E.Participant_ID = P.Participant_ID";
                OracleDataAdapter adapter = new OracleDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Assuming 'dataGridView1' is your DataGridView
                dataGridView1.DataSource = dt; // Uncomment this line to bind the DataTable to the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating grid: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Note: Do not close the connection here, as it's managed in the button2_Click method
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminManagement form = new AdminManagement();
            this.Hide();
            form.ShowDialog();
        }
    }
}
