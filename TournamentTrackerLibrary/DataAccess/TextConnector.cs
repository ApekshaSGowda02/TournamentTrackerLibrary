using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;
// adding the anmespace of the logic written in the static file  called textconnectorprocessor
using TournamentTrackerLibrary.DataAccess.TextHelpers;

namespace TournamentTrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        //TODO - create a logic to add data to the text file.
        
        private const string PrizesFile = "PrizeModels.csv"; // private variables are always camel case but a private constant class is alwasy pascal case
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.PersonId).First().PersonId + 1;
            }
            model.PersonId = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            //throw new NotImplementedException();

            // implementing helper methods that will allow us to talk to the text files

            //Step 1: Load the text file
            //Step 2: Convert the text to a List<Prize Model>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //Step 3: Find the max ID
            // finds the id if the highest id in the list and adds one to it
            int currentId = 1;
            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            //Step 4: Add the new record with the new ID ie, Max+1S
            prizes.Add(model);

            //Step 6: Save the List<String> to the text File
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.TeamId).First().TeamId + 1;
            }
            model.TeamId = currentId;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
    }
}
