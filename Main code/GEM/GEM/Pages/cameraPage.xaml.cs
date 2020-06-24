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
            string listname;

            try
            {
                listname = compartmentPicker.SelectedItem.ToString();
            }
            catch(System.NullReferenceException)
            {
                listname = "";
            }

            var output = App.ListsDatabase.GetListId(1, listname);

            try
            {
                selectedListId = output[0].ListId;
            }
            catch(System.ArgumentOutOfRangeException)
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
            compartmentPicker.IsVisible = true;
            mySwitch.IsVisible = true;
            cancelButton.IsVisible = true;
            addButton.IsVisible = true;
        }

        private void openFunctions2()
        {
            //prodLabel.IsVisible = true;
            expLabel.IsVisible = true;
            quantity.IsVisible = true;
            expLabel.IsVisible = true;
            //startTime.IsVisible = true;
            expDate.IsVisible = true;
        }

        private void closeFunctions1()
        {
            scanButton.IsVisible = true;
            compartmentPicker.IsVisible = false;
            mySwitch.IsVisible = false;
            mySwitch.IsToggled = false;
            cancelButton.IsVisible = false;
            addButton.IsVisible = false;
        }

        private void closeFunctions2()
        {
            //prodLabel.IsVisible = false;
            expLabel.IsVisible = false;
            quantity.IsVisible = false;
            expLabel.IsVisible = false;
            //startTime.IsVisible = false;
            expDate.IsVisible = false;
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
            }
            else
            {
                closeFunctions2();
            }
        }

        private void AddProduct_Clicked(object sender, EventArgs e)
        {
            string BarCode = barCode.Text;
            int ListId = selectedListId;
            
            int Quantity = Int32.Parse(quantity.SelectedItem.ToString());
            DateTime StartDate = DateTime.Now;
            DateTime ExpDate = expDate.Date;

            if (App.ProductListDatabase.SaveProductLists(new ProductList(BarCode, ListId, 10.0, Quantity, StartDate, ExpDate)) == 1)
            {
                DisplayAlert("Alert", "The product was added to your list", "Ok");
            }
            else
            {
                DisplayAlert("Alert", "The product was not added to your list", "Ok");
            }
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