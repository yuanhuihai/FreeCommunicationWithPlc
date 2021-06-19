using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCommunicationWithPlc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
      //  [STAThread]
     
    
        static void Main()
        { 
            bool createNew;
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("应用程序已经在运行中...");
                }
            }
        }
    }
}


