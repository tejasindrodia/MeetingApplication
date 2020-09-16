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

namespace Meeting_Management_System
{
    public partial class Dashboard : Form
    {
        public static int meetingScheduleNumber;

        OleDbConnection connection = new OleDbConnection();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'meetingManagementSystem_dbDataSet1.agenda' table. You can move, or remove it, as needed.
            this.agendaTableAdapter.Fill(this.meetingManagementSystem_dbDataSet1.agenda);
            // TODO: This line of code loads data into the 'meetingManagementSystem_dbDataSet1.schedule' table. You can move, or remove it, as needed.
            this.scheduleTableAdapter.Fill(this.meetingManagementSystem_dbDataSet1.schedule);
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=MeetingManagementSystem_db.mdb;
Persist Security Info=False;";
            //populating the venue from the database into the combobox
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            try
            {
                connection.Open();
                command.CommandText = "select * from venue";

                OleDbDataReader reader = command.ExecuteReader();
                //check for login if correct
                while (reader.Read())
                {
                    meetingVenueComboBox.AddItem(reader["venue_name"].ToString());
                }
            }
            catch (OleDbException error)
            {
                MessageBox.Show("Error Occur " + error.Message);
            }
            finally
            {
                connection.Close();
            }

            //set the date picker
            meetingDate.Value = DateTime.Now;

            //set default value for starting time and ending time
            startingTime.selectedIndex = 0;
            endingTime.selectedIndex = 0;
            //setting user infor
            userIdLabel.Text = "No#: " + LoginForm.userId;
            userLabel.Text = "User: " + LoginForm.username;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            {
                this.Visible = false; //we hide the current form
                this.Close();
                new LoginForm().Show(); //we show a new form
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = false;
            SchedulePanel.Visible = true;
            AddAgendaPanel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainPanel.Visible = true;
            SchedulePanel.Visible = false;
            AddAgendaPanel.Visible = false;

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picMaximize_Click(object sender, EventArgs e)
        {
             if (this.WindowState == FormWindowState.Maximized){
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
             }else{
                 this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txtDescription.Text = "";
        }

        private void changePasswordBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(convert_format(955).ToString("hh:mm:ss tt"));
            //MessageBox.Show(convert_format(Convert.ToInt32(removeCharacterFromString(startingTime.selectedValue))).ToString("hh:mm"));
            //meetingTitleDropdown.selectedValue
            
                 connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data source=MeetingManagementSystem_db.mdb;
Persist Security Info=False;";
            //populating the venue from the database into the combobox
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;


            try
            {
                connection.Open();
                command.CommandText = "insert into schedule (`user_id`,`title`,`description`,`remarks`,`venue_id`,`meetingdate`,`starttime`,`endtime`) "+
			   "values ('" + LoginForm.userId + "','" + meetingTitleDropdown.selectedValue + "','" + txtDescription.Text + "','" + txtMeetingRemarks.Text + "','"+"','"+"','"+"','"+"')";

                OleDbDataReader reader = command.ExecuteReader();
                //check for login if correct
                while (reader.Read())
                {
                    meetingVenueComboBox.AddItem(reader["venue_name"].ToString());
                }
            }
            catch (OleDbException error)
            {
                MessageBox.Show("Error Occur " + error.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private DateTime convert_format(int time)
        {
	        string timeTemp = (time >= 1300 ? time - 1200 : (time < 100 ? time + 1200 : time)).ToString();
            int len = timeTemp.Length;

            string unformat_time =  (len == 3) ? timeTemp.Insert(1, ":") : timeTemp.Insert(2, ":");
            return DateTime.Parse(unformat_time, System.Globalization.CultureInfo.CurrentCulture);

        }
        private string removeCharacterFromString(string str, String c = "H", String[] chars = null)
        {
            if (chars != null)
            {
                foreach (var item in chars)
                {
                    str = str.Replace(item, string.Empty);
                }
            }
            else
            {
                if (c == "H")
                {
                    str = str.Replace("H", string.Empty);
                }
                else
                {
                    str = str.Replace(c, string.Empty);
                }
            }
            return str;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            meetingScheduleNumber = Convert.ToInt32(sheduleDisplayGrid.CurrentRow.Cells[0].Value);
            meetingNoLabel.Text = meetingScheduleNumber.ToString();
            meetingTitleLabel.Text = sheduleDisplayGrid.CurrentRow.Cells[1].Value.ToString();

            AddAgendaPanel.Visible = true;
            SchedulePanel.Visible = false; //we hide the current form
            
        }
    }
}
