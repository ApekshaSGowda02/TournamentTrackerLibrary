using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerUI
{
    public interface ITeamRequestor
    {
        void TeamComplete(TeamModel model);
    }
}
