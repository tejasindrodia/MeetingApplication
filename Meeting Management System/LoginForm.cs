using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace Meeting_Management_System
{
    public partial class LoginForm : Form
    {
        OleDbConnection connection = new OleDbConnection();
        public static string username;
        public static int userId;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("form loaded");
            
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=MeetingManagementSystem_db.mdb;
Persist Security Info=False;";

            try
            {
                connection.Open();

                connectionStatus.BackColor = Color.LightGreen;
                //MessageBox.Show("connection successful");
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("connection un-successful  \n"+ex.Message);
            }
           
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            try { 
                string passwordHash = CalculateMD5Hash(txtpassword.Text);
                command.CommandText = "select * from user_info where username='" + txtusername.Text + "' and password='" + passwordHash + "' and active = 1";

                OleDbDataReader reader = command.ExecuteReader();
                //check for login if correct
                if (reader.Read() == true)
                {
                    //get user infor
                    username = reader["fname"].ToString();
                    userId = Convert.ToInt32(reader["ID"]);

                    new Dashboard().Show(); //we show a new form
                    this.Visible = false; //we hide the current form
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }
            catch (OleDbException error)
            {
                MessageBox.Show("Error Occur " + error.Message);
            }
           
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private void txtusername_MouseClick(object sender, MouseEventArgs e)
        {
            txtusername.Text = "";
        }

        private void txtpassword_MouseClick(object sender, MouseEventArgs e)
        {
            txtpassword.Text = "";
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
