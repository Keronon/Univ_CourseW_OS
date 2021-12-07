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
    public partial class FORM_Properties : Form
    {
        File_System.Inode inode;
        int inode_pos;

        public FORM_Properties()
        {
            InitializeComponent();

            BTN_action.Click += (o, e) =>
            {
                Log.Write($"FORM_Properties | {BTN_action.Text} clicked\n");

                if (BTN_action.Text == "Change")
                {
                    if (inode.owner_id != Data_Keeper.cur_user.id && inode.owner_id != 1)
                    {
                        MessageBox.Show("You can not change this object atributes");

                        Log.Write($"FORM_Properties | User can not change object atributes\n");
                        return;
                    }
                    Log.Write($"FORM_Properties | {BTN_action.Text} started\n");

                    CHECK_hidden.Enabled      = true;
                    CHECK_o_read.Enabled      = true;
                    CHECK_o_write.Enabled     = true;
                    CHECK_o_execute.Enabled   = true;
                    CHECK_o_g_read.Enabled    = true;
                    CHECK_o_g_write.Enabled   = true;
                    CHECK_o_g_execute.Enabled = true;
                    CHECK_oth_read.Enabled    = true;
                    CHECK_oth_write.Enabled   = true;
                    CHECK_oth_execute.Enabled = true;

                    BTN_action.Text    = "Accept";
                    BTN_cancel.Visible = true;
                }
                else // if (BTN_action.Text == "Accept")
                {
                    try
                    {
                        inode.atributes[2] = CHECK_o_read.Checked;
                        inode.atributes[3] = CHECK_o_write.Checked;
                        inode.atributes[4] = CHECK_o_execute.Checked;

                        inode.atributes[5] = CHECK_o_g_read.Checked;
                        inode.atributes[6] = CHECK_o_g_write.Checked;
                        inode.atributes[7] = CHECK_o_g_execute.Checked;

                        inode.atributes[8] = CHECK_oth_read.Checked;
                        inode.atributes[9] = CHECK_oth_write.Checked;
                        inode.atributes[10] = CHECK_oth_execute.Checked;

                        inode.atributes[11] = CHECK_hidden.Checked;

                        Data_Keeper.file_system.Write_inode(inode_pos, inode);

                        Log.Write($"FORM_Properties | Ended without errors\n");
                    }
                    catch
                    {
                        MessageBox.Show("Error");

                        Log.Write($"FORM_Properties | Ended with errors\n");
                    }

                    CHECK_hidden.Enabled      = false;
                    CHECK_o_read.Enabled      = false;
                    CHECK_o_write.Enabled     = false;
                    CHECK_o_execute.Enabled   = false;
                    CHECK_o_g_read.Enabled    = false;
                    CHECK_o_g_write.Enabled   = false;
                    CHECK_o_g_execute.Enabled = false;
                    CHECK_oth_read.Enabled    = false;
                    CHECK_oth_write.Enabled   = false;
                    CHECK_oth_execute.Enabled = false;

                    BTN_action.Text    = "Change";
                    BTN_cancel.Visible = false;
                }
            };

            BTN_cancel.Click += (o, e) =>
            {
                Log.Write($"FORM_Properties | Cancel clicked\n");

                CHECK_hidden.Enabled      = false;
                CHECK_o_read.Enabled      = false;
                CHECK_o_write.Enabled     = false;
                CHECK_o_execute.Enabled   = false;
                CHECK_o_g_read.Enabled    = false;
                CHECK_o_g_write.Enabled   = false;
                CHECK_o_g_execute.Enabled = false;
                CHECK_oth_read.Enabled    = false;
                CHECK_oth_write.Enabled   = false;
                CHECK_oth_execute.Enabled = false;

                BTN_action.Text    = "Change";
                BTN_cancel.Visible = false;

                Refresh_atributes();
            };
        }

        public DialogResult ShowDialog(IWin32Window _owner, string _path, File_System.Inode _inode, int _inode_pos)
        {
            inode = _inode; inode_pos = _inode_pos;

            if (inode.atributes[0] == true && inode.atributes[1] == false) IMG_object.Image = new Bitmap(Data_Keeper.res_folder + "folder_great.ico");
            else if (inode.atributes[0] == false && inode.atributes[1] == true) IMG_object.Image = new Bitmap(Data_Keeper.res_folder + "file_sys_great.ico");

            if (_path.IndexOf('\\') < 0) { TXT_name.Text = _path; TXT_path.Text = ""; }
            else
            {
                TXT_name.Text = _path.Substring(_path.LastIndexOf('\\') + 1);
                TXT_path.Text = _path.Remove(_path.LastIndexOf('\\'));
            }

            TXT_size.Text = inode.size.ToString();
            TXT_creation_date.Text = DateTime.FromBinary(inode.creation_date_time).ToShortDateString();
            TXT_changing_date.Text = DateTime.FromBinary(inode.changing_date_time).ToShortDateString();

            Refresh_atributes();

            return ShowDialog(_owner);
        }

        private void Refresh_atributes()
        {
            CHECK_system.Checked = inode.atributes[12];
            CHECK_hidden.Checked = inode.atributes[11];

            CHECK_o_read.Checked    = inode.atributes[2];
            CHECK_o_write.Checked   = inode.atributes[3];
            CHECK_o_execute.Checked = inode.atributes[4];

            CHECK_o_g_read.Checked    = inode.atributes[5];
            CHECK_o_g_write.Checked   = inode.atributes[6];
            CHECK_o_g_execute.Checked = inode.atributes[7];

            CHECK_oth_read.Checked    = inode.atributes[8];
            CHECK_oth_write.Checked   = inode.atributes[9];
            CHECK_oth_execute.Checked = inode.atributes[10];
        }
    }
}
