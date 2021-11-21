﻿namespace CourseW
{
    partial class FORM_Authorization
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Authorization));
            this.TXT_password = new System.Windows.Forms.TextBox();
            this.BTN_login = new System.Windows.Forms.Button();
            this.BTN_quit = new System.Windows.Forms.Button();
            this.IMG_avatar = new System.Windows.Forms.PictureBox();
            this.COMBO_user = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // TXT_password
            // 
            this.TXT_password.Location = new System.Drawing.Point(12, 197);
            this.TXT_password.MaxLength = 35;
            this.TXT_password.Name = "TXT_password";
            this.TXT_password.PasswordChar = '^';
            this.TXT_password.Size = new System.Drawing.Size(260, 23);
            this.TXT_password.TabIndex = 2;
            // 
            // BTN_login
            // 
            this.BTN_login.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BTN_login.Location = new System.Drawing.Point(12, 226);
            this.BTN_login.Name = "BTN_login";
            this.BTN_login.Size = new System.Drawing.Size(205, 23);
            this.BTN_login.TabIndex = 3;
            this.BTN_login.Text = "LOG-IN";
            this.BTN_login.UseVisualStyleBackColor = true;
            this.BTN_login.Click += new System.EventHandler(this.BTN_login_Click);
            // 
            // BTN_quit
            // 
            this.BTN_quit.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_quit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BTN_quit.Location = new System.Drawing.Point(223, 226);
            this.BTN_quit.Name = "BTN_quit";
            this.BTN_quit.Size = new System.Drawing.Size(49, 23);
            this.BTN_quit.TabIndex = 4;
            this.BTN_quit.Text = "QUIT";
            this.BTN_quit.UseVisualStyleBackColor = true;
            this.BTN_quit.Click += new System.EventHandler(this.BTN_quit_Click);
            // 
            // IMG_avatar
            // 
            this.IMG_avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IMG_avatar.Image = global::CourseW.Properties.Resources.tech;
            this.IMG_avatar.Location = new System.Drawing.Point(67, 12);
            this.IMG_avatar.Name = "IMG_avatar";
            this.IMG_avatar.Size = new System.Drawing.Size(150, 150);
            this.IMG_avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IMG_avatar.TabIndex = 5;
            this.IMG_avatar.TabStop = false;
            // 
            // COMBO_user
            // 
            this.COMBO_user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_user.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.COMBO_user.FormattingEnabled = true;
            this.COMBO_user.Items.AddRange(new object[] {
            "ADMINISTRATOR"});
            this.COMBO_user.Location = new System.Drawing.Point(12, 168);
            this.COMBO_user.MaxLength = 35;
            this.COMBO_user.Name = "COMBO_user";
            this.COMBO_user.Size = new System.Drawing.Size(260, 23);
            this.COMBO_user.TabIndex = 1;
            // 
            // FORM_Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.IMG_avatar);
            this.Controls.Add(this.BTN_quit);
            this.Controls.Add(this.BTN_login);
            this.Controls.Add(this.TXT_password);
            this.Controls.Add(this.COMBO_user);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorization";
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TXT_password;
        private System.Windows.Forms.Button BTN_login;
        private System.Windows.Forms.Button BTN_quit;
        private System.Windows.Forms.PictureBox IMG_avatar;
        private System.Windows.Forms.ComboBox COMBO_user;
    }
}
