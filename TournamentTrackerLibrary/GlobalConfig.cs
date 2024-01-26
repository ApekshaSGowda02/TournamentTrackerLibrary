using System;
using System.Collections.Generic;
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
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static void InitializeConnections (bool database, bool textFiles)
        {
            if (database)
            {
                // TODO - Create the SQL Connection (done)
                // TODO - Set up the SQL connector properly
                SQLConnector sql = new SQLConnector();
                Connections.Add(sql);
            }

            if (textFiles)
            {
                //TODO - Create the text Connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }
        }

    }
}
