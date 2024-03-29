﻿using System;
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
    public partial class FORM_Input : Form
    {
        public FORM_Input()
        {
            InitializeComponent();

            TXT_input.KeyUp += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BTN_accept.PerformClick();
                }
            };
        }

        public DialogResult ShowDialog(IWin32Window _owner, string _title)
        {
            Text = _title;
            return ShowDialog(_owner);
        }
    }
}
