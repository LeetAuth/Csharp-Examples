using System;
using System.Diagnostics;

namespace LeetAuthConsole
{
    class Program
    {
        // This defines a global scoped variable of our auth class and allows us to access it from everywhere
        public static LeetAuth AuthenticationManager;
        
        static void Main(string[] args)
        {
            // Asking the user if he wants to auto login or not after the JWT expires
           string autoLogin = UserInterface.GetInput("Do you wish for the app to automatically log you in after your session expires? If no, the app will close when it expires. Y/N\n> ");

            // Initializing the LeetAuth class
            AuthenticationManager = new LeetAuth("197238084039-680135171323", autoLogin.ToLower().Contains("y"));
            UserInterface.LoginPrompt();
        }
    }
}      