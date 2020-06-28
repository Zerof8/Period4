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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();

            categoryPicker.Items.Add("Fridge");
            categoryPicker.Items.Add("Freezer");
            categoryPicker.Items.Add("Cupboard");
        }

        private void AddList_Clicked(object sender, EventArgs e)
        {
            if (categoryPicker.SelectedIndex == -1)
            {
                DisplayAlert("Alert", "Please select a category", "Ok");
            }
            else
            {
                string category = categoryPicker.SelectedItem.ToString();
                string listName = ListName.Text;

                if (string.IsNullOrEmpty(listName))
                {
                    DisplayAlert("Alert", "Please type a name", "Ok");
                }
                else
                {
                    var output = App.ListsDatabase.GetListId(listName);

                    if (!output.Any())
                    {
                        Lists newLists = new Lists()
                        {
                            ListName = listName,
                            ListCategory = category
                        };

                        if (App.ListsDatabase.SaveLists(newLists) == 1)
                        {
                            DisplayAlert("Alert", "List saved", "Ok");
                            ListName.Text = "";
                            categoryPicker.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        DisplayAlert("Alert", "This name is already in use", "Ok");
                        ListName.Text = "";
                        categoryPicker.SelectedIndex = -1;
                    }
                    
                }
            }
        }
    }
}