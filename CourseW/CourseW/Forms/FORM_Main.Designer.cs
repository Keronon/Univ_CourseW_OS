namespace CourseW
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
            this.PANEL_cur_action = new System.Windows.Forms.Panel();
            this.BTN_clear_action = new System.Windows.Forms.Button();
            this.LBL_cur_action = new System.Windows.Forms.Label();
            this.BTN_search = new System.Windows.Forms.Button();
            this.TXT_search = new System.Windows.Forms.TextBox();
            this.TREE_view = new System.Windows.Forms.TreeView();
            this.TAB_processes = new System.Windows.Forms.TabPage();
            this.GROUP_tracing = new System.Windows.Forms.GroupBox();
            this.TXT_tracing = new System.Windows.Forms.TextBox();
            this.GROUP_scheduler = new System.Windows.Forms.GroupBox();
            this.LBL_proc_count = new System.Windows.Forms.Label();
            this.NUMERIC_proc_count = new System.Windows.Forms.NumericUpDown();
            this.BTN_stop = new System.Windows.Forms.Button();
            this.BTN_run = new System.Windows.Forms.Button();
            this.TAB_system = new System.Windows.Forms.TabPage();
            this.PANEL_users = new System.Windows.Forms.Panel();
            this.BTN_edit_user = new System.Windows.Forms.Button();
            this.BTN_delete_user = new System.Windows.Forms.Button();
            this.BTN_add_user = new System.Windows.Forms.Button();
            this.LIST_users = new System.Windows.Forms.ListBox();
            this.GROUP_profile = new System.Windows.Forms.GroupBox();
            this.BTN_users = new System.Windows.Forms.Button();
            this.BTN_change_password = new System.Windows.Forms.Button();
            this.BTN_logout = new System.Windows.Forms.Button();
            this.BTN_quit = new System.Windows.Forms.Button();
            this.TXT_user = new System.Windows.Forms.TextBox();
            this.IMG_avatar = new System.Windows.Forms.PictureBox();
            this.IMG_logo = new System.Windows.Forms.PictureBox();
            this.PANEL_change_password = new System.Windows.Forms.Panel();
            this.LBL_repeat_password = new System.Windows.Forms.Label();
            this.LBL_new_password = new System.Windows.Forms.Label();
            this.LBL_old_password = new System.Windows.Forms.Label();
            this.BTN_accept = new System.Windows.Forms.Button();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.TXT_repeat_password = new System.Windows.Forms.TextBox();
            this.TXT_new_password = new System.Windows.Forms.TextBox();
            this.TXT_old_password = new System.Windows.Forms.TextBox();
            this.CONTEXT_directory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CONTEXT_file = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TAB_box.SuspendLayout();
            this.TAB_files.SuspendLayout();
            this.PANEL_cur_action.SuspendLayout();
            this.TAB_processes.SuspendLayout();
            this.GROUP_tracing.SuspendLayout();
            this.GROUP_scheduler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_proc_count)).BeginInit();
            this.TAB_system.SuspendLayout();
            this.PANEL_users.SuspendLayout();
            this.GROUP_profile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_logo)).BeginInit();
            this.PANEL_change_password.SuspendLayout();
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
            this.TAB_files.Controls.Add(this.PANEL_cur_action);
            this.TAB_files.Controls.Add(this.BTN_search);
            this.TAB_files.Controls.Add(this.TXT_search);
            this.TAB_files.Controls.Add(this.TREE_view);
            this.TAB_files.ForeColor = System.Drawing.Color.DarkBlue;
            this.TAB_files.Location = new System.Drawing.Point(4, 24);
            this.TAB_files.Name = "TAB_files";
            this.TAB_files.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_files.Size = new System.Drawing.Size(778, 434);
            this.TAB_files.TabIndex = 0;
            this.TAB_files.Text = "Files";
            this.TAB_files.UseVisualStyleBackColor = true;
            // 
            // PANEL_cur_action
            // 
            this.PANEL_cur_action.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANEL_cur_action.Controls.Add(this.BTN_clear_action);
            this.PANEL_cur_action.Controls.Add(this.LBL_cur_action);
            this.PANEL_cur_action.Location = new System.Drawing.Point(623, 0);
            this.PANEL_cur_action.Name = "PANEL_cur_action";
            this.PANEL_cur_action.Size = new System.Drawing.Size(155, 48);
            this.PANEL_cur_action.TabIndex = 3;
            // 
            // BTN_clear_action
            // 
            this.BTN_clear_action.Location = new System.Drawing.Point(3, 20);
            this.BTN_clear_action.Name = "BTN_clear_action";
            this.BTN_clear_action.Size = new System.Drawing.Size(147, 23);
            this.BTN_clear_action.TabIndex = 1;
            this.BTN_clear_action.Text = "Clear action";
            this.BTN_clear_action.UseVisualStyleBackColor = true;
            this.BTN_clear_action.Click += new System.EventHandler(this.BTN_clear_action_Click);
            // 
            // LBL_cur_action
            // 
            this.LBL_cur_action.AutoSize = true;
            this.LBL_cur_action.Location = new System.Drawing.Point(3, 2);
            this.LBL_cur_action.Name = "LBL_cur_action";
            this.LBL_cur_action.Size = new System.Drawing.Size(147, 15);
            this.LBL_cur_action.TabIndex = 0;
            this.LBL_cur_action.Text = "Current action: none";
            // 
            // BTN_search
            // 
            this.BTN_search.ForeColor = System.Drawing.Color.Black;
            this.BTN_search.Location = new System.Drawing.Point(704, 411);
            this.BTN_search.Name = "BTN_search";
            this.BTN_search.Size = new System.Drawing.Size(74, 23);
            this.BTN_search.TabIndex = 2;
            this.BTN_search.Text = "SEARCH";
            this.BTN_search.UseVisualStyleBackColor = true;
            // 
            // TXT_search
            // 
            this.TXT_search.Location = new System.Drawing.Point(0, 411);
            this.TXT_search.Name = "TXT_search";
            this.TXT_search.Size = new System.Drawing.Size(698, 23);
            this.TXT_search.TabIndex = 1;
            this.TXT_search.Text = "[/]";
            // 
            // TREE_view
            // 
            this.TREE_view.Location = new System.Drawing.Point(0, 0);
            this.TREE_view.Name = "TREE_view";
            this.TREE_view.Size = new System.Drawing.Size(778, 405);
            this.TREE_view.TabIndex = 0;
            // 
            // TAB_processes
            // 
            this.TAB_processes.Controls.Add(this.GROUP_tracing);
            this.TAB_processes.Controls.Add(this.GROUP_scheduler);
            this.TAB_processes.ForeColor = System.Drawing.Color.DarkBlue;
            this.TAB_processes.Location = new System.Drawing.Point(4, 24);
            this.TAB_processes.Name = "TAB_processes";
            this.TAB_processes.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_processes.Size = new System.Drawing.Size(778, 434);
            this.TAB_processes.TabIndex = 1;
            this.TAB_processes.Text = "Processes";
            this.TAB_processes.UseVisualStyleBackColor = true;
            // 
            // GROUP_tracing
            // 
            this.GROUP_tracing.Controls.Add(this.TXT_tracing);
            this.GROUP_tracing.Location = new System.Drawing.Point(179, 6);
            this.GROUP_tracing.Name = "GROUP_tracing";
            this.GROUP_tracing.Size = new System.Drawing.Size(599, 428);
            this.GROUP_tracing.TabIndex = 1;
            this.GROUP_tracing.TabStop = false;
            this.GROUP_tracing.Text = "Tracing";
            // 
            // TXT_tracing
            // 
            this.TXT_tracing.Location = new System.Drawing.Point(6, 22);
            this.TXT_tracing.Multiline = true;
            this.TXT_tracing.Name = "TXT_tracing";
            this.TXT_tracing.ReadOnly = true;
            this.TXT_tracing.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXT_tracing.Size = new System.Drawing.Size(587, 400);
            this.TXT_tracing.TabIndex = 0;
            this.TXT_tracing.TabStop = false;
            // 
            // GROUP_scheduler
            // 
            this.GROUP_scheduler.Controls.Add(this.LBL_proc_count);
            this.GROUP_scheduler.Controls.Add(this.NUMERIC_proc_count);
            this.GROUP_scheduler.Controls.Add(this.BTN_stop);
            this.GROUP_scheduler.Controls.Add(this.BTN_run);
            this.GROUP_scheduler.Location = new System.Drawing.Point(0, 0);
            this.GROUP_scheduler.Name = "GROUP_scheduler";
            this.GROUP_scheduler.Size = new System.Drawing.Size(173, 434);
            this.GROUP_scheduler.TabIndex = 0;
            this.GROUP_scheduler.TabStop = false;
            this.GROUP_scheduler.Text = "Scheduler";
            // 
            // LBL_proc_count
            // 
            this.LBL_proc_count.AutoSize = true;
            this.LBL_proc_count.ForeColor = System.Drawing.Color.Black;
            this.LBL_proc_count.Location = new System.Drawing.Point(6, 145);
            this.LBL_proc_count.Name = "LBL_proc_count";
            this.LBL_proc_count.Size = new System.Drawing.Size(161, 15);
            this.LBL_proc_count.TabIndex = 3;
            this.LBL_proc_count.Text = "Processes count to run";
            // 
            // NUMERIC_proc_count
            // 
            this.NUMERIC_proc_count.Location = new System.Drawing.Point(6, 163);
            this.NUMERIC_proc_count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUMERIC_proc_count.Name = "NUMERIC_proc_count";
            this.NUMERIC_proc_count.Size = new System.Drawing.Size(161, 23);
            this.NUMERIC_proc_count.TabIndex = 2;
            this.NUMERIC_proc_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NUMERIC_proc_count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BTN_stop
            // 
            this.BTN_stop.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_stop.Location = new System.Drawing.Point(6, 241);
            this.BTN_stop.Name = "BTN_stop";
            this.BTN_stop.Size = new System.Drawing.Size(161, 23);
            this.BTN_stop.TabIndex = 1;
            this.BTN_stop.Text = "STOP";
            this.BTN_stop.UseVisualStyleBackColor = true;
            this.BTN_stop.Click += new System.EventHandler(this.BTN_stop_Click);
            // 
            // BTN_run
            // 
            this.BTN_run.ForeColor = System.Drawing.Color.DarkGreen;
            this.BTN_run.Location = new System.Drawing.Point(6, 212);
            this.BTN_run.Name = "BTN_run";
            this.BTN_run.Size = new System.Drawing.Size(161, 23);
            this.BTN_run.TabIndex = 0;
            this.BTN_run.Text = "RUN";
            this.BTN_run.UseVisualStyleBackColor = true;
            this.BTN_run.Click += new System.EventHandler(this.BTN_run_Click);
            // 
            // TAB_system
            // 
            this.TAB_system.Controls.Add(this.PANEL_users);
            this.TAB_system.Controls.Add(this.GROUP_profile);
            this.TAB_system.Controls.Add(this.IMG_logo);
            this.TAB_system.Controls.Add(this.PANEL_change_password);
            this.TAB_system.ForeColor = System.Drawing.Color.DarkBlue;
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
            this.TXT_user.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TXT_user.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TXT_user.Location = new System.Drawing.Point(50, 214);
            this.TXT_user.Name = "TXT_user";
            this.TXT_user.ReadOnly = true;
            this.TXT_user.Size = new System.Drawing.Size(200, 23);
            this.TXT_user.TabIndex = 1;
            this.TXT_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // PANEL_change_password
            // 
            this.PANEL_change_password.Controls.Add(this.LBL_repeat_password);
            this.PANEL_change_password.Controls.Add(this.LBL_new_password);
            this.PANEL_change_password.Controls.Add(this.LBL_old_password);
            this.PANEL_change_password.Controls.Add(this.BTN_accept);
            this.PANEL_change_password.Controls.Add(this.BTN_cancel);
            this.PANEL_change_password.Controls.Add(this.TXT_repeat_password);
            this.PANEL_change_password.Controls.Add(this.TXT_new_password);
            this.PANEL_change_password.Controls.Add(this.TXT_old_password);
            this.PANEL_change_password.Location = new System.Drawing.Point(311, 4);
            this.PANEL_change_password.Name = "PANEL_change_password";
            this.PANEL_change_password.Size = new System.Drawing.Size(464, 421);
            this.PANEL_change_password.TabIndex = 1;
            // 
            // LBL_repeat_password
            // 
            this.LBL_repeat_password.AutoSize = true;
            this.LBL_repeat_password.Location = new System.Drawing.Point(108, 205);
            this.LBL_repeat_password.Name = "LBL_repeat_password";
            this.LBL_repeat_password.Size = new System.Drawing.Size(112, 15);
            this.LBL_repeat_password.TabIndex = 7;
            this.LBL_repeat_password.Text = "Repeat password";
            // 
            // LBL_new_password
            // 
            this.LBL_new_password.AutoSize = true;
            this.LBL_new_password.Location = new System.Drawing.Point(108, 161);
            this.LBL_new_password.Name = "LBL_new_password";
            this.LBL_new_password.Size = new System.Drawing.Size(91, 15);
            this.LBL_new_password.TabIndex = 6;
            this.LBL_new_password.Text = "New password";
            // 
            // LBL_old_password
            // 
            this.LBL_old_password.AutoSize = true;
            this.LBL_old_password.Location = new System.Drawing.Point(108, 117);
            this.LBL_old_password.Name = "LBL_old_password";
            this.LBL_old_password.Size = new System.Drawing.Size(91, 15);
            this.LBL_old_password.TabIndex = 5;
            this.LBL_old_password.Text = "Old password";
            // 
            // BTN_accept
            // 
            this.BTN_accept.Location = new System.Drawing.Point(111, 274);
            this.BTN_accept.Name = "BTN_accept";
            this.BTN_accept.Size = new System.Drawing.Size(122, 23);
            this.BTN_accept.TabIndex = 3;
            this.BTN_accept.Text = "ACCEPT";
            this.BTN_accept.UseVisualStyleBackColor = true;
            this.BTN_accept.Click += new System.EventHandler(this.BTN_accept_Click);
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(239, 274);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(122, 23);
            this.BTN_cancel.TabIndex = 4;
            this.BTN_cancel.Text = "CANCEL";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            this.BTN_cancel.Click += new System.EventHandler(this.BTN_cancel_Click);
            // 
            // TXT_repeat_password
            // 
            this.TXT_repeat_password.Location = new System.Drawing.Point(111, 223);
            this.TXT_repeat_password.MaxLength = 35;
            this.TXT_repeat_password.Name = "TXT_repeat_password";
            this.TXT_repeat_password.PasswordChar = '^';
            this.TXT_repeat_password.Size = new System.Drawing.Size(250, 23);
            this.TXT_repeat_password.TabIndex = 2;
            this.TXT_repeat_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_new_password
            // 
            this.TXT_new_password.Location = new System.Drawing.Point(111, 179);
            this.TXT_new_password.MaxLength = 35;
            this.TXT_new_password.Name = "TXT_new_password";
            this.TXT_new_password.PasswordChar = '^';
            this.TXT_new_password.Size = new System.Drawing.Size(250, 23);
            this.TXT_new_password.TabIndex = 1;
            this.TXT_new_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TXT_old_password
            // 
            this.TXT_old_password.Location = new System.Drawing.Point(111, 135);
            this.TXT_old_password.MaxLength = 35;
            this.TXT_old_password.Name = "TXT_old_password";
            this.TXT_old_password.PasswordChar = '^';
            this.TXT_old_password.Size = new System.Drawing.Size(250, 23);
            this.TXT_old_password.TabIndex = 0;
            this.TXT_old_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CONTEXT_directory
            // 
            this.CONTEXT_directory.Name = "CONTEXT_menu";
            this.CONTEXT_directory.Size = new System.Drawing.Size(61, 4);
            // 
            // CONTEXT_file
            // 
            this.CONTEXT_file.Name = "CONTEXT_file";
            this.CONTEXT_file.Size = new System.Drawing.Size(61, 4);
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
            this.TAB_files.PerformLayout();
            this.PANEL_cur_action.ResumeLayout(false);
            this.PANEL_cur_action.PerformLayout();
            this.TAB_processes.ResumeLayout(false);
            this.GROUP_tracing.ResumeLayout(false);
            this.GROUP_tracing.PerformLayout();
            this.GROUP_scheduler.ResumeLayout(false);
            this.GROUP_scheduler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_proc_count)).EndInit();
            this.TAB_system.ResumeLayout(false);
            this.PANEL_users.ResumeLayout(false);
            this.GROUP_profile.ResumeLayout(false);
            this.GROUP_profile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_logo)).EndInit();
            this.PANEL_change_password.ResumeLayout(false);
            this.PANEL_change_password.PerformLayout();
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
        private System.Windows.Forms.TextBox TXT_repeat_password;
        private System.Windows.Forms.TextBox TXT_new_password;
        private System.Windows.Forms.TextBox TXT_old_password;
        private System.Windows.Forms.PictureBox IMG_logo;
        private System.Windows.Forms.Button BTN_users;
        private System.Windows.Forms.Panel PANEL_users;
        private System.Windows.Forms.Button BTN_edit_user;
        private System.Windows.Forms.Button BTN_delete_user;
        private System.Windows.Forms.Button BTN_add_user;
        private System.Windows.Forms.ContextMenuStrip CONTEXT_directory;
        private System.Windows.Forms.ContextMenuStrip CONTEXT_file;
        private System.Windows.Forms.Button BTN_search;
        private System.Windows.Forms.TextBox TXT_search;
        private System.Windows.Forms.Panel PANEL_cur_action;
        private System.Windows.Forms.Button BTN_clear_action;
        private System.Windows.Forms.Label LBL_cur_action;
        private System.Windows.Forms.Label LBL_repeat_password;
        private System.Windows.Forms.Label LBL_new_password;
        private System.Windows.Forms.Label LBL_old_password;
        public System.Windows.Forms.ListBox LIST_users;
        private System.Windows.Forms.GroupBox GROUP_tracing;
        private System.Windows.Forms.GroupBox GROUP_scheduler;
        private System.Windows.Forms.Button BTN_run;
        private System.Windows.Forms.Label LBL_proc_count;
        private System.Windows.Forms.Button BTN_stop;
        public System.Windows.Forms.TextBox TXT_tracing;
        private System.Windows.Forms.NumericUpDown NUMERIC_proc_count;
    }
}