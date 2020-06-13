using GEM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public void Init()
        {
            var enumerator = App.ProductDatabase.GetProducts();

            while (enumerator.MoveNext())
            {
                this.items.Add(enumerator.Current);
            }
            allProducts.ItemsSource = items;
        }

    }
}