using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDMProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfBanksPage : ContentPage
    {
        List<Bank> listAllBanks;
        public ListOfBanksPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadXMLData();
            
        }

        private async void LoadXMLData()
        {
            try
            {
                listAllBanks = await NetUtils.GetBank();
                listViewBanks.ItemsSource = listAllBanks;
                BindingContext = listAllBanks[0];
                BtnRefresh.IsVisible = false;
                LabelLoading.IsVisible = false;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "Please refresh the page!", "OK");
                BtnRefresh.IsEnabled = true;
                BtnRefresh.Text = "Refresh";
                BtnRefresh.IsVisible = true;
                LabelLoading.IsVisible = false;
            }
        }

        private void BtnRefresh_Refresh(object sender, EventArgs e)
        {
            BtnRefresh.IsEnabled = false;
            BtnRefresh.Text = "Refreshing...";
            LoadXMLData();
        }
    }
}