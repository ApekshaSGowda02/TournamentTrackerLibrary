using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    public class TeamModel
    {
        //initialize the list property
        // This could be done by adding a constructor code as below 
        /*
        public TeamModel(){
            TeamMembers = new List<Person>        
        }*/
        // But now there is no need to define a constructor to instatiate a property and this is new in core 6
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        public string TeamName { get; set; }
    }
}
