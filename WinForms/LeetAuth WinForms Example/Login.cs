using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeetAuthWinForms;

namespace LeetAuth_WinForms_Example
{
    public partial class Login : Form
    {
        public Login()
        {


            InitializeComponent();

        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (Program.AuthenticationManager.Login(new LeetAuthWinForms.Models.LoginRequest {Username = usernameBox.Text, Password = passwordBox.Text}))
            {
                Hub hub = new Hub();
                this.Hide();
                hub.Show();
            }
        }

    }
}
