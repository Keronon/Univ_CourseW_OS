﻿namespace CourseW
{
    partial class FORM_Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Main));
            this.TAB_box = new System.Windows.Forms.TabControl();
            this.TAB_files = new System.Windows.Forms.TabPage();
            this.TREE_view = new System.Windows.Forms.TreeView();
            this.TAB_processes = new System.Windows.Forms.TabPage();
            this.TAB_system = new System.Windows.Forms.TabPage();
            this.PANEL_users = new System.Windows.Forms.Panel();
            this.BTN_edit_user = new System.Windows.Forms.Button();
            this.BTN_delete_user = new System.Windows.Forms.Button();
            this.BTN_add_user = new System.Windows.Forms.Button();
            this.LIST_users = new System.Windows.Forms.ListBox();
            this.PANEL_change_password = new System.Windows.Forms.Panel();
            this.BTN_accept = new System.Windows.Forms.Button();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.TXT_repeat = new System.Windows.Forms.TextBox();
            this.TXT_new_password = new System.Windows.Forms.TextBox();
            this.TXT_old_password = new System.Windows.Forms.TextBox();
            this.GROUP_profile = new System.Windows.Forms.GroupBox();
            this.BTN_users = new System.Windows.Forms.Button();
            this.BTN_change_password = new System.Windows.Forms.Button();
            this.BTN_logout = new System.Windows.Forms.Button();
            this.BTN_quit = new System.Windows.Forms.Button();
            this.TXT_user = new System.Windows.Forms.TextBox();
            this.CONTEXT_directory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.IMG_avatar = new System.Windows.Forms.PictureBox();
            this.IMG_logo = new System.Windows.Forms.PictureBox();
            this.CONTEXT_file = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TAB_box.SuspendLayout();
            this.TAB_files.SuspendLayout();
            this.TAB_system.SuspendLayout();
            this.PANEL_users.SuspendLayout();
            this.PANEL_change_password.SuspendLayout();
            this.GROUP_profile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // TAB_box
            // 
            this.TAB_box.Controls.Add(this.TAB_files);
            this.TAB_box.Controls.Add(this.TAB_processes);
            this.TAB_box.Controls.Add(this.TAB_system);
            this.TAB_box.Location = new System.Drawing.Point(0, 0);
            this.TAB_box.Name = "TAB_box";
            this.TAB_box.SelectedIndex = 0;
            this.TAB_box.Size = new System.Drawing.Size(786, 462);
            this.TAB_box.TabIndex = 0;
            // 
            // TAB_files
            // 
            this.TAB_files.Controls.Add(this.TREE_view);
            this.TAB_files.Location = new System.Drawing.Point(4, 24);
            this.TAB_files.Name = "TAB_files";
            this.TAB_files.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_files.Size = new System.Drawing.Size(778, 434);
            this.TAB_files.TabIndex = 0;
            this.TAB_files.Text = "Files";
            this.TAB_files.UseVisualStyleBackColor = true;
            // 
            // TREE_view
            // 
            this.TREE_view.Location = new System.Drawing.Point(0, 0);
            this.TREE_view.Name = "TREE_view";
            this.TREE_view.Size = new System.Drawing.Size(778, 434);
            this.TREE_view.TabIndex = 0;
            // 
            // TAB_processes
            // 
            this.TAB_processes.Location = new System.Drawing.Point(4, 24);
            this.TAB_processes.Name = "TAB_processes";
            this.TAB_processes.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_processes.Size = new System.Drawing.Size(778, 434);
            this.TAB_processes.TabIndex = 1;
            this.TAB_processes.Text = "Processes";
            this.TAB_processes.UseVisualStyleBackColor = true;
            // 
            // TAB_system
            // 
            this.TAB_system.Controls.Add(this.PANEL_users);
            this.TAB_system.Controls.Add(this.PANEL_change_password);
            this.TAB_system.Controls.Add(this.GROUP_profile);
            this.TAB_system.Controls.Add(this.IMG_logo);
            this.TAB_system.Location = new System.Drawing.Point(4, 24);
            this.TAB_system.Name = "TAB_system";
            this.TAB_system.Size = new System.Drawing.Size(778, 434);
            this.TAB_system.TabIndex = 2;
            this.TAB_system.Text = "System";
            this.TAB_system.UseVisualStyleBackColor = true;
            // 
            // PANEL_users
            // 
            this.PANEL_users.Controls.Add(this.BTN_edit_user);
            this.PANEL_users.Controls.Add(this.BTN_delete_user);
            this.PANEL_users.Controls.Add(this.BTN_add_user);
            this.PANEL_users.Controls.Add(this.LIST_users);
            this.PANEL_users.Location = new System.Drawing.Point(311, 4);
            this.PANEL_users.Name = "PANEL_users";
            this.PANEL_users.Size = new System.Drawing.Size(464, 421);
            this.PANEL_users.TabIndex = 6;
            // 
            // BTN_edit_user
            // 
            this.BTN_edit_user.Location = new System.Drawing.Point(312, 392);
            this.BTN_edit_user.Name = "BTN_edit_user";
            this.BTN_edit_user.Size = new System.Drawing.Size(149, 23);
            this.BTN_edit_user.TabIndex = 3;
            this.BTN_edit_user.Text = "EDIT";
            this.BTN_edit_user.UseVisualStyleBackColor = true;
            this.BTN_edit_user.Click += new System.EventHandler(this.BTN_edit_user_Click);
            // 
            // BTN_delete_user
            // 
            this.BTN_delete_user.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_delete_user.Location = new System.Drawing.Point(158, 392);
            this.BTN_delete_user.Name = "BTN_delete_user";
            this.BTN_delete_user.Size = new System.Drawing.Size(148, 23);
            this.BTN_delete_user.TabIndex = 2;
            this.BTN_delete_user.Text = "DELETE";
            this.BTN_delete_user.UseVisualStyleBackColor = true;
            this.BTN_delete_user.Click += new System.EventHandler(this.BTN_delete_user_Click);
            // 
            // BTN_add_user
            // 
            this.BTN_add_user.ForeColor = System.Drawing.Color.DarkGreen;
            this.BTN_add_user.Location = new System.Drawing.Point(3, 392);
            this.BTN_add_user.Name = "BTN_add_user";
            this.BTN_add_user.Size = new System.Drawing.Size(149, 23);
            this.BTN_add_user.TabIndex = 1;
            this.BTN_add_user.Text = "ADD";
            this.BTN_add_user.UseVisualStyleBackColor = true;
            this.BTN_add_user.Click += new System.EventHandler(this.BTN_add_user_Click);
            // 
            // LIST_users
            // 
            this.LIST_users.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LIST_users.FormattingEnabled = true;
            this.LIST_users.ItemHeight = 15;
            this.LIST_users.Location = new System.Drawing.Point(3, 3);
            this.LIST_users.Name = "LIST_users";
            this.LIST_users.Size = new System.Drawing.Size(458, 379);
            this.LIST_users.TabIndex = 0;
            // 
            // PANEL_change_password
            // 
            this.PANEL_change_password.Controls.Add(this.BTN_accept);
            this.PANEL_change_password.Controls.Add(this.BTN_cancel);
            this.PANEL_change_password.Controls.Add(this.TXT_repeat);
            this.PANEL_change_password.Controls.Add(this.TXT_new_password);
            this.PANEL_change_password.Controls.Add(this.TXT_old_password);
            this.PANEL_change_password.Location = new System.Drawing.Point(311, 4);
            this.PANEL_change_password.Name = "PANEL_change_password";
            this.PANEL_change_password.Size = new System.Drawing.Size(464, 421);
            this.PANEL_change_password.TabIndex = 1;
            // 
            // BTN_accept
            // 
            this.BTN_accept.Location = new System.Drawing.Point(235, 255);
            this.BTN_accept.Name = "BTN_accept";
            this.BTN_accept.Size = new System.Drawing.Size(122, 23);
            this.BTN_accept.TabIndex = 4;
            this.BTN_accept.Text = "ACCEPT";
            this.BTN_accept.UseVisualStyleBackColor = true;
            this.BTN_accept.Click += new System.EventHandler(this.BTN_accept_Click);
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(107, 255);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(122, 23);
            this.BTN_cancel.TabIndex = 3;
            this.BTN_cancel.Text = "CANCEL";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            this.BTN_cancel.Click += new System.EventHandler(this.BTN_cancel_Click);
            // 
            // TXT_repeat
            // 
            this.TXT_repeat.Location = new System.Drawing.Point(107, 206);
            this.TXT_repeat.MaxLength = 35;
            this.TXT_repeat.Name = "TXT_repeat";
            this.TXT_repeat.PasswordChar = '^';
            this.TXT_repeat.Size = new System.Drawing.Size(250, 23);
            this.TXT_repeat.TabIndex = 2;
            this.TXT_repeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_new_password
            // 
            this.TXT_new_password.Location = new System.Drawing.Point(107, 177);
            this.TXT_new_password.MaxLength = 35;
            this.TXT_new_password.Name = "TXT_new_password";
            this.TXT_new_password.PasswordChar = '^';
            this.TXT_new_password.Size = new System.Drawing.Size(250, 23);
            this.TXT_new_password.TabIndex = 1;
            this.TXT_new_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_old_password
            // 
            this.TXT_old_password.Location = new System.Drawing.Point(107, 143);
            this.TXT_old_password.MaxLength = 35;
            this.TXT_old_password.Name = "TXT_old_password";
            this.TXT_old_password.PasswordChar = '^';
            this.TXT_old_password.Size = new System.Drawing.Size(250, 23);
            this.TXT_old_password.TabIndex = 0;
            this.TXT_old_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GROUP_profile
            // 
            this.GROUP_profile.Controls.Add(this.BTN_users);
            this.GROUP_profile.Controls.Add(this.BTN_change_password);
            this.GROUP_profile.Controls.Add(this.BTN_logout);
            this.GROUP_profile.Controls.Add(this.BTN_quit);
            this.GROUP_profile.Controls.Add(this.TXT_user);
            this.GROUP_profile.Controls.Add(this.IMG_avatar);
            this.GROUP_profile.Location = new System.Drawing.Point(4, 4);
            this.GROUP_profile.Name = "GROUP_profile";
            this.GROUP_profile.Size = new System.Drawing.Size(300, 421);
            this.GROUP_profile.TabIndex = 0;
            this.GROUP_profile.TabStop = false;
            this.GROUP_profile.Text = "PROFILE";
            // 
            // BTN_users
            // 
            this.BTN_users.Location = new System.Drawing.Point(50, 302);
            this.BTN_users.Name = "BTN_users";
            this.BTN_users.Size = new System.Drawing.Size(200, 23);
            this.BTN_users.TabIndex = 5;
            this.BTN_users.Text = "USERS";
            this.BTN_users.UseVisualStyleBackColor = true;
            this.BTN_users.Click += new System.EventHandler(this.BTN_users_Click);
            // 
            // BTN_change_password
            // 
            this.BTN_change_password.Location = new System.Drawing.Point(50, 272);
            this.BTN_change_password.Name = "BTN_change_password";
            this.BTN_change_password.Size = new System.Drawing.Size(200, 23);
            this.BTN_change_password.TabIndex = 4;
            this.BTN_change_password.Text = "CHANGE PASSWORD";
            this.BTN_change_password.UseVisualStyleBackColor = true;
            this.BTN_change_password.Click += new System.EventHandler(this.BTN_change_password_Click);
            // 
            // BTN_logout
            // 
            this.BTN_logout.ForeColor = System.Drawing.Color.DarkOrange;
            this.BTN_logout.Location = new System.Drawing.Point(50, 363);
            this.BTN_logout.Name = "BTN_logout";
            this.BTN_logout.Size = new System.Drawing.Size(200, 23);
            this.BTN_logout.TabIndex = 3;
            this.BTN_logout.Text = "LOG-OUT";
            this.BTN_logout.UseVisualStyleBackColor = true;
            this.BTN_logout.Click += new System.EventHandler(this.BTN_logout_Click);
            // 
            // BTN_quit
            // 
            this.BTN_quit.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_quit.Location = new System.Drawing.Point(50, 392);
            this.BTN_quit.Name = "BTN_quit";
            this.BTN_quit.Size = new System.Drawing.Size(200, 23);
            this.BTN_quit.TabIndex = 2;
            this.BTN_quit.Text = "QUIT";
            this.BTN_quit.UseVisualStyleBackColor = true;
            this.BTN_quit.Click += new System.EventHandler(this.BTN_quit_Click);
            // 
            // TXT_user
            // 
            this.TXT_user.Enabled = false;
            this.TXT_user.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TXT_user.Location = new System.Drawing.Point(50, 214);
            this.TXT_user.Name = "TXT_user";
            this.TXT_user.ReadOnly = true;
            this.TXT_user.Size = new System.Drawing.Size(200, 23);
            this.TXT_user.TabIndex = 1;
            this.TXT_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CONTEXT_directory
            // 
            this.CONTEXT_directory.Name = "CONTEXT_menu";
            this.CONTEXT_directory.Size = new System.Drawing.Size(61, 4);
            // 
            // IMG_avatar
            // 
            this.IMG_avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IMG_avatar.Image = global::CourseW.Properties.Resources.tech;
            this.IMG_avatar.Location = new System.Drawing.Point(50, 22);
            this.IMG_avatar.Name = "IMG_avatar";
            this.IMG_avatar.Size = new System.Drawing.Size(200, 185);
            this.IMG_avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IMG_avatar.TabIndex = 0;
            this.IMG_avatar.TabStop = false;
            // 
            // IMG_logo
            // 
            this.IMG_logo.Image = global::CourseW.Properties.Resources.tech;
            this.IMG_logo.Location = new System.Drawing.Point(311, 4);
            this.IMG_logo.Name = "IMG_logo";
            this.IMG_logo.Size = new System.Drawing.Size(464, 421);
            this.IMG_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.IMG_logo.TabIndex = 5;
            this.IMG_logo.TabStop = false;
            // 
            // CONTEXT_file
            // 
            this.CONTEXT_file.Name = "CONTEXT_file";
            this.CONTEXT_file.Size = new System.Drawing.Size(181, 26);
            // 
            // FORM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.ControlBox = false;
            this.Controls.Add(this.TAB_box);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Course Work";
            this.TAB_box.ResumeLayout(false);
            this.TAB_files.ResumeLayout(false);
            this.TAB_system.ResumeLayout(false);
            this.PANEL_users.ResumeLayout(false);
            this.PANEL_change_password.ResumeLayout(false);
            this.PANEL_change_password.PerformLayout();
            this.GROUP_profile.ResumeLayout(false);
            this.GROUP_profile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TAB_box;
        private System.Windows.Forms.TabPage TAB_files;
        private System.Windows.Forms.TabPage TAB_processes;
        private System.Windows.Forms.TabPage TAB_system;
        private System.Windows.Forms.GroupBox GROUP_profile;
        private System.Windows.Forms.PictureBox IMG_avatar;
        private System.Windows.Forms.TreeView TREE_view;
        private System.Windows.Forms.TextBox TXT_user;
        private System.Windows.Forms.Button BTN_logout;
        private System.Windows.Forms.Button BTN_quit;
        private System.Windows.Forms.Button BTN_change_password;
        private System.Windows.Forms.Panel PANEL_change_password;
        private System.Windows.Forms.Button BTN_accept;
        private System.Windows.Forms.Button BTN_cancel;
        private System.Windows.Forms.TextBox TXT_repeat;
        private System.Windows.Forms.TextBox TXT_new_password;
        private System.Windows.Forms.TextBox TXT_old_password;
        private System.Windows.Forms.PictureBox IMG_logo;
        private System.Windows.Forms.Button BTN_users;
        private System.Windows.Forms.Panel PANEL_users;
        private System.Windows.Forms.Button BTN_edit_user;
        private System.Windows.Forms.Button BTN_delete_user;
        private System.Windows.Forms.Button BTN_add_user;
        private System.Windows.Forms.ListBox LIST_users;
        private System.Windows.Forms.ContextMenuStrip CONTEXT_directory;
        private System.Windows.Forms.ContextMenuStrip CONTEXT_file;
    }
}