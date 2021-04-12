using System;
using System.IO;
using System.Text;
using System.Threading;
using LeetAuthConsole.Models;

namespace LeetAuthConsole
{
    public class UserInterface
    {
        // This is a simple self implementation of a way to read from the console while also displaying a prompt for the user
        public static string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static Models.LoginRequest GetLoginData()
        {
            return new LeetAuthConsole.Models.LoginRequest
            {
                Username = GetInput("Username: "),
                Password = GetInput("Password: "),
            };
        }
        
        public static void LoginPrompt()
        {
            int loginAttempts = 0;
            bool loggedIn = Program.AuthenticationManager.Login(GetLoginData());
            
            while (!loggedIn && loginAttempts < 10)
            {
                loggedIn = Program.AuthenticationManager.Login(GetLoginData());
                loginAttempts++;
                Console.Clear();
            }
            
            if(loggedIn)
                MainMenu();
            else
                Environment.Exit(1);
                
        }


        public static void MainMenu()
        {
            Console.Clear();
            Console.Write($"Welcome to the LeetAuth console example: {Session.Username}");
            while (true)
            {
                Console.Clear();
                Console.Write("Options:\r\n[1] Get account properties\r\n[2] Change password\r\n[3] Test the fetching of variables\r\n[4] Test the file download\r\n[5] Fetch login logs\r\nOption: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AccountProperties properties = Program.AuthenticationManager.AccountProperties();
                        if (properties == null)
                            break;
                        Console.Clear();
                        Console.Write($"Username: {properties.Username}\r\n" +
                                      $"Plan: {properties.Plan}\r\n" + 
                                      $"Last Login: {properties.LastLogin}\r\n" +
                                      $"Expiry Date: {properties.ExpiryDate}\r\n" +
                                      $"Join Date: {properties.JoinDate}\r\n" 
                        );
                        break;
                    case "2":
                        if (Program.AuthenticationManager.ChangePassword(new ChangePasswordRequest
                        {
                            NewPassword = GetInput("New Password: "),
                            ConfirmNewPassword = GetInput("Confirm Password: ")
                        })) { Console.WriteLine("Password successfully changed."); }
                        break;
                    case "3":
                        ServerVariable variable = Program.AuthenticationManager.GetVariable("VARIABLE_NAME");
                        if (variable.Status)
                            Console.WriteLine("Your variable: " + variable.Variable);
                        break;
                    case "4":
                        string fileName = "myTestFile.txt";
                        // In this example we are storing the file inside the Temp directory
                        string downloadPath = Path.Combine(Path.GetTempPath(), fileName);
                        // This takes the file name (can be seen on the web panel) + the download path
                        if (Program.AuthenticationManager.DownloadFile(fileName, downloadPath))
                            Console.WriteLine("Successfully downloaded the file");
                        break;
                    case "5":
                        LoginLogs logsResponse = Program.AuthenticationManager.LoginLogs();
                        if (!logsResponse.Status)
                            return;
                        LoginLog[] loginLogs = logsResponse.Logs;
                        
                        for (int i = 0; i < loginLogs.Length; i++)
                        {
                            Console.Write(
                                $"[{i}]\r\n Timestamp: {loginLogs[i].Timestamp}\r\n" +
                                $"IP Address: {loginLogs[i].IpAddress}\r\n"
                            );
                        }

                        GetInput("Press Enter to continue");
                        break;
                    default:
                        Console.WriteLine("Invalid input! Choose from the list");
                        break;
                }
                Thread.Sleep(3000);
            }
        }
    }
}