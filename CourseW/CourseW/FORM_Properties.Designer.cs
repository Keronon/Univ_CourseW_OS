namespace CourseW
{
    partial class FORM_Properties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Properties));
            this.TAB_box = new System.Windows.Forms.TabControl();
            this.TAB_info = new System.Windows.Forms.TabPage();
            this.FLOW_info = new System.Windows.Forms.FlowLayoutPanel();
            this.IMG_object = new System.Windows.Forms.PictureBox();
            this.LBL_name = new System.Windows.Forms.Label();
            this.TXT_name = new System.Windows.Forms.TextBox();
            this.LBL_path = new System.Windows.Forms.Label();
            this.TXT_path = new System.Windows.Forms.TextBox();
            this.LBL_size = new System.Windows.Forms.Label();
            this.TXT_size = new System.Windows.Forms.TextBox();
            this.LBL_creation_date = new System.Windows.Forms.Label();
            this.TXT_creation_date = new System.Windows.Forms.TextBox();
            this.LBL_changing_date = new System.Windows.Forms.Label();
            this.TXT_changing_date = new System.Windows.Forms.TextBox();
            this.TAB_rights = new System.Windows.Forms.TabPage();
            this.FLOW_rights = new System.Windows.Forms.FlowLayoutPanel();
            this.CHECK_system = new System.Windows.Forms.CheckBox();
            this.CHECK_hidden = new System.Windows.Forms.CheckBox();
            this.LBL_owner = new System.Windows.Forms.Label();
            this.CHECK_o_read = new System.Windows.Forms.CheckBox();
            this.CHECK_o_write = new System.Windows.Forms.CheckBox();
            this.CHECK_o_execute = new System.Windows.Forms.CheckBox();
            this.LBL_owner_group = new System.Windows.Forms.Label();
            this.CHECK_o_g_read = new System.Windows.Forms.CheckBox();
            this.CHECK_o_g_write = new System.Windows.Forms.CheckBox();
            this.CHECK_o_g_execute = new System.Windows.Forms.CheckBox();
            this.LBL_other = new System.Windows.Forms.Label();
            this.CHECK_oth_read = new System.Windows.Forms.CheckBox();
            this.CHECK_oth_write = new System.Windows.Forms.CheckBox();
            this.CHECK_oth_execute = new System.Windows.Forms.CheckBox();
            this.BTN_action = new System.Windows.Forms.Button();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.TAB_box.SuspendLayout();
            this.TAB_info.SuspendLayout();
            this.FLOW_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_object)).BeginInit();
            this.TAB_rights.SuspendLayout();
            this.FLOW_rights.SuspendLayout();
            this.SuspendLayout();
            // 
            // TAB_box
            // 
            this.TAB_box.Controls.Add(this.TAB_info);
            this.TAB_box.Controls.Add(this.TAB_rights);
            this.TAB_box.Location = new System.Drawing.Point(0, 0);
            this.TAB_box.Name = "TAB_box";
            this.TAB_box.SelectedIndex = 0;
            this.TAB_box.Size = new System.Drawing.Size(284, 361);
            this.TAB_box.TabIndex = 0;
            // 
            // TAB_info
            // 
            this.TAB_info.Controls.Add(this.FLOW_info);
            this.TAB_info.Location = new System.Drawing.Point(4, 24);
            this.TAB_info.Name = "TAB_info";
            this.TAB_info.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_info.Size = new System.Drawing.Size(276, 333);
            this.TAB_info.TabIndex = 0;
            this.TAB_info.Text = "Information";
            this.TAB_info.UseVisualStyleBackColor = true;
            // 
            // FLOW_info
            // 
            this.FLOW_info.Controls.Add(this.IMG_object);
            this.FLOW_info.Controls.Add(this.LBL_name);
            this.FLOW_info.Controls.Add(this.TXT_name);
            this.FLOW_info.Controls.Add(this.LBL_path);
            this.FLOW_info.Controls.Add(this.TXT_path);
            this.FLOW_info.Controls.Add(this.LBL_size);
            this.FLOW_info.Controls.Add(this.TXT_size);
            this.FLOW_info.Controls.Add(this.LBL_creation_date);
            this.FLOW_info.Controls.Add(this.TXT_creation_date);
            this.FLOW_info.Controls.Add(this.LBL_changing_date);
            this.FLOW_info.Controls.Add(this.TXT_changing_date);
            this.FLOW_info.Location = new System.Drawing.Point(6, 6);
            this.FLOW_info.Name = "FLOW_info";
            this.FLOW_info.Size = new System.Drawing.Size(264, 321);
            this.FLOW_info.TabIndex = 6;
            // 
            // IMG_object
            // 
            this.IMG_object.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FLOW_info.SetFlowBreak(this.IMG_object, true);
            this.IMG_object.Location = new System.Drawing.Point(90, 3);
            this.IMG_object.Margin = new System.Windows.Forms.Padding(90, 3, 3, 3);
            this.IMG_object.Name = "IMG_object";
            this.IMG_object.Size = new System.Drawing.Size(90, 90);
            this.IMG_object.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IMG_object.TabIndex = 10;
            this.IMG_object.TabStop = false;
            // 
            // LBL_name
            // 
            this.LBL_name.AutoSize = true;
            this.FLOW_info.SetFlowBreak(this.LBL_name, true);
            this.LBL_name.Location = new System.Drawing.Point(3, 96);
            this.LBL_name.Name = "LBL_name";
            this.LBL_name.Size = new System.Drawing.Size(35, 15);
            this.LBL_name.TabIndex = 1;
            this.LBL_name.Text = "Name";
            // 
            // TXT_name
            // 
            this.TXT_name.Location = new System.Drawing.Point(3, 114);
            this.TXT_name.Name = "TXT_name";
            this.TXT_name.ReadOnly = true;
            this.TXT_name.Size = new System.Drawing.Size(258, 23);
            this.TXT_name.TabIndex = 0;
            // 
            // LBL_path
            // 
            this.LBL_path.AutoSize = true;
            this.FLOW_info.SetFlowBreak(this.LBL_path, true);
            this.LBL_path.Location = new System.Drawing.Point(3, 140);
            this.LBL_path.Name = "LBL_path";
            this.LBL_path.Size = new System.Drawing.Size(35, 15);
            this.LBL_path.TabIndex = 2;
            this.LBL_path.Text = "Path";
            // 
            // TXT_path
            // 
            this.TXT_path.Location = new System.Drawing.Point(3, 158);
            this.TXT_path.Name = "TXT_path";
            this.TXT_path.ReadOnly = true;
            this.TXT_path.Size = new System.Drawing.Size(258, 23);
            this.TXT_path.TabIndex = 3;
            // 
            // LBL_size
            // 
            this.LBL_size.AutoSize = true;
            this.FLOW_info.SetFlowBreak(this.LBL_size, true);
            this.LBL_size.Location = new System.Drawing.Point(3, 184);
            this.LBL_size.Name = "LBL_size";
            this.LBL_size.Size = new System.Drawing.Size(35, 15);
            this.LBL_size.TabIndex = 4;
            this.LBL_size.Text = "Size";
            // 
            // TXT_size
            // 
            this.TXT_size.Location = new System.Drawing.Point(3, 202);
            this.TXT_size.Name = "TXT_size";
            this.TXT_size.ReadOnly = true;
            this.TXT_size.Size = new System.Drawing.Size(258, 23);
            this.TXT_size.TabIndex = 5;
            // 
            // LBL_creation_date
            // 
            this.LBL_creation_date.AutoSize = true;
            this.FLOW_info.SetFlowBreak(this.LBL_creation_date, true);
            this.LBL_creation_date.Location = new System.Drawing.Point(3, 228);
            this.LBL_creation_date.Name = "LBL_creation_date";
            this.LBL_creation_date.Size = new System.Drawing.Size(98, 15);
            this.LBL_creation_date.TabIndex = 6;
            this.LBL_creation_date.Text = "Creation date";
            // 
            // TXT_creation_date
            // 
            this.TXT_creation_date.Location = new System.Drawing.Point(3, 246);
            this.TXT_creation_date.Name = "TXT_creation_date";
            this.TXT_creation_date.ReadOnly = true;
            this.TXT_creation_date.Size = new System.Drawing.Size(258, 23);
            this.TXT_creation_date.TabIndex = 7;
            // 
            // LBL_changing_date
            // 
            this.LBL_changing_date.AutoSize = true;
            this.FLOW_info.SetFlowBreak(this.LBL_changing_date, true);
            this.LBL_changing_date.Location = new System.Drawing.Point(3, 272);
            this.LBL_changing_date.Name = "LBL_changing_date";
            this.LBL_changing_date.Size = new System.Drawing.Size(98, 15);
            this.LBL_changing_date.TabIndex = 8;
            this.LBL_changing_date.Text = "Changing date";
            // 
            // TXT_changing_date
            // 
            this.TXT_changing_date.Location = new System.Drawing.Point(3, 290);
            this.TXT_changing_date.Name = "TXT_changing_date";
            this.TXT_changing_date.ReadOnly = true;
            this.TXT_changing_date.Size = new System.Drawing.Size(258, 23);
            this.TXT_changing_date.TabIndex = 9;
            // 
            // TAB_rights
            // 
            this.TAB_rights.Controls.Add(this.FLOW_rights);
            this.TAB_rights.Location = new System.Drawing.Point(4, 24);
            this.TAB_rights.Name = "TAB_rights";
            this.TAB_rights.Padding = new System.Windows.Forms.Padding(3);
            this.TAB_rights.Size = new System.Drawing.Size(276, 333);
            this.TAB_rights.TabIndex = 1;
            this.TAB_rights.Text = "Rights";
            this.TAB_rights.UseVisualStyleBackColor = true;
            // 
            // FLOW_rights
            // 
            this.FLOW_rights.Controls.Add(this.CHECK_system);
            this.FLOW_rights.Controls.Add(this.CHECK_hidden);
            this.FLOW_rights.Controls.Add(this.LBL_owner);
            this.FLOW_rights.Controls.Add(this.CHECK_o_read);
            this.FLOW_rights.Controls.Add(this.CHECK_o_write);
            this.FLOW_rights.Controls.Add(this.CHECK_o_execute);
            this.FLOW_rights.Controls.Add(this.LBL_owner_group);
            this.FLOW_rights.Controls.Add(this.CHECK_o_g_read);
            this.FLOW_rights.Controls.Add(this.CHECK_o_g_write);
            this.FLOW_rights.Controls.Add(this.CHECK_o_g_execute);
            this.FLOW_rights.Controls.Add(this.LBL_other);
            this.FLOW_rights.Controls.Add(this.CHECK_oth_read);
            this.FLOW_rights.Controls.Add(this.CHECK_oth_write);
            this.FLOW_rights.Controls.Add(this.CHECK_oth_execute);
            this.FLOW_rights.Controls.Add(this.BTN_action);
            this.FLOW_rights.Controls.Add(this.BTN_cancel);
            this.FLOW_rights.Location = new System.Drawing.Point(6, 6);
            this.FLOW_rights.Name = "FLOW_rights";
            this.FLOW_rights.Size = new System.Drawing.Size(264, 321);
            this.FLOW_rights.TabIndex = 0;
            // 
            // CHECK_system
            // 
            this.CHECK_system.AutoSize = true;
            this.CHECK_system.Enabled = false;
            this.FLOW_rights.SetFlowBreak(this.CHECK_system, true);
            this.CHECK_system.Location = new System.Drawing.Point(3, 3);
            this.CHECK_system.Name = "CHECK_system";
            this.CHECK_system.Size = new System.Drawing.Size(68, 19);
            this.CHECK_system.TabIndex = 1;
            this.CHECK_system.Text = "System";
            this.CHECK_system.UseVisualStyleBackColor = true;
            // 
            // CHECK_hidden
            // 
            this.CHECK_hidden.AutoSize = true;
            this.CHECK_hidden.Enabled = false;
            this.FLOW_rights.SetFlowBreak(this.CHECK_hidden, true);
            this.CHECK_hidden.Location = new System.Drawing.Point(3, 28);
            this.CHECK_hidden.Name = "CHECK_hidden";
            this.CHECK_hidden.Size = new System.Drawing.Size(68, 19);
            this.CHECK_hidden.TabIndex = 2;
            this.CHECK_hidden.Text = "Hidden";
            this.CHECK_hidden.UseVisualStyleBackColor = true;
            // 
            // LBL_owner
            // 
            this.LBL_owner.AutoSize = true;
            this.FLOW_rights.SetFlowBreak(this.LBL_owner, true);
            this.LBL_owner.Location = new System.Drawing.Point(111, 70);
            this.LBL_owner.Margin = new System.Windows.Forms.Padding(111, 15, 3, 0);
            this.LBL_owner.Name = "LBL_owner";
            this.LBL_owner.Size = new System.Drawing.Size(42, 15);
            this.LBL_owner.TabIndex = 3;
            this.LBL_owner.Text = "Owner";
            // 
            // CHECK_o_read
            // 
            this.CHECK_o_read.AutoSize = true;
            this.CHECK_o_read.Enabled = false;
            this.CHECK_o_read.Location = new System.Drawing.Point(3, 88);
            this.CHECK_o_read.Name = "CHECK_o_read";
            this.CHECK_o_read.Size = new System.Drawing.Size(54, 19);
            this.CHECK_o_read.TabIndex = 4;
            this.CHECK_o_read.Text = "Read";
            this.CHECK_o_read.UseVisualStyleBackColor = true;
            // 
            // CHECK_o_write
            // 
            this.CHECK_o_write.AutoSize = true;
            this.CHECK_o_write.Enabled = false;
            this.CHECK_o_write.Location = new System.Drawing.Point(90, 88);
            this.CHECK_o_write.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.CHECK_o_write.Name = "CHECK_o_write";
            this.CHECK_o_write.Size = new System.Drawing.Size(61, 19);
            this.CHECK_o_write.TabIndex = 5;
            this.CHECK_o_write.Text = "Write";
            this.CHECK_o_write.UseVisualStyleBackColor = true;
            // 
            // CHECK_o_execute
            // 
            this.CHECK_o_execute.AutoSize = true;
            this.CHECK_o_execute.Enabled = false;
            this.CHECK_o_execute.Location = new System.Drawing.Point(184, 88);
            this.CHECK_o_execute.Name = "CHECK_o_execute";
            this.CHECK_o_execute.Size = new System.Drawing.Size(75, 19);
            this.CHECK_o_execute.TabIndex = 6;
            this.CHECK_o_execute.Text = "Execute";
            this.CHECK_o_execute.UseVisualStyleBackColor = true;
            // 
            // LBL_owner_group
            // 
            this.LBL_owner_group.AutoSize = true;
            this.FLOW_rights.SetFlowBreak(this.LBL_owner_group, true);
            this.LBL_owner_group.Location = new System.Drawing.Point(90, 125);
            this.LBL_owner_group.Margin = new System.Windows.Forms.Padding(90, 15, 3, 0);
            this.LBL_owner_group.Name = "LBL_owner_group";
            this.LBL_owner_group.Size = new System.Drawing.Size(84, 15);
            this.LBL_owner_group.TabIndex = 7;
            this.LBL_owner_group.Text = "Owner group";
            // 
            // CHECK_o_g_read
            // 
            this.CHECK_o_g_read.AutoSize = true;
            this.CHECK_o_g_read.Enabled = false;
            this.CHECK_o_g_read.Location = new System.Drawing.Point(3, 143);
            this.CHECK_o_g_read.Name = "CHECK_o_g_read";
            this.CHECK_o_g_read.Size = new System.Drawing.Size(54, 19);
            this.CHECK_o_g_read.TabIndex = 8;
            this.CHECK_o_g_read.Text = "Read";
            this.CHECK_o_g_read.UseVisualStyleBackColor = true;
            // 
            // CHECK_o_g_write
            // 
            this.CHECK_o_g_write.AutoSize = true;
            this.CHECK_o_g_write.Enabled = false;
            this.CHECK_o_g_write.Location = new System.Drawing.Point(90, 143);
            this.CHECK_o_g_write.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.CHECK_o_g_write.Name = "CHECK_o_g_write";
            this.CHECK_o_g_write.Size = new System.Drawing.Size(61, 19);
            this.CHECK_o_g_write.TabIndex = 9;
            this.CHECK_o_g_write.Text = "Write";
            this.CHECK_o_g_write.UseVisualStyleBackColor = true;
            // 
            // CHECK_o_g_execute
            // 
            this.CHECK_o_g_execute.AutoSize = true;
            this.CHECK_o_g_execute.Enabled = false;
            this.CHECK_o_g_execute.Location = new System.Drawing.Point(184, 143);
            this.CHECK_o_g_execute.Name = "CHECK_o_g_execute";
            this.CHECK_o_g_execute.Size = new System.Drawing.Size(75, 19);
            this.CHECK_o_g_execute.TabIndex = 10;
            this.CHECK_o_g_execute.Text = "Execute";
            this.CHECK_o_g_execute.UseVisualStyleBackColor = true;
            // 
            // LBL_other
            // 
            this.LBL_other.AutoSize = true;
            this.FLOW_rights.SetFlowBreak(this.LBL_other, true);
            this.LBL_other.Location = new System.Drawing.Point(111, 180);
            this.LBL_other.Margin = new System.Windows.Forms.Padding(111, 15, 3, 0);
            this.LBL_other.Name = "LBL_other";
            this.LBL_other.Size = new System.Drawing.Size(42, 15);
            this.LBL_other.TabIndex = 11;
            this.LBL_other.Text = "Other";
            // 
            // CHECK_oth_read
            // 
            this.CHECK_oth_read.AutoSize = true;
            this.CHECK_oth_read.Enabled = false;
            this.CHECK_oth_read.Location = new System.Drawing.Point(3, 198);
            this.CHECK_oth_read.Name = "CHECK_oth_read";
            this.CHECK_oth_read.Size = new System.Drawing.Size(54, 19);
            this.CHECK_oth_read.TabIndex = 12;
            this.CHECK_oth_read.Text = "Read";
            this.CHECK_oth_read.UseVisualStyleBackColor = true;
            // 
            // CHECK_oth_write
            // 
            this.CHECK_oth_write.AutoSize = true;
            this.CHECK_oth_write.Enabled = false;
            this.CHECK_oth_write.Location = new System.Drawing.Point(90, 198);
            this.CHECK_oth_write.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.CHECK_oth_write.Name = "CHECK_oth_write";
            this.CHECK_oth_write.Size = new System.Drawing.Size(61, 19);
            this.CHECK_oth_write.TabIndex = 13;
            this.CHECK_oth_write.Text = "Write";
            this.CHECK_oth_write.UseVisualStyleBackColor = true;
            // 
            // CHECK_oth_execute
            // 
            this.CHECK_oth_execute.AutoSize = true;
            this.CHECK_oth_execute.Enabled = false;
            this.CHECK_oth_execute.Location = new System.Drawing.Point(184, 198);
            this.CHECK_oth_execute.Name = "CHECK_oth_execute";
            this.CHECK_oth_execute.Size = new System.Drawing.Size(75, 19);
            this.CHECK_oth_execute.TabIndex = 14;
            this.CHECK_oth_execute.Text = "Execute";
            this.CHECK_oth_execute.UseVisualStyleBackColor = true;
            // 
            // BTN_action
            // 
            this.BTN_action.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_action.ForeColor = System.Drawing.Color.DarkBlue;
            this.BTN_action.Location = new System.Drawing.Point(3, 285);
            this.BTN_action.Margin = new System.Windows.Forms.Padding(3, 65, 3, 3);
            this.BTN_action.Name = "BTN_action";
            this.BTN_action.Size = new System.Drawing.Size(126, 30);
            this.BTN_action.TabIndex = 15;
            this.BTN_action.Text = "Change";
            this.BTN_action.UseVisualStyleBackColor = true;
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(135, 285);
            this.BTN_cancel.Margin = new System.Windows.Forms.Padding(3, 65, 3, 3);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(126, 30);
            this.BTN_cancel.TabIndex = 16;
            this.BTN_cancel.Text = "Cancel";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            this.BTN_cancel.Visible = false;
            // 
            // FORM_Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.TAB_box);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_Properties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Properties";
            this.TAB_box.ResumeLayout(false);
            this.TAB_info.ResumeLayout(false);
            this.FLOW_info.ResumeLayout(false);
            this.FLOW_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_object)).EndInit();
            this.TAB_rights.ResumeLayout(false);
            this.FLOW_rights.ResumeLayout(false);
            this.FLOW_rights.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TAB_box;
        private System.Windows.Forms.TabPage TAB_info;
        private System.Windows.Forms.TabPage TAB_rights;
        private System.Windows.Forms.FlowLayoutPanel FLOW_info;
        private System.Windows.Forms.Label LBL_name;
        private System.Windows.Forms.TextBox TXT_name;
        private System.Windows.Forms.Label LBL_path;
        private System.Windows.Forms.TextBox TXT_path;
        private System.Windows.Forms.Label LBL_size;
        private System.Windows.Forms.TextBox TXT_size;
        private System.Windows.Forms.Label LBL_creation_date;
        private System.Windows.Forms.TextBox TXT_creation_date;
        private System.Windows.Forms.Label LBL_changing_date;
        private System.Windows.Forms.TextBox TXT_changing_date;
        private System.Windows.Forms.PictureBox IMG_object;
        private System.Windows.Forms.FlowLayoutPanel FLOW_rights;
        private System.Windows.Forms.CheckBox CHECK_system;
        private System.Windows.Forms.CheckBox CHECK_hidden;
        private System.Windows.Forms.Label LBL_owner;
        private System.Windows.Forms.CheckBox CHECK_o_read;
        private System.Windows.Forms.CheckBox CHECK_o_write;
        private System.Windows.Forms.CheckBox CHECK_o_execute;
        private System.Windows.Forms.Label LBL_owner_group;
        private System.Windows.Forms.CheckBox CHECK_o_g_read;
        private System.Windows.Forms.CheckBox CHECK_o_g_write;
        private System.Windows.Forms.CheckBox CHECK_o_g_execute;
        private System.Windows.Forms.Label LBL_other;
        private System.Windows.Forms.CheckBox CHECK_oth_read;
        private System.Windows.Forms.CheckBox CHECK_oth_write;
        private System.Windows.Forms.CheckBox CHECK_oth_execute;
        private System.Windows.Forms.Button BTN_action;
        private System.Windows.Forms.Button BTN_cancel;
    }
}