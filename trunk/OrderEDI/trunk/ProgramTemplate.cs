using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace OrderLoader
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
            XmlReader reader = new XmlReader("po5905092170.xml");
            reader.runIt();
            Order ord = reader.getOrder();
            WriteSalesOrder writer = new WriteSalesOrder(ord);
        }
    }
}