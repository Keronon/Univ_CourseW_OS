namespace CourseW
{
    partial class FORM_User
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_User));
            this.TXT_login = new System.Windows.Forms.TextBox();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.BTN_accept = new System.Windows.Forms.Button();
            this.LBL_login = new System.Windows.Forms.Label();
            this.LBL_group = new System.Windows.Forms.Label();
            this.TXT_repeat_password = new System.Windows.Forms.TextBox();
            this.TXT_password = new System.Windows.Forms.TextBox();
            this.LBL_password = new System.Windows.Forms.Label();
            this.NUMERIC_group = new System.Windows.Forms.NumericUpDown();
            this.LBL_repeat_password = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_group)).BeginInit();
            this.SuspendLayout();
            // 
            // TXT_login
            // 
            this.TXT_login.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TXT_login.Location = new System.Drawing.Point(12, 27);
            this.TXT_login.MaxLength = 10;
            this.TXT_login.Name = "TXT_login";
            this.TXT_login.Size = new System.Drawing.Size(204, 23);
            this.TXT_login.TabIndex = 0;
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_cancel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(145, 144);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(127, 27);
            this.BTN_cancel.TabIndex = 5;
            this.BTN_cancel.Text = "CANCEL";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            // 
            // BTN_accept
            // 
            this.BTN_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_accept.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_accept.ForeColor = System.Drawing.Color.DarkBlue;
            this.BTN_accept.Location = new System.Drawing.Point(12, 144);
            this.BTN_accept.Name = "BTN_accept";
            this.BTN_accept.Size = new System.Drawing.Size(127, 27);
            this.BTN_accept.TabIndex = 4;
            this.BTN_accept.Text = "ACCEPT";
            this.BTN_accept.UseVisualStyleBackColor = true;
            // 
            // LBL_login
            // 
            this.LBL_login.AutoSize = true;
            this.LBL_login.Location = new System.Drawing.Point(12, 9);
            this.LBL_login.Name = "LBL_login";
            this.LBL_login.Size = new System.Drawing.Size(42, 15);
            this.LBL_login.TabIndex = 6;
            this.LBL_login.Text = "Login";
            // 
            // LBL_group
            // 
            this.LBL_group.AutoSize = true;
            this.LBL_group.Location = new System.Drawing.Point(222, 9);
            this.LBL_group.Name = "LBL_group";
            this.LBL_group.Size = new System.Drawing.Size(42, 15);
            this.LBL_group.TabIndex = 9;
            this.LBL_group.Text = "Group";
            this.LBL_group.Visible = false;
            // 
            // TXT_repeat_password
            // 
            this.TXT_repeat_password.Location = new System.Drawing.Point(12, 115);
            this.TXT_repeat_password.Name = "TXT_repeat_password";
            this.TXT_repeat_password.PasswordChar = '^';
            this.TXT_repeat_password.Size = new System.Drawing.Size(260, 23);
            this.TXT_repeat_password.TabIndex = 3;
            // 
            // TXT_password
            // 
            this.TXT_password.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TXT_password.Location = new System.Drawing.Point(12, 71);
            this.TXT_password.Name = "TXT_password";
            this.TXT_password.PasswordChar = '^';
            this.TXT_password.Size = new System.Drawing.Size(260, 23);
            this.TXT_password.TabIndex = 2;
            // 
            // LBL_password
            // 
            this.LBL_password.AutoSize = true;
            this.LBL_password.Location = new System.Drawing.Point(12, 53);
            this.LBL_password.Name = "LBL_password";
            this.LBL_password.Size = new System.Drawing.Size(63, 15);
            this.LBL_password.TabIndex = 7;
            this.LBL_password.Text = "Password";
            // 
            // NUMERIC_group
            // 
            this.NUMERIC_group.Location = new System.Drawing.Point(222, 27);
            this.NUMERIC_group.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.NUMERIC_group.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUMERIC_group.Name = "NUMERIC_group";
            this.NUMERIC_group.Size = new System.Drawing.Size(50, 23);
            this.NUMERIC_group.TabIndex = 1;
            this.NUMERIC_group.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NUMERIC_group.Visible = false;
            // 
            // LBL_repeat_password
            // 
            this.LBL_repeat_password.AutoSize = true;
            this.LBL_repeat_password.Location = new System.Drawing.Point(12, 97);
            this.LBL_repeat_password.Name = "LBL_repeat_password";
            this.LBL_repeat_password.Size = new System.Drawing.Size(112, 15);
            this.LBL_repeat_password.TabIndex = 8;
            this.LBL_repeat_password.Text = "Repeat password";
            // 
            // FORM_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 183);
            this.Controls.Add(this.LBL_repeat_password);
            this.Controls.Add(this.NUMERIC_group);
            this.Controls.Add(this.TXT_repeat_password);
            this.Controls.Add(this.LBL_password);
            this.Controls.Add(this.LBL_group);
            this.Controls.Add(this.LBL_login);
            this.Controls.Add(this.BTN_accept);
            this.Controls.Add(this.BTN_cancel);
            this.Controls.Add(this.TXT_password);
            this.Controls.Add(this.TXT_login);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add user";
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_group)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTN_cancel;
        private System.Windows.Forms.Button BTN_accept;
        private System.Windows.Forms.Label LBL_login;
        private System.Windows.Forms.Label LBL_group;
        private System.Windows.Forms.TextBox TXT_repeat_password;
        private System.Windows.Forms.Label LBL_password;
        public System.Windows.Forms.TextBox TXT_login;
        public System.Windows.Forms.TextBox TXT_password;
        public System.Windows.Forms.NumericUpDown NUMERIC_group;
        private System.Windows.Forms.Label LBL_repeat_password;
    }
}