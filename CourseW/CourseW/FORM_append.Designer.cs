namespace CourseW
{
    partial class FORM_Append
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Append));
            this.TXT_file = new System.Windows.Forms.TextBox();
            this.TXT_append = new System.Windows.Forms.TextBox();
            this.BTN_accept = new System.Windows.Forms.Button();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXT_file
            // 
            this.TXT_file.Location = new System.Drawing.Point(12, 12);
            this.TXT_file.Multiline = true;
            this.TXT_file.Name = "TXT_file";
            this.TXT_file.ReadOnly = true;
            this.TXT_file.Size = new System.Drawing.Size(700, 250);
            this.TXT_file.TabIndex = 0;
            // 
            // TXT_append
            // 
            this.TXT_append.Location = new System.Drawing.Point(12, 268);
            this.TXT_append.Multiline = true;
            this.TXT_append.Name = "TXT_append";
            this.TXT_append.Size = new System.Drawing.Size(700, 250);
            this.TXT_append.TabIndex = 1;
            // 
            // BTN_accept
            // 
            this.BTN_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_accept.ForeColor = System.Drawing.Color.DarkBlue;
            this.BTN_accept.Location = new System.Drawing.Point(406, 524);
            this.BTN_accept.Name = "BTN_accept";
            this.BTN_accept.Size = new System.Drawing.Size(150, 23);
            this.BTN_accept.TabIndex = 2;
            this.BTN_accept.Text = "ACCEPT";
            this.BTN_accept.UseVisualStyleBackColor = true;
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(562, 524);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(150, 23);
            this.BTN_cancel.TabIndex = 3;
            this.BTN_cancel.Text = "CANCEL";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            // 
            // FORM_Append
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 559);
            this.Controls.Add(this.BTN_cancel);
            this.Controls.Add(this.BTN_accept);
            this.Controls.Add(this.TXT_append);
            this.Controls.Add(this.TXT_file);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_Append";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Append";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_file;
        private System.Windows.Forms.Button BTN_accept;
        private System.Windows.Forms.Button BTN_cancel;
        public System.Windows.Forms.TextBox TXT_append;
    }
}