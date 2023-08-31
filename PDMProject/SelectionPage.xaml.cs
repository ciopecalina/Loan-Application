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
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Personal_Loan(object sender, CheckedChangedEventArgs e)
        {
            WelcomePage.loan.LoanType = "Personal";
        }

        private void RadioButton_House_Loan(object sender, CheckedChangedEventArgs e)
        {
            WelcomePage.loan.LoanType = "House";
        }

        private void RadioButton_Car_Loan(object sender, CheckedChangedEventArgs e)
        {
            WelcomePage.loan.LoanType = "Car";
        }

        private void Button_Next(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalculatorPage());
        }
    }

   
}