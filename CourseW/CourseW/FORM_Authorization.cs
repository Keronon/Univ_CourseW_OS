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

            Data_Keeper.FORM_Authorization = this;
            InitializeComponent();

            Log.Write("FORM_Authorization | Initialized\n");
        }

        private void BTN_reboot_file_system_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Reboot file system Clicked\n");

            DialogResult result = MessageBox.Show("Подтвердите действие", "Сброс файловой системы", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Log.Write("FORM_Authorization | Rebooting accepted\n");

                File_System.Create(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            else
            {
                Log.Write("FORM_Authorization | Rebooting canceled\n");
            }
        }

        private void BTN_login_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Login Clicked\n");

            if (Data_Keeper.FORM_Main is null) { Hide(); new FORM_Main().Show(); }
            else                               { Hide(); Data_Keeper.FORM_Main.Show(); }

            Log.Write("FORM_Authorization | Logged in\n");
        }

        private void BTN_quit_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Quit Clicked\n");

            Application.Exit();
        }
    }
}
