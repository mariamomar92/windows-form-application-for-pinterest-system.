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
    public partial class Form2 : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        CrystalReport2 CR;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CR = new CrystalReport2();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = "data source= orcl; user id = scott;password=tiger;";
            string cmdstr;
                cmdstr = "select * from pin where userid =:id";
            adapter = new OracleDataAdapter(cmdstr,constr);
            adapter.SelectCommand.Parameters.Add("id", textBox1.Text);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
            MessageBox.Show("Table Updated Successfully");
         
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = CR;
            CR.SetParameterValue(0, textBox1.Text);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f1 = new Form3();
            f1.Show();
            this.Hide();
        }
    }
}
