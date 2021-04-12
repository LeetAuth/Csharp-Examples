
namespace LeetAuth_WinForms_Example
{
    partial class Login
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
            this.LoginBtn = new System.Windows.Forms.Button();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(55, 148);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(94, 38);
            this.LoginBtn.TabIndex = 0;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(155, 148);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(94, 38);
            this.RegisterBtn.TabIndex = 1;
            this.RegisterBtn.Text = "Register";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(36, 68);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(230, 20);
            this.usernameBox.TabIndex = 2;
            this.usernameBox.Text = "Username";
            this.usernameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(36, 94);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(230, 20);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.Text = "Password";
            this.passwordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 256);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.LoginBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passwordBox;
    }
}

