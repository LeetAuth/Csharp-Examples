using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeetAuthWinForms;

namespace LeetAuth_WinForms_Example
{
    public partial class Hub : Form
    {
        public Hub()
        {
            InitializeComponent();
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            // Example of a Download function

            string fileName = "hey.txt";  // Your file Name
            string saveToPath = "\\Windows\\" + fileName;  // Path where you want to save it


            if (Program.AuthenticationManager.DownloadFile(fileName, saveToPath))
            {
                MessageBox.Show("Downloaded the requested file!");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var my_variable = Program.AuthenticationManager.GetVariable("testVariable"); // Variable Name
            variableLabel.Text = $"Variable: {my_variable.Variable}";
        }

        private void ChangePwBtn_Click(object sender, EventArgs e)
        {
            if (Program.AuthenticationManager.ChangePassword(new LeetAuthWinForms.Models.ChangePasswordRequest { NewPassword = newPwBox.Text, ConfirmNewPassword = newPwBox2.Text }))
            {
                MessageBox.Show("You have changed your password!");
            }
        }
    }
}
