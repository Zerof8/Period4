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
    public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete?", "This will delete all data!", "Ok", "Cancel");

            if (answer)
            {
                int i = App.ListsDatabase.DeleteListsData();
                int k = App.ProductDatabase.DeleteProductsData();
                int j = App.ProductListDatabase.DeleteProductListData();
                int l = App.CategoryDatabase.DeleteCategories();

                if (i != 0)
                {
                    DisplayAlert("Lists", "All lists were deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Lists", "No list were deleted", "Ok");
                }

                if (k != 0)
                {
                    DisplayAlert("Products", "All products were deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Products", "No products were deleted", "Ok");
                }

                if (j != 0)
                {
                    DisplayAlert("Products", "All productlists were deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Products", "No productlists were deleted", "Ok");
                }

                if (l != 0)
                {
                    DisplayAlert("Products", "All categories were deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Products", "No categories were deleted", "Ok");
                }
            }
            
        }
    }
}