using GEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GEM.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWBtoPL : ContentPage
    {
        public AddWBtoPL()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            try
            {
                var output = App.QueryForAllDatabase.GetAllProductsWB();

                if (output.Any())
                {
                    productsWB.ItemsSource = output;
                }
                else
                {
                    productsWB.ItemsSource = null;
                    DisplayAlert("Alert", "no available products", "ok");
                }
            }
            catch (Exception e)
            {

            }
        }

        private void TapGestureRecognizer_Tap_Remove(object sender, EventArgs e)
        {
            var something = sender as Image;
            var what = something.BindingContext as QueryForAll;

            Navigation.PushAsync(new AddSelectedWB(what.Id));
        }
    }
}