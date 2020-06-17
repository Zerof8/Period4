using GEM.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GEM
{
    public partial class App : Application
    {
        static ProductDatabaseController productDatabase;
        static ListsDatabaseController listsDatabase;
        static CategoryDatabaseController categoryDatabase;
        static ExpirationDateDatabaseController expirationDateDatabase;

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

        public static CategoryDatabaseController CategoryDatabase
        {
            get
            {
                if (categoryDatabase == null)
                {
                    categoryDatabase = new CategoryDatabaseController();
                }

                return categoryDatabase;
            }
        }

        public static ListsDatabaseController ListsDatabase
        {
            get
            {
                if (listsDatabase == null)
                {
                    listsDatabase = new ListsDatabaseController();
                }
                return listsDatabase;
            }
        }

        public static ExpirationDateDatabaseController ExpiratonDateDatabase
        {
            get
            {
                if (expirationDateDatabase == null)
                {
                    expirationDateDatabase = new ExpirationDateDatabaseController();
                }

                return expirationDateDatabase;
            }
        }
    }
}
