using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.PDF417.Internal;

namespace GEM.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreInf : ContentPage
    {
        public MoreInf(string barcode, int listId)
        {
            InitializeComponent();

            try
            {
                var output = App.QueryForAllDatabase.GetInfoAboutProduct(barcode, listId);

                title.Text = output[0].ProductName;
                category.Text = output[0].Category;
                purchDate.Text = output[0].BuyDate.ToString();
                amount.Text = output[0].Amount;
                expDate.Text = output[0].ExpDate.ToString();

                DateTime endDate = output[0].ExpDate;
                DateTime startDate = DateTime.Now;

                double days = (endDate - startDate).TotalDays;

                if (days <= -1)
                {
                    daysExp.Text = "Expired!";
                    expBorder.BackgroundColor = Color.FromHex("#f76a63");
                }
                else if (days < 0)
                {
                    daysExp.Text = "Expires today!";
                    expBorder.BackgroundColor = Color.FromHex("#f76a63");
                }
                else if (days > 0 && days < 1)
                {
                    daysExp.Text = "1 day";
                    expBorder.BackgroundColor = Color.FromHex("#fafa55");
                }
                else if (days <= 14)
                {
                    daysExp.Text = Math.Round(days, MidpointRounding.AwayFromZero) + " days";
                    expBorder.BackgroundColor = Color.FromHex("#fafa55");
                }
                else
                {
                    daysExp.Text = Math.Round(days, MidpointRounding.AwayFromZero) + " days";
                    expBorder.BackgroundColor = Color.FromHex("#98f294");
                }
                


                location.Text = output[0].ListName;
                price.Text = output[0].Price.ToString() + " €";
                quantity.Text = output[0].Quantity.ToString();
                barCode.Text = barcode;
            }
            catch(Exception e)
            {
                DisplayAlert("Alert", e.Message, "Ok");
            }
        }
    }
}