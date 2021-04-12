using System;
using System.IO;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using LeetAuthConsole.Models;
using Newtonsoft.Json;

namespace LeetAuthConsole.Models
{
    // This class will be inherited by all data that requires url encoding
    public class UrlTools
    {
        public string Encode(string payload)
        {
            return HttpUtility.UrlEncode(payload);
        }
    }

    public class DefaultResponseData
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
    
    public class LoginRequest: UrlTools
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HardwareIdentifier
        {
            get { return Session.HardwareIdentifier; }
        }
        public string ApplicationChecksum
        {
            get { return Session.ApplicationChecksum;  }
        }

        public string ToQueryParams()
        {
            // Converting our data to valid query parameters
            return $"&username={Encode(Username)}&password={Encode(Password)}&hwid={Encode(HardwareIdentifier)}&checksum={Encode(ApplicationChecksum)}";
        }
    }

    public class LoginResponse: DefaultResponseData
    {
        [JsonProperty("plan")]
        public string Plan { get; set; }
        [JsonProperty("token")]
        public string JsonWebToken { get; set; }
        [JsonProperty("token_expiry")]
        public Int64 TokenExpiry { get; set; }
    }

    public class ChangePasswordRequest: UrlTools
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        
        public string ToQueryParams()
        {
            // Converting our data to valid query parameters
            return $"&new_password={Encode(NewPassword)}&cnf_password={Encode(ConfirmNewPassword)}";
        }
    }

    public class ChangePasswordResponse: DefaultResponseData { }

    public class RegisterRequest: UrlTools
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string License { get; set; }
        public string HardwareIdentifier
        {
            get { return Session.HardwareIdentifier; }
        }
        public string ApplicationChecksum
        {
            get { return Session.ApplicationChecksum; } 
        }

        public string ToQueryParams()
        {
            // Converting our data to valid query parameters
            return $"&username={Encode(Username)}&password={Encode(Password)}&license={Encode(License)}&hwid={Encode(HardwareIdentifier)}&checksum={Encode(ApplicationChecksum)}";
        }
    }

    public class RegisterResponse: DefaultResponseData { }

    public class AccountProperties: DefaultResponseData
    {
        [JsonProperty("app")]
        public string App { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("plan")]
        public string Plan { get; set; }
        [JsonProperty("lastLogin")]
        public string LastLogin { get; set; }
        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }
        [JsonProperty("joinDate")]
        public string JoinDate { get; set; }
    }

    public class LoginLog
    {
        [JsonProperty("appID")]
        public string AppIdentifier { get; set; }
        [JsonProperty("userID")]
        public string UserIdentifier { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("IpAddress")]
        public string IpAddress { get; set; }
        [JsonProperty("unixTimestamp")]
        public Int64 UnixTimestamp { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }
    
    public class LoginLogs: DefaultResponseData
    {
        [JsonProperty("logs")]
        public LoginLog[] Logs { get; set; }
    }

    public class ServerVariable: DefaultResponseData
    {
        [JsonProperty("variable")]
        public string Variable { get; set; }
    }
    
    public class DownloadFile: DefaultResponseData { }
}


namespace LeetAuthConsole
{
    public class Utils
    {
        public static string GetHardwareIdentifier()
        {
            if (!OperatingSystem.IsWindows())
                // Here you can add Linux support for getting the HWID, for now we will just return nothing
                return "invalid_hwid";
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();

            if (mbsList.Count == 0)
                return "invalid_hwid";

            foreach (ManagementBaseObject managementObject in mbsList)
            {
                // Checking if the current object may be null, then proceeding to the next otherwise hashing it and returning it
                string hardwareIdentifier = managementObject["ProcessorId"].ToString();
                if (hardwareIdentifier == null)
                    continue;

                using (SHA512 securityHasher = SHA512.Create())
                {
                    byte[] hwidBytes = Encoding.UTF8.GetBytes(hardwareIdentifier);
                    // Hashing the hardware id for privacy reasons and in order to make it harder for people to attempt to spoof it
                    byte[] hash = securityHasher.ComputeHash(hwidBytes);
                    return BitConverter
                        .ToString(hash)
                        .Replace("-", String.Empty);
                }
            }
            Console.WriteLine("Invlaid HWID 2"); Thread.Sleep(3000);
            return "invalid_hwid";
        }
        public static string GenerateChecksum(string algorithm)
        {
            string checksum = "";
            byte[] fileData;
            // Here we read the current file
            fileData = File.ReadAllBytes(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            switch (algorithm)
            {
                case "SHA-512":
                    using (SHA512 checksumGenerator = SHA512.Create())
                    {
                        checksum = BitConverter
                            .ToString(checksumGenerator.ComputeHash(fileData))
                            .Replace("-", String.Empty);
                    }
                    break;
                case "SHA-256":
                    using (SHA256 checksumGenerator = SHA256.Create())
                    {
                        checksum = BitConverter
                            .ToString(checksumGenerator.ComputeHash(fileData))
                            .Replace("-", String.Empty);
                    }
                    break;
            }

            return checksum;
        }
        
        public string Prompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        
    }

    internal static class Session
    {
        public static string ApplicationChecksum { get; set; }
        public static string HardwareIdentifier { get; set; }
        public static bool LoggedIn { get; set; }
        public static Int64 TokenExpiry { get; set; }
        public static string AuthorizationToken { get; set; }
        public static string Username { get;set; }
        public static string Password { get;set; }
    }
    
    ///<summary>
    ///This is the class used to communicate with the LeetAuth API
    ///</summary>
    ///<param name="appId">the unique application identifier found on the website</param>
    ///<param name="automaticallyRenewJwt">This specifies if the JWT should be automatically renewed when it expires. WARNING: if true, user login data will be stored in memory</param>
    public class LeetAuth: Utils
    {
        private const string BaseUrl = "https://api.leet-auth.dev/public";
        
        // Pre-defining variables that we will assign in the constructor
        private readonly string _applicationIdentifier;
        private readonly bool _automaticallyRenewJwt;
        private HttpClient _apiClient;
        
        public LeetAuth(string appId, bool automaticallyRenewJwt)
        {
            Task.Run(() =>
            {
                Session.HardwareIdentifier = Utils.GetHardwareIdentifier();
                Session.ApplicationChecksum = Utils.GenerateChecksum("SHA-512");
            });
            
            _applicationIdentifier = appId;
            // We only need one instance of HttpClient, creating one/request is a common mistake that slows down your app
            _apiClient = CreateClient(false);
            _automaticallyRenewJwt = automaticallyRenewJwt;
        }
        /// <summary>
        /// Used by us to create an instance of a HttpClient. We use this so that we can customize some settings inside it
        /// </summary>
        private HttpClient CreateClient(bool includeAuthorizationToken)
        {
            HttpClientHandler handler = new HttpClientHandler();
            // Setting up a secure client for our application to prevent
            // some basic man in the middle attacks
            handler.Proxy = null;
            handler.SslProtocols = SslProtocols.Tls12;
            handler.MaxConnectionsPerServer = 64;
            // This is for us to use the CDN properly in order to download files
            handler.AllowAutoRedirect = true;

            HttpClient client = new HttpClient(handler);
            // Checking if we should include the JWT into our Headers
            if (includeAuthorizationToken)
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", Session.AuthorizationToken);
            
            return client;
        }
        /// <summary>
        /// Used by us inside this class to generate a URL together with its app ID and maybe custom data if needed
        /// </summary>
        private string Url(string path, string requestData)
        {
            return $"{BaseUrl}/{path}?app={_applicationIdentifier}{requestData}";
        }
        /// <summary>
        /// A simple function for writing errors we encounter in the API, to stdout
        /// </summary>
        private void InvalidResponse(string action)
        {
            Console.Error.WriteLine($"The API has sent back an invalid response while: {action}");
        }
        
        /// <summary>
        /// This function sends a POST request to the /authenticate endpoint in order to
        /// check a user's credentials
        /// </summary>
        /// <param name="payload">This payload is defined by using LeetAuth.Models.LoginRequest
        /// to set the Username and Password fields</param>
        public bool Login(LoginRequest payload)
        {
            string responseBody = _apiClient.PostAsync(
                Url("authenticate", payload.ToQueryParams()),
                null).Result.Content.ReadAsStringAsync().Result;
            LoginResponse response = 
                JsonConvert.DeserializeObject<LoginResponse>(responseBody);
            if (response == null)
            {
                InvalidResponse("logging in");
                return false;
            }

            switch (response.Status)
            {
                case true:
                    Task.Run(() =>
                    {
                        // If auto-login was true, we will be storing the login data into memory for it to be re-used in the background
                        if (_automaticallyRenewJwt)
                        {
                            Session.Username = payload.Username;
                            Session.Password = payload.Password;
                            Session.AuthorizationToken = response.JsonWebToken;
                            Session.TokenExpiry = response.TokenExpiry;
                            Session.LoggedIn = true;
                        }
                        // Re-creating the client in order to include the JWT in its requests
                        _apiClient = CreateClient(true);
                        // Starting a thread that notifies the user when a logout has occurred
                        int sleepTime = Convert.ToInt32(response.TokenExpiry - DateTimeOffset.Now.ToUnixTimeSeconds());
                        // Multiplying this by 1000 to get the amount of milliseconds we have to sleep for :D
                        Thread.Sleep(sleepTime * 1000);
                        Session.LoggedIn = false;
                        if (_automaticallyRenewJwt)
                        {
                            Login(new LoginRequest
                            {
                                Username = Session.Username,
                                Password = Session.Password,
                            });
                        }
                        else
                        {
                            Console.Error.WriteLine("Your session has expired, this session has been set to not automatically log you in again. Closing now.");
                            Prompt("Press any key to exit");
                        }
                    });
                    break;
                case false:
                    Session.LoggedIn = false;
                    Console.Error.WriteLine($"Error logging in: {response.Error}");
                    break;
            }
            return response.Status;
        }
        /// <summary>
        /// This function sends a POST request to the /register endpoint in order to
        /// register the user
        /// </summary>
        /// <param name="payload">This payload is defined by using LeetAuth.Models.RegisterRequest
        /// to set the Username, Password & License key fields</param>
        public bool Register(RegisterRequest payload)
        {
            string responseBody = _apiClient.PostAsync(
                Url("register", payload.ToQueryParams()),
                null
                ).Result.Content.ReadAsStringAsync().Result;
            RegisterResponse response = 
                JsonConvert.DeserializeObject<RegisterResponse>(responseBody);
            if (response == null)
            {
                InvalidResponse("Registering");
                return false;
            }
            
            switch (response.Status)
            {
                case true:
                    Console.WriteLine("Successfully registered. Welcome!");
                    return response.Status;
                default:
                    Console.WriteLine("There has been an issue registering.\nError: " + response.Error);
                    return response.Status;
            }
        }
        
        /// <summary>
        /// This function sends a POST request to the /change_password endpoint in order
        /// to change a users password
        /// </summary>
        /// <param name="payload">This payload is defined by using LeetAuth.Models.ChangePasswordRequest
        /// to set the NewPassword and ConfirmNewPassword fields</param>
        public bool ChangePassword(ChangePasswordRequest payload)
        {
            string responseBody = _apiClient.PostAsync(
                Url("change_password", payload.ToQueryParams()),
                null
            ).Result.Content.ReadAsStringAsync().Result;

            ChangePasswordResponse response = 
                JsonConvert.DeserializeObject<ChangePasswordResponse>(responseBody);

            if (response == null)
            {
                InvalidResponse("Changing password");
                return false;
            }
            
            if (!response.Status)
                Console.WriteLine("There has been an issue changing your password.\nError: " + response.Error);

            return response.Status;
        }
        
        /// <summary>
        /// This function sends a GET request to the /properties endpoint in order to
        /// get the user's account properties like expiry, plan, etc...
        /// </summary>
        public Models.AccountProperties AccountProperties()
        {
            string responseBody = _apiClient.GetAsync(
                Url("properties", "")).Result.Content.ReadAsStringAsync().Result;

            Models.AccountProperties response = 
                JsonConvert.DeserializeObject<Models.AccountProperties>(responseBody);
            
            if (response == null)
            {
                InvalidResponse("Getting the account properties");
                return null;
            }
            
            if (!response.Status)
                Console.WriteLine("There has been an issue fetching the properties.\nError: " + response.Error);
            
            return response;
        }
        
        /// <summary>
        /// This function sends a GET request to the /logins endpoint in order to get
        /// the user's login logs, users can check for breaches this way
        /// </summary>
        public LoginLogs LoginLogs()
        {
            string responseBody = _apiClient.GetAsync(
                Url("logins", "")).Result.Content.ReadAsStringAsync().Result;
            
            LoginLogs response = 
                JsonConvert.DeserializeObject<LoginLogs>(responseBody);
            
            if (response == null)
            {
                InvalidResponse("Getting the login logs");
                return null;
            } 
            
            if (!response.Status)
               Console.WriteLine("There has been an issue fetching the logs.\nError: " + response.Error);
            return response;
        }
        
        /// <summary>
        /// This function sends a GET request to the /file/:fileName in order to download
        /// a file, if successful it will get a 301 redirect code and will be forwarded to
        /// cdn.leet-auth.dev to download the requested file
        /// </summary>
        /// <param name="fileName">Specifies the name of the file we need</param>
        /// <param name="saveToPath">Specifies the path where to save the file at</param>
        public bool DownloadFile(string fileName, string saveToPath)
        {
            HttpContent content = _apiClient.GetAsync(
                Url($"/files/{fileName}", "")).Result.Content;
            try
            {
                Models.DownloadFile response =
                    JsonConvert.DeserializeObject<DownloadFile>(content.ReadAsStringAsync().Result);
                if (response != null)
                {
                    if (!response.Status)
                    {
                        Console.WriteLine("Something went wrong downloading your file: " + response.Error);
                        return false;
                    }

                }
            }
            // When this catch triggers, it most likely means that we got our file 
            // we have to ignore this, because it is trying to parse a raw file, as a JSON object
            catch{}

            // Making sure the file path is a new file
            File.Delete(saveToPath);

            using FileStream fs = File.Create(saveToPath);
            // Reading & Writing the data from the CDN to the file
            byte[] data = content.ReadAsByteArrayAsync().Result;
            fs.Write(data, 0, data.Length);
            fs.Close();
            
            return true;
        }
        
        /// <summary>
        /// This function will simply return a string containing the server-stored variable 
        /// </summary>
        /// <param name="variableName"></param>
        public ServerVariable GetVariable(string variableName)
        {
            string responseBody = _apiClient.GetAsync(
                Url($"/variables/{variableName}", "")).Result.Content.ReadAsStringAsync().Result;

            ServerVariable response = 
                JsonConvert.DeserializeObject<ServerVariable>(responseBody);
            
            if (response == null)
            {
                InvalidResponse("Getting a server variable");
                return null;
            } 
            
            if (!response.Status)
                Console.WriteLine("There has been an issue fetching the variable.\nError: " + response.Error);
            return response;
        }
        
    }
}