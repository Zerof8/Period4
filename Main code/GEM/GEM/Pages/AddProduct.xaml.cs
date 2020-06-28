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
    public partial class Page5 : ContentPage
    {
        public Page5()
        {
            InitializeComponent();
        }

        public async void addProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4());
        }

        public async void addProductWB_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductWB());
        }
    }
}