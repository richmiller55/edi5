using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace OrderEDI
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
            runDir();
            runLCDir();
        }
        static void runLCDir()
        {
            string dir = "D:/users/rich/data/lc/ToLoad/";
            DirectoryInfo mainDir = new DirectoryInfo(dir);
            try
            {
                FileSystemInfo[] ediOrders = mainDir.GetFileSystemInfos();
                foreach (FileSystemInfo ediOrder in ediOrders)
                {
                    string fileName = ediOrder.Name;
                    XmlReader reader = new XmlReader(dir , fileName);
                    reader.runIt();
                    ShipToOrder ord = reader.getOrder();
                    WriteShipToOrder writer = new WriteShipToOrder();
                    writer.ProcessOrder(ord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void runDir()
        {
            string dir = "D:/users/rich/data/wm/ToLoad/";
            DirectoryInfo mainDir = new DirectoryInfo(dir);
            try
            {
                FileSystemInfo[] ediOrders = mainDir.GetFileSystemInfos();
                foreach (FileSystemInfo ediOrder in ediOrders)
                {
                    string fileName = ediOrder.Name;
                    XmlReader reader = new XmlReader(dir,fileName);
                    reader.runIt();
                    Order ord = reader.getOrder();
                    WriteSalesOrder writer = new WriteSalesOrder();
                    writer.ProcessOrder(ord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

    }
}