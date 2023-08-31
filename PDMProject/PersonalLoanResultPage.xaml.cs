using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDMProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalLoanResultPage : ContentPage
    {
        DBManager dBManager;
        public PersonalLoanResultPage()
        {
            InitializeComponent();
            DisplayCurrencyType();
            CalculateResults();
            DisplayResults();
        }

        private void DisplayCurrencyType()
        {
            LabelCurrency.Text ="Currency " + WelcomePage.loan.Currency;
        }

        private void CalculateResults()
        {
            double r = WelcomePage.loan.Rate / 12 / 100;
            double nr = 1 + r;
            double month = WelcomePage.loan.Tenure * 12;
            double up = Math.Pow(nr, month);
            double down = Math.Pow(nr, month) - 1;
            double frac = up / down;
            double result = WelcomePage.loan.Amount * r * frac;

            WelcomePage.loan.EMIAmount = result;
            WelcomePage.loan.TotalPayable = WelcomePage.loan.EMIAmount * month;
            WelcomePage.loan.TotalInterest = WelcomePage.loan.TotalPayable - WelcomePage.loan.Amount;
            WelcomePage.loan.PrincipalAmount = WelcomePage.loan.Amount;
            
        }

        private void DisplayResults()
        {
            EntryPrincipalAmount.Text = WelcomePage.loan.PrincipalAmount.ToString();
            EntryTotalInterest.Text = String.Format("{0:0.00}", WelcomePage.loan.TotalInterest);
            EntryPayable.Text = String.Format("{0:0.00}", WelcomePage.loan.TotalPayable);
            EntryEMIAmount.Text = String.Format("{0:0.00}", WelcomePage.loan.EMIAmount);
        }

        private async void Button_Save_Data(object sender, EventArgs e)
        {
            dBManager = new DBManager();
           
            if (dBManager.AddLoanToDB(WelcomePage.loan) == 1)
            {
                await DisplayAlert("Success!", "The record has been successfully saved", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "Cannot write to database", "OK");
            }
            
        }
    }
}