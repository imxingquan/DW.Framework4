using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DW.Framework.Utils
{
   
    
    public class Debug
    {
        [Conditional("DEBUG")]
        public static void Trace(object obj)
        {
            lock (obj)
            {
                try
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText("debug.txt");

                    sw.WriteLine(DateTime.Now.ToString() + "\n\r");

                    sw.WriteLine(obj.ToString());

                    sw.WriteLine("\r\n");

                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Idcwest.Uitls.Debug:"+ex.Message);
                }
                finally { }
            }
        }

        [Conditional("DEBUG")]
        public static void Trace(Exception e)
        {
           
                try
                {
                    System.IO.StreamWriter sw = System.IO.File.AppendText("debug.txt");

                    sw.WriteLine(DateTime.Now.ToString() + "\n\r");

                    sw.WriteLine(e.Message);
                    sw.WriteLine("\r\n");
                    sw.WriteLine(e.StackTrace);
                    sw.WriteLine("\r\n");

                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Idcwest.Uitls.Debug:" + ex.Message);
                }
                finally { }
            
        }

        
    }
}
