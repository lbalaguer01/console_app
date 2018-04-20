using System;
using System.IO;

namespace Collector
{
    class Program
    {
        static void Main()
        {
                
               Locator l = new Locator();
            // For Opera
            try
            {
                l.OperaLocator();
                l.ChromeLocator();
                l.MozillaLocator(); 
                                            
            }
            catch(Exception ex)
            {
                new LogWriter(ex.Message);
            }

            Console.ReadLine();
        }
      

    }

} 