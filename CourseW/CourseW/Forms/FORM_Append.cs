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
    public partial class FORM_Append : Form
    {
        public FORM_Append()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IWin32Window _owner, string _title, string _file)
        {
            Text = _title;
            TXT_file.Text = _file;
            return ShowDialog(_owner);
        }
    }
}
