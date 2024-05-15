using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GymApp
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elvis\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            populate();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
      //  int key = 1;
        private void button3_Click(object sender, EventArgs e)
        {
            if(NameCb.Text == "" || AmountTb.Text== "" || EntriesTb.Text =="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string payperiode = Periode.Value.Day.ToString() +"." + Periode.Value.Month.ToString()+"."+Periode.Value.Year.ToString();
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from PaymentTbl where PMember='"+NameCb.SelectedValue.ToString()+"' and PMonth='"+payperiode+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Already Paid For This Month");
                }
                else
                {
                    string query = "insert into PaymentTbl values('" + payperiode + "','" + NameCb.SelectedValue.ToString() + "'," + AmountTb.Text + "," + EntriesTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Amout Paid Successfully");
                }
                Con.Close();
                populate();
            }
        }

        private void FillName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Mname from MemberTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mname",typeof(string));
            dt.Load(rdr);
            NameCb.ValueMember = "Mname";
            NameCb.DataSource = dt;
            Con.Close();
        }
        private void filterByName()
        {
            Con.Open();
            string query = "select * from PaymentTbl where PMember='"+SearchName.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from PaymentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
          //  NameTb.Text = "";
            AmountTb.Text = "";
            EntriesTb.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            filterByName();
            SearchName.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void PaymentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
