using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentTrackerLibrary;

namespace TournamentTrackerUI
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
            // Initialize DB connection
            // First create a reference of TournamentTrackerLibray in TournamentTrackerUI References section

            //TournamentTrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile);
            TournamentTrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);

            //Application.Run(new TournamentDashboardForm());
            Application.Run(new CreateTournamentForm());

        }
    }
}
