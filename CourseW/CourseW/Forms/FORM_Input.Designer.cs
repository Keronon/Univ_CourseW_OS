namespace CourseW
{
    partial class FORM_Input
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Input));
            this.TXT_input = new System.Windows.Forms.TextBox();
            this.BTN_accept = new System.Windows.Forms.Button();
            this.BTN_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXT_input
            // 
            this.TXT_input.Location = new System.Drawing.Point(12, 12);
            this.TXT_input.MaxLength = 30;
            this.TXT_input.Name = "TXT_input";
            this.TXT_input.Size = new System.Drawing.Size(300, 23);
            this.TXT_input.TabIndex = 0;
            // 
            // BTN_accept
            // 
            this.BTN_accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTN_accept.ForeColor = System.Drawing.Color.DarkBlue;
            this.BTN_accept.Location = new System.Drawing.Point(12, 41);
            this.BTN_accept.Name = "BTN_accept";
            this.BTN_accept.Size = new System.Drawing.Size(147, 23);
            this.BTN_accept.TabIndex = 1;
            this.BTN_accept.Text = "ACCEPT";
            this.BTN_accept.UseVisualStyleBackColor = true;
            // 
            // BTN_cancel
            // 
            this.BTN_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_cancel.ForeColor = System.Drawing.Color.DarkRed;
            this.BTN_cancel.Location = new System.Drawing.Point(165, 41);
            this.BTN_cancel.Name = "BTN_cancel";
            this.BTN_cancel.Size = new System.Drawing.Size(147, 23);
            this.BTN_cancel.TabIndex = 2;
            this.BTN_cancel.Text = "CANCEL";
            this.BTN_cancel.UseVisualStyleBackColor = true;
            // 
            // FORM_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 76);
            this.Controls.Add(this.BTN_cancel);
            this.Controls.Add(this.BTN_accept);
            this.Controls.Add(this.TXT_input);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_Input";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTN_accept;
        private System.Windows.Forms.Button BTN_cancel;
        public System.Windows.Forms.TextBox TXT_input;
    }
}