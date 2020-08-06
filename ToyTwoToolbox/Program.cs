using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe")) {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new
                System.Threading.ThreadExceptionEventHandler(SessionManager.ReportException);

                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                //AppDomain.CurrentDomain.UnhandledException += new
                //UnhandledExceptionEventHandler(SessionManager.ReportException);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SessionManager());
        }
    }
}
