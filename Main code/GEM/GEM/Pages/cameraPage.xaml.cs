using GEM.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace GEM.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cameraPage : ContentPage
    {
        public cameraPage()
        {
            InitializeComponent();

            //Connect to database and fill the dropdown with possible values
            //compartmentPicker.Items.Add("hi");

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
                    mycode.Text = result.Text;

                    Product product = new Product()
                    {
                        barCode = result.Text
                    };

                    App.ProductDatabase.SaveProduct(product);
                });
            };

        }
    }
}