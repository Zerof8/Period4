using GEM.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace GEM.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cameraPage : ContentPage
    {
        private int selectedListId;
        private bool toggled = false;

        public cameraPage()
        {
            InitializeComponent();

            for (int i = 1; i <= 20; i++)
            {
                quantity.Items.Add(i.ToString());
            }
            quantity.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
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
            catch(System.NullReferenceException)
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
                    barCode.Text = "";
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void OnTextChanged(object sender, EventArgs e)
        {
            var output = App.ProductDatabase.SelectProduct(barCode.Text);

            if (output.Any())
            {

                string productName = output[0].productName;
                productNameText.Text = productName;

                openFunctions1();
            }
            else
            {
                productNameText.Text = "No such product found";
                closeFunctions1();
            }
        }

        private void openFunctions1()
        {
            scanButton.IsVisible = false;
            productWB.IsVisible = false;
            compartmentPicker.IsVisible = true;
            mySwitch.IsVisible = true;
            cancelButton.IsVisible = true;
            addButton.IsVisible = true;
        }

        private void openFunctions2()
        {
            expLabel.IsVisible = true;
            quantity.IsVisible = true;
            expLabel.IsVisible = true;
            expDate.IsVisible = true;
            price.IsVisible = true;
        }

        private void closeFunctions1()
        {
            scanButton.IsVisible = true;
            productWB.IsVisible = true;
            compartmentPicker.IsVisible = false;
            mySwitch.IsVisible = false;
            mySwitch.IsToggled = false;
            cancelButton.IsVisible = false;
            addButton.IsVisible = false;
        }

        private void closeFunctions2()
        {
            expLabel.IsVisible = false;
            quantity.IsVisible = false;
            expLabel.IsVisible = false;
            expDate.IsVisible = false;
            price.IsVisible = false;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            barCode.Text = "";
        }

        private void switch_Toggled(object sender, EventArgs e)
        {
            if(mySwitch.IsToggled)
            {
                openFunctions2();
                toggled = true;
            }
            else
            {
                closeFunctions2();
                toggled = false;
            }
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            string BarCode = barCode.Text;
            int ListId = selectedListId;

            int Quantity = Int32.Parse(quantity.SelectedItem.ToString());
            bool parsed;
            double PricePr;
            DateTime StartDate = DateTime.Now;
            DateTime ExpDate;

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

            if (!toggled)
            {
                var output = App.ExpiratonDateDatabase.GetAverage(BarCode);

                int[] average = new int[output.Count()];
                int c = 0;

                foreach (var current in output)
                {
                    int k = (current.ExpDate - current.StartDate).Days;
                    average[c] = k;
                    c++;
                }

                double avg = Queryable.Average(average.AsQueryable());
                ExpDate = DateTime.Now.AddDays(avg);
            }
            else
            {
                ExpDate = expDate.Date;
            }

            
            

            if(parsed)
            {
                var check = App.ProductListDatabase.CheckForList(BarCode, ListId);

                if (!check.Any())
                {
                    if (App.ProductListDatabase.SaveProductLists(new ProductList(BarCode, ListId, PricePr, Quantity, StartDate, ExpDate)) == 1)
                    {
                        DisplayAlert("Alert", "The product was added to your list", "Ok");
                        barCode.Text = "";
                        compartmentPicker.SelectedIndex = 0;
                        price.Text = "";
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
            quantity.SelectedIndex = 0;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();

                    barCode.Text = result.Text;
                });
            };
        }

        private async void ButtonWB_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWBtoPL());
        }
    }
}