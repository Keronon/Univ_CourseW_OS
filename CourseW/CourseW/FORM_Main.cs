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

            DataKeeper.FORM_Main = this;
            InitializeComponent();

            Log.Write("FORM_Main | Initialized\n");
        }

        #region INTERFACE
        #region System
        #region change password
        private void BTN_change_password_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Change password Clicked\n");
        }

        private void BTN_cancel_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Cancel Clicked\n");
        }

        private void BTN_accept_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Accept Clicked\n");
        }
        #endregion change password

        private void BTN_logout_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Main | Logout Clicked\n");

            Hide();
            DataKeeper.FORM_Authorization.Show();
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
