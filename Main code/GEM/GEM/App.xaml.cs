using GEM.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GEM
{
    public partial class App : Application
    {
        static ProductDatabaseController productDatabase;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static ProductDatabaseController ProductDatabase
        {
            get
            {
                if (productDatabase == null)
                {
                    productDatabase = new ProductDatabaseController();
                }
                return productDatabase;
            }
        }
    }
}
