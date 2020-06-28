using GEM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GEM.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private ObservableCollection<Product> items = new ObservableCollection<Product>();
        public Page1()
        {
            InitializeComponent();
            Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        public void Init()
        {
            try
            {
                var output = App.QueryForAllDatabase.GetAllProductsPerUser();

                if (output.Any())
                {
                    allProducts.ItemsSource = output;
                }
                else
                {
                    allProducts.ItemsSource = null;
                }
            }
            catch(Exception e)
            {
                
            }
        }

        public void TapGestureRecognizer_Tap_Remove(object sender, EventArgs e)
        {
            var something = sender as Image;
            var what = something.BindingContext as QueryForAll;

            if (App.ProductListDatabase.DeleteProduct(what.ListId, what.BarCode) == 1)
            {
                DisplayAlert("Alert", "Product deleted successfully", "Ok");
                Init();
            }
            else
            {
                DisplayAlert("Alert", "Product was not deleted", "Ok");
            }
        }

        public void item_tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var myselectedItem = e.Item as QueryForAll;
            Navigation.PushAsync(new MoreInf(myselectedItem.BarCode, myselectedItem.ListId));
        }
    }
}