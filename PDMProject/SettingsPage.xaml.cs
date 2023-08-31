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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            picker.SelectedIndex = 0;
        }

        private async void Button_Delete_History(object sender, EventArgs e)
        {
            DBManager dBManager = new DBManager();

            if (dBManager.DeleteAllData() == 0)
            {
                await DisplayAlert("Success!", "The history data has been wiped successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "Cannot wipe history", "OK");
            }
        }

        private void picker_currency(object sender, EventArgs e)
        {
            WelcomePage.loan.Currency = picker.SelectedItem.ToString();
        }
    }
}