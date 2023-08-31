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
    public partial class HistoryPage : ContentPage
    {
        Loan selectedLoan;

        public HistoryPage()
        {
            InitializeComponent();
        }

        List<Loan> listaLoan = new DBManager().LoadData();

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            DBManager manager = new DBManager();

            listaLoan = manager.LoadData();

            if(listaLoan.Count != 0)
            {
                listViewLoans.ItemsSource = listaLoan;
                BindingContext = listaLoan[0];
            }
            
        }

        void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            selectedLoan = e.SelectedItem as Loan;
        }

        private void editItem(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditRecordPage(selectedLoan));

        }
    
    }
}