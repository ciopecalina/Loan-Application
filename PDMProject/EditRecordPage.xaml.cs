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
    public partial class EditRecordPage : ContentPage
    {
        DBManager dBManager;
        Loan currentLoan;
        Loan newLoan;

        public EditRecordPage(Loan loan)
        {
            InitializeComponent();
            currentLoan = loan;

            LoanLabel.Text = loan.LoanType + " Loan";
            EntryAmount.Text = loan.Amount.ToString("0.00");
            EntryRate.Text = loan.Rate.ToString("0.00");
            EntryTenure.Text = loan.Tenure.ToString("0");
            LabelLoanAmount.Text = "Loan Amount (" + loan.Currency + ")";

            OldEntryTotalInterest.Text = loan.TotalInterest.ToString("0.00");
            OldEntryPayable.Text = loan.TotalPayable.ToString("0.00");
            OldEntryEMIAmount.Text = loan.EMIAmount.ToString("0.00");


            EntryTotalInterest.Text = String.Empty;
            EntryEMIAmount.Text = String.Empty;
            EntryPayable.Text = String.Empty;
        }

        private double getEMIAmount()
        {
            double amount = double.Parse(EntryAmount.Text, System.Globalization.CultureInfo.InvariantCulture);
            double rate = double.Parse(EntryRate.Text, System.Globalization.CultureInfo.InvariantCulture);
            double tenure = double.Parse(EntryTenure.Text, System.Globalization.CultureInfo.InvariantCulture);

            return computeEMIAmount(amount, rate, tenure);
        }

        private double getTotalPayable()
        {
            double emiAmount = getEMIAmount();
            double tenure = double.Parse(EntryTenure.Text, System.Globalization.CultureInfo.InvariantCulture);
            double numMonths = tenure * 12;

            return computeTotalPayable(emiAmount, numMonths);
        }

        private double getTotalInterest()
        {
            double totalPayable = getTotalPayable();
            double amount = double.Parse(EntryAmount.Text, System.Globalization.CultureInfo.InvariantCulture);

            return computeTotalInterest(totalPayable, amount);
        }

        private double computeEMIAmount(double amount, double yearlyRate, double tenure) //I
        {
            //r
            double monthlyInterestRate = yearlyRate / 12 / 100;

            //month
            double numMonths = tenure * 12;

            //up
            double up = Math.Pow(1 + monthlyInterestRate, numMonths);

            return amount * monthlyInterestRate * up / (up - 1);
        }

        private double computeTotalPayable(double emiAmount, double numMonths) 
        {
            return emiAmount * numMonths;
        }

        private double computeTotalInterest(double totalPayable, double amount)
        {
            return totalPayable - amount;
        }


        private async void Button_Calculate(object sender, EventArgs e)
        {
            if (CheckFields())
            {
                await DisplayAlert("Error!", "Please make sure all fields are not empty and contains only numbers.", "OK");
            }else
            {

                double resultEMI = getEMIAmount();
                double resultPayable = getTotalPayable();
                double resultInterest = getTotalInterest();

                EntryPayable.Text = resultPayable.ToString("F2");
                EntryEMIAmount.Text = resultEMI.ToString("F2");
                EntryTotalInterest.Text = resultInterest.ToString("F2");
            }
           

        }

        private async void Button_Override(object sender, EventArgs e)
        {
            if (CheckOutputFields())
            {
                await DisplayAlert("Error!", "Please make sure all output fields are not empty.", "OK");
            }
            else
            {
                newLoan = new Loan();

                newLoan.Id = currentLoan.Id;
                newLoan.LoanType = currentLoan.LoanType;
                newLoan.Currency = currentLoan.Currency;

                newLoan.Amount = Double.Parse(EntryAmount.Text);
                newLoan.Rate = Double.Parse(EntryRate.Text);
                newLoan.Tenure = Int32.Parse(EntryTenure.Text);

                newLoan.TotalInterest = Double.Parse(EntryTotalInterest.Text);
                newLoan.EMIAmount = Double.Parse(EntryEMIAmount.Text);
                newLoan.TotalPayable = Double.Parse(EntryPayable.Text);

                newLoan.PrincipalAmount = newLoan.Amount;
                newLoan.InitialPayment = 0.2f * newLoan.TotalPayable;

                dBManager = new DBManager();

                if (dBManager.UpdateLoan(newLoan) == 1)
                {
                    await DisplayAlert("Success!", "The record has been successfully edited!", "OK");
                }
                else
                {
                    await DisplayAlert("Error!", "Cannot update the database!", "OK");
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

        private bool CheckOutputFields() //false = everything ok .. true = warning
        {
            if (EntryAmount.Text.Equals(String.Empty) || EntryRate.Text.Equals(String.Empty) || EntryTenure.Text.Equals(String.Empty) || EntryTotalInterest.Text.Equals(String.Empty) ||
                EntryEMIAmount.Text.Equals(String.Empty) || EntryPayable.Text.Equals(String.Empty))
            {
                return true;
            }

            return false;
        }

    }
}