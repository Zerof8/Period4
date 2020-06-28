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
    public partial class AddSelectedWB : ContentPage
    {
        private int productId { get; set; }
        private int selectedListId;

        public AddSelectedWB(int productId)
        {
            InitializeComponent();
            Init();

            for (int i = 1; i <= 20; i++)
            {
                quantity.Items.Add(i.ToString());
            }
            quantity.SelectedIndex = 0;

            this.productId = productId;
        }
        public void Init()
        {
            var output = App.ListsDatabase.GetListsPerUser();
            compartmentPicker.Items.Clear();

            if (output.Any())
            {
                for (int i = 0; i < output.Count; i++)
                {
                    compartmentPicker.Items.Add(output[i].ListName);
                }
            }
            else
            {
                DisplayAlert("Alert", "there are no lists for this user", "ok");
            }
        }

        private void Selected_Changed(object sender, EventArgs e)
        {
            string listname;

            try
            {
                listname = compartmentPicker.SelectedItem.ToString();
            }
            catch (System.NullReferenceException)
            {
                listname = "";
            }

            try
            {
                var output = App.ListsDatabase.GetListId(listname);

                try
                {
                    selectedListId = output[0].ListId;
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void openFunctions()
        {
            quantity.IsVisible = true;
            expLabel.IsVisible = true;
            expDate.IsVisible = true;
            price.IsVisible = true;
        }
        private void closeFunctions()
        {
            expLabel.IsVisible = false;
            quantity.IsVisible = false;
            expLabel.IsVisible = false;
            expDate.IsVisible = false;
            price.IsVisible = false;
        }

        private void switch_Toggled(object sender, EventArgs e)
        {
            if (mySwitch.IsToggled)
            {
                openFunctions();
            }
            else
            {
                closeFunctions();
            }
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            string BarCode = productId.ToString();
            int ListId = selectedListId;

            int Quantity = Int32.Parse(quantity.SelectedItem.ToString());
            bool parsed;
            double PricePr;

            if (!String.IsNullOrEmpty(price.Text))
            {
                parsed = Double.TryParse(price.Text, out double pricePr);
                PricePr = pricePr;
            }
            else
            {
                parsed = true;
                PricePr = 0;
            }


            DateTime StartDate = DateTime.Now;
            DateTime ExpDate = expDate.Date;

            if (parsed)
            {
                var check = App.ProductListDatabase.CheckForList(BarCode, ListId);

                if (!check.Any())
                {
                    if (App.ProductListDatabase.SaveProductLists(new ProductList(BarCode, ListId, PricePr, Quantity, StartDate, ExpDate)) == 1)
                    {
                        DisplayAlert("Alert", "The product was added to your list", "Ok");
                        Navigation.PopAsync();
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Alert", "The product was not added to your list", "Ok");
                    }
                }
                else
                {
                    App.ProductListDatabase.UpdateQuantity(check[0].Quantity, Quantity, BarCode, ListId, PricePr, ExpDate);
                }
            }
            else
            {
                DisplayAlert("Alert", "Please enter a valid number", "Ok");
            }
        }
    }
}