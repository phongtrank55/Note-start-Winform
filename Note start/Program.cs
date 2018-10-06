using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Note_start
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmNoteStart());
        }
    }
}
