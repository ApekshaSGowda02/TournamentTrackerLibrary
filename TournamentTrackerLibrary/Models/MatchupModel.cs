using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    public class MatchupModel
    {
        public int MatchupId { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
