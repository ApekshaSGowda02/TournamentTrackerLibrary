using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerLibrary.DataAccess
{
    public class SQLConnector : IDataConnection
    {
        private const string db = "Tournaments";
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                // we can write a db query heer like insert into prize values blah blah but we are not going to do that here, we will instead a SP to keep it secure from SQL ijections

                //dapper method
                var dynmParameter = new DynamicParameters();
                dynmParameter.Add("@FirstName", model.FirstName);
                dynmParameter.Add("@LastName", model.LastName);
                dynmParameter.Add("@EmailAddress", model.Email);
                dynmParameter.Add("@CellPhoneNumber", model.Phone);
                dynmParameter.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", dynmParameter, commandType: CommandType.StoredProcedure);

                model.PersonId = dynmParameter.Get<int>("@id");
                return model;
            }
        }


        //TODO - Make the craete prize method save the data to the db
        /// <summary>
        /// saves a new prize into the db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The info about prize including the unique id</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //throw new NotImplementedException();

            //IDbConnection is a microsoft provided interface.

            // using - it says i want to wrap the ode in () and when it hits the using block's closing curly brace, the connection gets destroyed properly - hence providig memory leaks
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                // we can write a db query heer like insert into prize values blah blah but we are not going to do that here, we will instead a SP to keep it secure from SQL ijections

                //dapper method
                var dynmParameter = new DynamicParameters();
                dynmParameter.Add("@PlaceNumber", model.PlaceNumber);
                dynmParameter.Add("@PlaceName", model.PlaceName);
                dynmParameter.Add("@PrizeAmount", model.PrizeAmount);
                dynmParameter.Add("@PrizePercentage", model.PrizePercentage);
                dynmParameter.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", dynmParameter, commandType:CommandType.StoredProcedure);

                model.Id = dynmParameter.Get<int>("@id");
                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                var dynmParameter = new DynamicParameters();
                dynmParameter.Add("@TeamName", model.TeamName);
                dynmParameter.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", dynmParameter, commandType: CommandType.StoredProcedure);

                model.TeamId = dynmParameter.Get<int>("@id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    dynmParameter = new DynamicParameters();
                    dynmParameter.Add("@TeamId", model.TeamId);
                    dynmParameter.Add("@PersonId", tm.PersonId);

                    connection.Execute("dbo.spTeamMembers_Insert", dynmParameter, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
