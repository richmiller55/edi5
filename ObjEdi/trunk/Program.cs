using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ObjEdi
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
            Application.Run(new Form1());
            // PearlBlanketReader reader = new PearlBlanketReader();
            // SteinmartDataReader reader = new SteinmartDataReader();
            AcademyReader reader = new  AcademyReader();
            // NviDataReader reader = new NviDataReader();
        }
    }
}