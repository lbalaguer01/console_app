using System;
using System.IO;
using System.Data.SQLite;

namespace Collector
{

    class FileCopy
    {
        public FileCopy(string source, string destination)
        {
            Copy(source, destination);
        }

        void Copy(string source, string destination)
        {
            try
            {
                // Create file stream variable
                // Close filestream
                // If not closed, will caught an exception saying file used by another process
                // Copy file to new directory
                var filestream = File.Create(destination);
                filestream.Close();
                File.Copy(source, destination, true);

                Console.WriteLine(source + " copied to " + destination);
                new LogWriter(source + " copied to " + destination);

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                new LogWriter(ex.Message);
            }
        }
    }

    class FileReader
    {
        public FileReader(string sql, string src)
        {
            Reader(sql, src);
        }

        public void Reader(string sql, string src)
        {
            try
            {
                // Connection string               
                SQLiteConnection con = new SQLiteConnection(@"Data Source=" + src);
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Log history
                    new LogWriter(dr[0].ToString() + " | " + dr[1].ToString());
                    // Console.WriteLine(dr[0].ToString() + " | " + dr[1].ToString());                                        
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                new LogWriter(ex.Message);
            }
        }

    }
}
