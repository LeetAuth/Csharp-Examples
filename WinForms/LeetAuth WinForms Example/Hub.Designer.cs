
namespace LeetAuth_WinForms_Example
{
    partial class Hub
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
            this.DownloadBtn = new System.Windows.Forms.Button();
            this.variableLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ChangePwBtn = new System.Windows.Forms.Button();
            this.newPwBox = new System.Windows.Forms.TextBox();
            this.newPwBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DownloadBtn
            // 
            this.DownloadBtn.Location = new System.Drawing.Point(12, 12);
            this.DownloadBtn.Name = "DownloadBtn";
            this.DownloadBtn.Size = new System.Drawing.Size(104, 30);
            this.DownloadBtn.TabIndex = 0;
            this.DownloadBtn.Text = "Download";
            this.DownloadBtn.UseVisualStyleBackColor = true;
            this.DownloadBtn.Click += new System.EventHandler(this.DownloadBtn_Click);
            // 
            // variableLabel
            // 
            this.variableLabel.AutoSize = true;
            this.variableLabel.Location = new System.Drawing.Point(12, 113);
            this.variableLabel.Name = "variableLabel";
            this.variableLabel.Size = new System.Drawing.Size(51, 13);
            this.variableLabel.TabIndex = 1;
            this.variableLabel.Text = "Variable: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Fetch Variable";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangePwBtn
            // 
            this.ChangePwBtn.Location = new System.Drawing.Point(481, 73);
            this.ChangePwBtn.Name = "ChangePwBtn";
            this.ChangePwBtn.Size = new System.Drawing.Size(114, 23);
            this.ChangePwBtn.TabIndex = 3;
            this.ChangePwBtn.Text = "Change Password";
            this.ChangePwBtn.UseVisualStyleBackColor = true;
            this.ChangePwBtn.Click += new System.EventHandler(this.ChangePwBtn_Click);
            // 
            // newPwBox
            // 
            this.newPwBox.Location = new System.Drawing.Point(463, 21);
            this.newPwBox.Name = "newPwBox";
            this.newPwBox.Size = new System.Drawing.Size(149, 20);
            this.newPwBox.TabIndex = 4;
            this.newPwBox.Text = "New Password";
            this.newPwBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // newPwBox2
            // 
            this.newPwBox2.Location = new System.Drawing.Point(463, 47);
            this.newPwBox2.Name = "newPwBox2";
            this.newPwBox2.Size = new System.Drawing.Size(149, 20);
            this.newPwBox2.TabIndex = 5;
            this.newPwBox2.Text = "Confirm New Password";
            this.newPwBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Hub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 269);
            this.Controls.Add(this.newPwBox2);
            this.Controls.Add(this.newPwBox);
            this.Controls.Add(this.ChangePwBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.variableLabel);
            this.Controls.Add(this.DownloadBtn);
            this.Name = "Hub";
            this.Text = "Hub";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadBtn;
        private System.Windows.Forms.Label variableLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ChangePwBtn;
        private System.Windows.Forms.TextBox newPwBox;
        private System.Windows.Forms.TextBox newPwBox2;
    }
}