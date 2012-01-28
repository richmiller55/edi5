using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class AppVarsMgr
    {
        string user = "";
        string password = "";
        string watchDirectory = "";
        string database = "test";
        string dataPort;
        string deferDirectory;
        string processedDirectory;

        string file = "appVars.xml";
        public AppVarsMgr()
        {
            ParseFile();
        }
        protected void ParseFile()
        {
            XmlTextReader reader = new XmlTextReader(file);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            string element = "";
            string readValue = "";
            while (reader.Read())
            {
                bool ready = false;
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        element = reader.Name;
                        ready = false;
                        break;
                    case XmlNodeType.Text:
                        readValue = reader.Value;
                        ready = true;
                        break;
                }
                if (ready)
                {
                    switch (element)
                    {
                        case "user":
                            User = readValue;
                            break;
                        case "password":
                            Password = readValue;
                            break;
                        case "watchDirectory":
                            WatchDirectory = readValue;
                            break;
                        case "database":
                            break;
                        case "dataPort":
                            DataPort = readValue;
                            break;
                        case "processedDirectory":
                            ProcessedDirectory = readValue;
                            break;
                        case "deferDirectory":
                            DeferDirectory = readValue;
                            break;
                    }
                }
            }
        }
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public string WatchDirectory
        {
            get
            {
                return watchDirectory;
            }
            set
            {
                watchDirectory = value;
            }
        }
        public string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
            }
        }
        public string DataPort
        {
            get
            {
                return dataPort;
            }
            set
            {
                dataPort = value;
            }
        }
        public string DeferDirectory
        {
            get
            {
              return deferDirectory;
            }
            set
            {
                deferDirectory = value;
            }
        }
        public string ProcessedDirectory
        {
            get
            {
              return processedDirectory;
            }
            set
            {
              processedDirectory = value;
            }
        }
    }
}


