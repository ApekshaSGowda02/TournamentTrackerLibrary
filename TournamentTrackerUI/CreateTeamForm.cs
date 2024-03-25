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
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequestor callingForm;
        public CreateTeamForm(ITeamRequestor caller)
        {
            InitializeComponent();
            callingForm = caller;
            //CreateSampleData();
            WireUpLists();
        }

        // its a method to add sample data to test out data 
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Appu", LastName = "gg" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Abby", LastName = "jhon" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Shiv", LastName = "Tandav" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Bramha", LastName = "Astra" });
        }

        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName"; // Full name here is rhe property that we defined that has only get method that returns the full name from first and last name concatenation

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.Email = emailValue.Text;
                p.Phone = phoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p); // we capture back teh id as well

                selectedTeamMembers.Add(p);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (phoneValue.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if(p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                //refresh that list
                WireUpLists();
                //selectTeamMemberDropDown.Refresh(); -- this doesn't work
                //teamMembersListBox.Refresh(); -- this doesn't work
            }
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);
                WireUpLists();
            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);
            callingForm.TeamComplete(t);
            this.Close();
        }
    }
}
