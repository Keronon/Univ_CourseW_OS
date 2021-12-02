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
        private int password_hash;

        public FORM_Authorization()
        {
            Log.Write("FORM_Authorization | Initialization\n");

            Data_Keeper.FORM_Authorization = this;
            InitializeComponent();

            if (!File.Exists(Data_Keeper.file_system_path))
            {
                MessageBox.Show("Can not find file system file\nNeed to reboot file system");
                if (!rfs_c()) { BTN_login.Enabled = false; return; }
            }
            Data_Keeper.file_system = new File_System();

            COMBO_user.Items.Clear();
            try
            {
                for (int i = 1; i <= Data_Keeper.file_system.super_block.cur_users_count; i++)
                {
                    File_System.User user = Data_Keeper.file_system.Read_user(1).Value;
                    COMBO_user.Items.Add(user);
                }
                COMBO_user.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Reading users ERROR");
                Log.Write("FORM_Authorization | Initialization - Reading users ERROR\n");
            }

            Log.Write("FORM_Authorization | Initialized\n");
        }

        private void COMBO_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Selected user changed\n");

            password_hash = ((File_System.User)COMBO_user.SelectedItem).password_hash;
        }

        private void BTN_login_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Login Clicked\n");

            if (TXT_password.Text.GetHashCode() == password_hash)
            {
                Data_Keeper.cur_user = (File_System.User)COMBO_user.SelectedItem;

                if (Data_Keeper.FORM_Main is null) { Hide(); new FORM_Main().Show(); Data_Keeper.FORM_Main.Set_user(); }
                else                               { Hide(); Data_Keeper.FORM_Main.Show(); Data_Keeper.FORM_Main.Set_user(); }

                Data_Keeper.FORM_Main.Refresh_tree_view();

                Log.Write("FORM_Authorization | Logged in\n");
            }
            else
            {
                MessageBox.Show("Incorrect password");

                Log.Write("FORM_Authorization | Incorrect password\n");
            }
        }

        private void BTN_quit_Click(object sender, EventArgs e)
        {
            Log.Write("FORM_Authorization | Quit Clicked\n");

            Application.Exit();
        }

        private void BTN_reboot_file_system_Click(object sender, EventArgs e) { rfs_c(); }
        private bool rfs_c()
        {
            Log.Write("FORM_Authorization | Reboot file system Clicked\n");

            if (MessageBox.Show("Accept action", "File system rebooting", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Log.Write("FORM_Authorization | Rebooting accepted\n");

                File_System.Create(new File_System.Super_Block(34, 0, 1024, 12, 512, 510, 1024, 42, 512, 510, 20, 32, 1, 36));
                Data_Keeper.file_system = new File_System();

                COMBO_user.Items.Clear();
                File_System.User user = Data_Keeper.file_system.Read_user(1).Value;
                COMBO_user.Items.Add(user);
                COMBO_user.SelectedIndex = 0;

                MessageBox.Show("File system rebooting comleted");
                BTN_login.Enabled = true;
                return true;
            }
            else
            {
                MessageBox.Show("Rebooting error");

                Log.Write("FORM_Authorization | Rebooting canceled\n");
                return false;
            }
        }

        private void BTN_testing_Click(object sender, EventArgs e)
        {
            TEST_CLASS.START();
        }
    }
}
