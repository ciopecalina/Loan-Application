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
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();

            SetCurrencyAndLoanLabels();
         
            EntryAmount.Text = String.Empty;
            EntryRate.Text = String.Empty;
            EntryTenure.Text = String.Empty;
        }

        private void SetCurrencyAndLoanLabels()
        {
            LabelLoanAmount.Text = "Loan Amount (" + WelcomePage.loan.Currency + ")";
            LoanLabel.Text = WelcomePage.loan.LoanType + " Loan";
        }

        private void SetCalculationDataInLoan()
        {
            WelcomePage.loan.Amount = Double.Parse(EntryAmount.Text);
            WelcomePage.loan.Rate = Double.Parse(EntryRate.Text);
            WelcomePage.loan.Tenure = int.Parse(EntryTenure.Text);
        }

        private async void Button_Calculate(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                await DisplayAlert("Warning!", "Please make sure every field is not empty and contains only numbers.", "OK");
            }else
            {

                SetCalculationDataInLoan();

                //Loading appropriate result page
                switch (WelcomePage.loan.LoanType)
                {
                    case "Personal":
                       await Navigation.PushAsync(new PersonalLoanResultPage());
                        break;

                    case "House":
                       await Navigation.PushAsync(new HouseLoanResultPage());
                        break;

                    case "Car":
                       await Navigation.PushAsync(new CarLoanResultPage());
                        break;
                }
            }
            
        }

        private bool CheckFields() //false = everything ok .. true = warning
        {
            if (EntryAmount.Text.Equals(String.Empty))
            {
                return true;
            }
            else
            {
                double entryAmount;
                if (double.TryParse(EntryAmount.Text, out entryAmount) == false)
                {
                    return true;
                }

            }

            if (EntryRate.Text.Equals(String.Empty))
            {
                return true;
            }
            else
            {
                double entryRate;
                if (double.TryParse(EntryRate.Text, out entryRate) == false)
                {
                    return true;
                }
            }

            if (EntryTenure.Text.Equals(String.Empty))
            {
                return true;
            }
            else
            {
                foreach (char c in EntryTenure.Text)
                {
                    if (c < '0' || c > '9')
                        return true;
                }
            }

            return false;
        }

        private async void Button_Clear(object sender, EventArgs e)
        {
            EntryAmount.Text = String.Empty;
            EntryRate.Text = String.Empty;
            EntryTenure.Text = String.Empty;

            await DisplayAlert("Cleared!", "All fields have been cleared", "OK");
        }
    }
}