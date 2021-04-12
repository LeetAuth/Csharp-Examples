using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeetAuth_WinForms_Example
{
    static class Program
    {
        public static LeetAuthWinForms.LeetAuth AuthenticationManager;

        [STAThread]
        static void Main()
        {
            // Initializing the LeetAuth class 
            AuthenticationManager = new LeetAuthWinForms.LeetAuth("197238084039-680135171323", true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
