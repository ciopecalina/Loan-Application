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
    public partial class WelcomePage : ContentPage
    {
        public static Loan loan;
        public WelcomePage()
        {
            InitializeComponent();
            if (loan == null)
            {
                loan = new Loan();
            }
        }

        private void Button_Selection(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectionPage());
        }

        private void Button_History(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }

        private void Button_Banks(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListOfBanksPage());
        }

        private void Button_Settings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }
    }
}