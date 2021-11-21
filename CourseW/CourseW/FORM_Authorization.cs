using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseW
{
    public partial class FORM_Authorization : Form
    {
        public FORM_Authorization()
        {
            Log.Write("FORM_Authorization | Initialization\n");

            DataKeeper.FORM_Authorization = this;
            InitializeComponent();

            Log.Write("FORM_Authorization | Initialized\n");
        }

        private void BTN_login_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Login Clicked\n");

            if (DataKeeper.FORM_Main is null) { Hide(); new FORM_Main().Show(); }
            else                              { Hide(); DataKeeper.FORM_Main.Show(); }
        }

        private void BTN_quit_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Quit Clicked\n");

            Application.Exit();
        }
    }
}
