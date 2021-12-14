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
    public partial class FORM_User : Form
    {
        public FORM_User()
        {
            InitializeComponent();

            TXT_repeat_password.TextChanged += (o, e) =>
            {
                if (TXT_repeat_password.Text != TXT_password.Text)
                {
                    TXT_repeat_password.BackColor = Color.Red;
                    BTN_accept.Enabled = false;
                }
                else
                {
                    TXT_repeat_password.BackColor = DefaultBackColor;
                    BTN_accept.Enabled = true;
                }
            };
        }

        public DialogResult ShowDialog(IWin32Window _owner, File_System.User _user)
        {
            Text = "Edit user";

            TXT_login.Text = new string(_user.login).Trim();
            NUMERIC_group.Value = _user.group;
            TXT_password.Enabled = TXT_repeat_password.Enabled = false;

            TXT_login.TextChanged += (o, e) =>
            {
                if (Data_Keeper.FORM_Main.LIST_users.FindStringExact(TXT_login.Text) > 0)
                {
                    TXT_login.BackColor = Color.Red;
                    BTN_accept.Enabled = false;
                }
                else
                {
                    TXT_login.BackColor = DefaultBackColor;
                    BTN_accept.Enabled = true;
                }
            };

            return ShowDialog(_owner);
        }
    }
}
