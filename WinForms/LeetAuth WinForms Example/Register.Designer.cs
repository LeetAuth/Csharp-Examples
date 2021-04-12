
namespace LeetAuth_WinForms_Example
{
    partial class Register
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
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.licenseBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(37, 33);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(147, 20);
            this.usernameBox.TabIndex = 0;
            this.usernameBox.Text = "Username";
            this.usernameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // licenseBox
            // 
            this.licenseBox.Location = new System.Drawing.Point(37, 113);
            this.licenseBox.Name = "licenseBox";
            this.licenseBox.Size = new System.Drawing.Size(147, 20);
            this.licenseBox.TabIndex = 1;
            this.licenseBox.Text = "License";
            this.licenseBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(37, 74);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(147, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = "Password";
            this.passwordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(68, 161);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(84, 23);
            this.RegisterBtn.TabIndex = 3;
            this.RegisterBtn.Text = "Register";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 221);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.licenseBox);
            this.Controls.Add(this.usernameBox);
            this.Name = "Register";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox licenseBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Button RegisterBtn;
    }
}