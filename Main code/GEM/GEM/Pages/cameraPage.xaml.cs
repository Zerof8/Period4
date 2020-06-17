using GEM.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Lists> items = new ObservableCollection<Lists>();

        public cameraPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var enumerator = App.ListsDatabase.GetAllLists();

            if (enumerator != null)
            { 
                while (enumerator.MoveNext())
                {
                    compartmentPicker.Items.Add(enumerator.Current.ListName + "Type: " + enumerator.Current.ListCategory);
                }
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
                    mycode.Text = result.Text;

                    Product product = new Product()
                    {
                        barCode = result.Text
                    };

                    mycode.Text = result.Text;
                    //App.ProductDatabase.SaveProduct(product);
                });
            };

        }
    }
}