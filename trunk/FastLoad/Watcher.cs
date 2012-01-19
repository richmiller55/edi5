using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FastLoad
{
    class Watcher
    {
        FileSystemWatcher watcher;
        Epicor.Mfg.Core.Session session;
        AppVarsMgr appVars;
        ExReport report = new ExReport();
        public Watcher()
        {
            this.appVars = new AppVarsMgr();
            string dir = appVars.WatchDirectory;
            string user = appVars.User;
            string pass = appVars.Password;
            string dataPort = appVars.DataPort;
            string connectionStr = @"AppServerDC://VantageDB1:" + dataPort;
            try
            {
                session = new Epicor.Mfg.Core.Session(user, pass, connectionStr,
                    Epicor.Mfg.Core.Session.LicenseType.Default);
            }
            catch (Exception e)
            {
                string message = e.Message;
                MessageBox.Show("Login failed - Check xml and retry " + message );
                this.report.AddMessage("loginFailed", message + " user " + appVars.User);
                this.report.UpdatePage();
                Application.Exit();
            }
            watcher = new FileSystemWatcher(dir, "*.*");
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            // watcher.Created += OnError;
            watcher.EnableRaisingEvents = true;
            while (Console.Read() != 'q') ;
        }
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            /*
              watcher.EnableRaisingEvents = false;
              if (e.ChangeType == WatcherChangeTypes.Created)
              {
                 UPSReader reader = new UPSReader(this.session);
              }
              watcher.EnableRaisingEvents = true;
            */
        }
        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                UPSReader reader = new UPSReader(this.session,this.report);
                this.report.UpdatePage();
            }
        }
        private static void OnError(object source, ErrorEventArgs e)
        {
            Console.WriteLine("error message");
            Console.WriteLine(e.GetException());
        }
    }
}
