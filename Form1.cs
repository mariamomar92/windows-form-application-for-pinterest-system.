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
using CrystalDecisions.Shared;

namespace Project
{
    public partial class Form1 : Form
    {
        string ordb = "Data Source=ORCL; User Id=scott; Password=tiger;";
        OracleConnection conn;
        CrystalReport1 CR;
        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            CR = new CrystalReport1();
            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select userid from users";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select count(userid) from followers where followerid=:id";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("id", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                textBox3.Text = dr[0].ToString();
            }

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select username from users where userid =:iddd";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("iddd", comboBox1.SelectedItem.ToString());
            OracleDataReader drr = cmd.ExecuteReader();
            if (drr.Read())
            {
                textBox2.Text = drr[0].ToString();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "BOARDSS";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", comboBox1.SelectedItem);
            c.Parameters.Add("bname", OracleDbType.RefCursor, ParameterDirection.Output);


            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            { comboBox2.Items.Add(dr[0]); }

            dr.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "BOARD_ID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("Idd", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            int new_id;
            try
            {
                new_id = Convert.ToInt32(c.Parameters["Idd"].Value.ToString()) + 1;
            }
            catch
            {
                new_id = 1;
            }
            char s;

            if (radioButton1.Checked)
            {
                 s = 'Y';
            }
            else
            {
                 s = 'N';

            }

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "ISERTBOARD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", new_id);
            cmd.Parameters.Add("name", textBox1.Text);
            cmd.Parameters.Add("public", s);
            cmd.Parameters.Add("createdby", comboBox1.SelectedItem);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Board Inserted Successfully");
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {     
            crystalReportViewer1.ReportSource = CR;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f1 = new Form3();
            f1.Show();
            this.Hide();
        }
    }
}
