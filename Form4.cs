using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Project
{
    public partial class Form4 : Form
    {
        string ordb = "Data Source=ORCL; User Id=scott; Password=tiger;";
        OracleConnection conn;
        int new_id;

        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into users values (:id, :name)";
            cmd.Parameters.Add("id", new_id);
            cmd.Parameters.Add("name", textBox1.Text);
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("New User is added \nNew User ID: " + new_id);
            }
            new_id++;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "USER_ID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("Idd", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            
            try
            {
                new_id = Convert.ToInt32(c.Parameters["Idd"].Value.ToString()) + 1;
            }
            catch
            {
                new_id = 1;
            }

        }


        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f1 = new Form3();
            f1.Show();
            this.Hide();
        }
    }
}
