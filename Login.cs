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
    public partial class Login : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;

        // Getter methods for username and password
        public string GetLoggedInUsername() => loggedInUsername;
        public string GetLoggedInPassword() => loggedInPassword;
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UidTb.Text = "";
            PassTb.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(UidTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else if(UidTb.Text=="Admin" && PassTb.Text == "Admin")
            {
                MainForm mainform = new MainForm();
                mainform.Show();
                this.Hide();
            }
            else
            {
                string email = UidTb.Text;
                string password = PassTb.Text;

                // Query the database to check if the provided email and password exist in the payment table
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elvis\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM MemberTbl WHERE MEmail = @Email AND MPassword = @Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        // If a record with the provided email and password exists, proceed to appropriate form
                        ClientPage client = new ClientPage(); // or ClientPage mainform = new ClientPage(); depending on the role
                        client.Show();
                        this.Hide();
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Invalid email or password");
                    }
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
