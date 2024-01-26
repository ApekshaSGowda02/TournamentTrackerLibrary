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
using TournamentTrackerLibrary.DataAccess;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(placeNameValue.Text, placeNumberValue.Text, prizeAmountValue.Text, prizePercentageValue.Text);
                // instead of the below where we may need to parse the int or etc as text we can simply
                // create a constructor in the model class and pass all the values and convert the values there instead and pass them as parameters here as above
                //model.PlaceName = placeNameValue.Text;
                //model.PlaceNumber = placeNumberValue.Text;

                //now we save our model once we have it 
                foreach (IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }
                placeNameValue.Text = "";
                placeNumberValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";
            }
            else
            {
                MessageBox.Show("This form had invalid information. Please check and try again");
            }
        }
        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            decimal prizeAmount = 0;
            double prizePercent = 0;
            // So the int.TryParse here tries to take the value in the place number which is a text and tries
            // to convert it into an int. If it can then it will put that into the placeNumber variable (which is an out)
            // and also returns boolean value true as a whole
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);
            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentValid = double.TryParse(prizePercentageValue.Text, out prizePercent);

            if (!placeNumberValidNumber)
            {
                output = false;
            }
            if (placeNumber < 0)
            {
                output = false;
            }
            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }
            if ((!prizeAmountValid) || (!prizePercentValid))
            {
                output = false;
            }
            if (prizeAmount <= 0 && prizePercent <= 0)
            {
                output = false;
            }
            if(prizePercent < 0 || prizePercent > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
