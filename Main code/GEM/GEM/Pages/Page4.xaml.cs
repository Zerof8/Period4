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
    public partial class Page4 : ContentPage
    {
        public Page4()
        {
            InitializeComponent();
            Init();

            //startTime.Date = DateTime.Now;
        }

        public void Init()
        {
            var enumerator = App.CategoryDatabase.GetCategories();

            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    foodTypePicker.Items.Add(enumerator.Current.CategoryName);
                }
            }
            else
            {
                App.CategoryDatabase.SaveProductLists(new Category("Dairies", "dairies.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Meat", "meat.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Vegetables & fruit", "fruit.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Drinks", "drinks.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Nuts & grains", "nuts.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Pastries", "pastries.png"));
                App.CategoryDatabase.SaveProductLists(new Category("Sweets", "sweets.png"));
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var output = App.ProductDatabase.SelectProduct(barCode.Text);

            if (output.Any())
            {
                
                string productName = output[0].productName;
                string category = output[0].category;
                string amountProduct = output[0].amount;

                name.Text = productName;
                name.IsReadOnly = true;

                foodTypePicker.SelectedItem = category;
                foodTypePicker.IsEnabled = false;

                amount.Text = amountProduct;
                amount.IsReadOnly = true;
            }
            else
            {
                name.IsReadOnly = false;
                name.Text = "";
                foodTypePicker.IsEnabled = true;
                amount.Text = "";
                amount.IsReadOnly = false;
            }
        }

        private void addProduct_Clicked(object sender, EventArgs e)
        {
            var output = App.ProductDatabase.SelectProduct(barCode.Text);

            string barCodeDb = barCode.Text;
            DateTime StartDate = startTime.Date;
            DateTime ExpDate = expDate.Date;

            if (!output.Any())
            {
                string productName = name.Text;
                string amountDb = amount.Text;
                string category =  foodTypePicker.SelectedItem.ToString();

                if (barCodeDb != null && productName != null && amountDb != null && category != null)
                {
                    if (App.ProductDatabase.SaveProduct(new Product(barCodeDb, productName, category, amountDb)) == 1)
                    {
                        DisplayAlert("Alert", "Product saved", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Alert", "Could not save data", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Please fill in everything!", "Ok");
                }
                
            }
            
            if (barCodeDb != null)
            {
                if (ExpDate > StartDate)
                {
                    if (App.ExpiratonDateDatabase.SaveExpirationDate(new ExpirationDate(barCodeDb, StartDate, ExpDate)) == 1)
                    {
                        DisplayAlert("Alert", "Expiration date saved", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Alert", "Could not save data", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Production date cannot be greater than expiraton date.", "Ok");
                }
            }
        }
    }
}