using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace FastLoad
{
    public class AppVarsMgr
    {
        string m_user = "";
        string m_password = "";
        string m_watchDirectory = "";
        string m_database = "test";
        string m_dataPort;
        string m_regularBatch = "";
        string m_buyGroupBatch = "";

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
                        case "buyGroupBatch":
                            BuyGroupBatch = readValue;
                            break;
                        case "regularBatch":
                            RegularBatch = readValue;
                            break;
                    }
                }
            }
        }
        public string User
        {
            get
            {
                return m_user;
            }
            set
            {
                m_user = value;
            }
        }
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }
        public string WatchDirectory
        {
            get
            {
                return m_watchDirectory;
            }
            set
            {
                m_watchDirectory = value;
            }
        }
        public string Database
        {
            get
            {
                return m_database;
            }
            set
            {
                m_database = value;
            }
        }
        public string DataPort
        {
            get
            {
                return m_dataPort;
            }
            set
            {
                m_dataPort = value;
            }
        }
        public string RegularBatch
        {
            get
            {
                return m_regularBatch;
            }
            set
            {
                m_regularBatch = value;
            }
        }
        public string BuyGroupBatch
        {
            get
            {
                return m_buyGroupBatch;
            }
            set
            {
                m_buyGroupBatch = value;
            }
        }
    }
}


