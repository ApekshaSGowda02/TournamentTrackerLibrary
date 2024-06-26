﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.DataAccess;

namespace TournamentTrackerLibrary
{
    public static class GlobalConfig
    {
        //private set - only methods inside of this global config class can change the value of connections 
        //but everyone can read the value of connections

        // I created this List of IDataCoonections so that we can have more than one data sources (in our case
        // being Text file and SQL logs)


        // the List<IDataConnections> need to be initialised before use and it can be done here or inside the method 
        // where the "Connections" is used like - Connections = new List<IDataConnection>
        //public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        //now list is not required as we have a seperate classes for sql and text and we will use an enum isntead
        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections (DatabaseType db)
        {
            // we can use this instead of the if else as well
            //switch (db)
            //{
            //    case DatabaseType.Sql:
            //        break;
            //    case DatabaseType.TextFile:
            //        break;
            //    default:
            //        break;
            //}
            if (db == DatabaseType.Sql)
            {
                // TODO - Create the SQL Connection (done)
                // TODO - Set up the SQL connector properly
                SQLConnector sql = new SQLConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                //TODO - Create the text Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        /// <summary>
        /// Goes to app.config and gets the connection string, by looking up the name parameter
        /// </summary>
        /// <param name="name"> Tournaments</param>
        /// <returns> the connection string </returns>
        public static string ConnString (string name)
        {
            //The below line initially threw an error because we did not have a ref for ConfigurationManager 
            // for that go to reference > add reference > assemblies > search for config and click System Configuration
            // once reference is assed go ahead and use it in using 
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
