using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentTrackerLibrary;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequestor, ITeamRequestor
    {
        // stores all the teams in the db
        // we can load the teams frm the db or the text file.
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        //wireup the data from available teams to the drop down
        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);
            }
            WireUpLists();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the craete prize form
            CreatePrizeForm frm = new CreatePrizeForm(this); // this keyword represents this particular instance
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            // get back from the form a prize model
            // take the prizemodel and put it into out list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void removeSelectedPlayersButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
                WireUpLists();
            }
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p= (PrizeModel)prizesListBox.SelectedItem;
            if (p != null)
            {
                selectedPrizes.Remove(p);
                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //ValidateData
            decimal fee = 0;

            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("you need to enter a valid entry fee.", "Invalid Fee",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // step 0: Create our tournament model

            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            //foreach(PrizeModel prize in selectedPrizes)
            //{
            //    tm.Prizes.Add(prize);
            //}
            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // step 4: WireUp our matchups 
            // Order our list randomly of teams
            // take that list and check if it's big enough - if not add in the byes 

            // step 1: Create tournament entry
            // step 2: Create all of the prizes entries 
            // step 3: Create all of teh team entries 

            GlobalConfig.Connection.CreateTournament(tm);
        }
    }
}
