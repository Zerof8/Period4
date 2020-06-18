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

        public cameraPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        public void Init()
        {
            var output = App.ListsDatabase.GetListsPerUser(1);
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
            string listname = compartmentPicker.SelectedItem.ToString();
            var output = App.ListsDatabase.GetListId(1, listname);

            selectedListId = output[0].ListId;
        }
        private void OnTextChanged(object sender, EventArgs e)
        {
            var output = App.ProductDatabase.SelectProduct(barCode.Text);

            if (output.Any())
            {

                string productName = output[0].productName;
                productNameText.Text = productName;

                openFunctions();
            }
            else
            {
                productNameText.Text = "No such product found";
                closeFunctions();
            }
        }

        private void openFunctions()
        {
            scanButton.IsVisible = false;
            compartmentPicker.IsVisible = true;
            hidden1.IsVisible = true;
            hidden2.IsVisible = true;
            hidden3.IsVisible = true;
            hidden4.IsVisible = true;
            startTime.IsVisible = true;
            expDate.IsVisible = true;

        }

        private void closeFunctions()
        {
            scanButton.IsVisible = true;
            compartmentPicker.IsVisible = false;
            hidden1.IsVisible = false;
            hidden2.IsVisible = false;
            hidden3.IsVisible = false;
            hidden4.IsVisible = false;
            startTime.IsVisible = false;
            expDate.IsVisible = false;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            productNameText.Text = "";
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            string BarCode = barCode.Text;
            int ListId = selectedListId;
            DateTime StartDate = startTime.Date;
            DateTime ExpDate = expDate.Date;

            if (App.ProductListDatabase.SaveProductLists(new ProductList(BarCode, ListId, 10.0, 6, StartDate, ExpDate)) == 1)
            {
                DisplayAlert("Alert", "The product was added to your list", "Ok");
            }
            else
            {
                DisplayAlert("Alert", "The product was not added to your list", "Ok");
            }

            //Add quantity and price
            /* this.BarCode = BarCode;
             this.ListId = ListId;
             this.Price = Price;
             this.Quantity = Quantity;
             this.BuyDate = BuyDate;
             this.ExpDate = ExpDate;*/
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
    }
}