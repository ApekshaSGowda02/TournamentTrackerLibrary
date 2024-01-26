using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerLibrary.DataAccess
{
    public class SQLConnector : IDataConnection
    {
        //TODO - Make the craete prize method save the data to the db
        /// <summary>
        /// saves a new prize into the db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The info about prize including the id</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //throw new NotImplementedException();
            model.Id = 1;
            return model;
        }
    }
}
