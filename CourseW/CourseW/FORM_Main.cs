using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseW
{
    public partial class FORM_Main : Form
    {
        public FORM_Main()
        {
            Log.Write("FORM_Main | Initialization\n");

            Data_Keeper.FORM_Main = this;
            InitializeComponent();
            PANEL_change_password.Hide();
            PANEL_users.Hide();

            Log.Write("FORM_Main | Initialized\n");
        }

        #region INTERFACE
        #region System
        #region change password
        private void BTN_change_password_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Change password Clicked\n");

            PANEL_change_password.Show();
        }

        private void BTN_cancel_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Cancel Clicked\n");

            PANEL_change_password.Hide();
        }

        private void BTN_accept_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Accept Clicked\n");
        }
        #endregion change password

        #region users
        private void BTN_users_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Users Clicked\n");

            if (PANEL_users.Visible) PANEL_users.Hide();
            else PANEL_users.Show();
        }

        private void BTN_add_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Add user Clicked\n");

        }

        private void BTN_delete_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Delete user Clicked\n");

        }

        private void BTN_edit_user_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Edit user Clicked\n");

        }
        #endregion users

        private void BTN_logout_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Logout Clicked\n");

            Hide();
            Data_Keeper.FORM_Authorization.Show();

            Log.Write("FORM_Main | Logged out\n");
        }

        private void BTN_quit_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Quit Clicked\n");

            Application.Exit();
        }
        #endregion System

        #endregion INTERFACE
    }
}
