using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymApp
{
    public partial class Vestiar : Form
    {
        public Vestiar()
        {
            InitializeComponent();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from PaymentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            MemberSDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Vestiar_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elvis\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void UpdatePEntries(string memberId)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elvis\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                con.Open();
                string query = "UPDATE PaymentTbl SET PEntries = PEntries - 1 WHERE PMember = @MemberId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemberId", memberId);
                cmd.ExecuteNonQuery();
            }
        }

        private void filterByName()
        {
            Con.Open();
            string memberId = SearchName.Text;

            // Update PEntries for the specified member
            UpdatePEntries(memberId);

            // Retrieve the updated data and display it in the DataGridView
            string query = "SELECT PEntries, PMonth FROM PaymentTbl WHERE PMember = @MemberId";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            sda.SelectCommand.Parameters.AddWithValue("@MemberId", memberId);

            var ds = new DataSet();
            sda.Fill(ds);
            MemberSDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterByName();
            SearchName.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
