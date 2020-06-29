using GEM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms;

namespace GEM.Data
{
    public class QueryForAllDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public QueryForAllDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<QueryForAll>();
        }

        public IEnumerator<QueryForAll> GetQueryForAll()
        {
            lock (locker)
            {
                if (database.Table<QueryForAll>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<QueryForAll>().GetEnumerator();
                }
            }
        }
        
        public List<QueryForAll> GetAllProductsPerUser()
        {
            var output = database.Query<QueryForAll>("SELECT ProductList.BarCode, ProductList.Quantity, ProductList.ListId, Product.productName, Category.FilePath " +
                                               "FROM Product, Category, ProductList " +
                                               "WHERE Category.CategoryName = Product.category AND (Product.barCode = ProductList.BarCode OR ProductList.BarCode = Product.Id)");

            return output;
        }

        public List<QueryForAll> GetAllProductsWB()
        {
            var output = database.Query<QueryForAll>("SELECT Product.Id, Product.productName, Product.amount, Category.FilePath " +
                                                     "FROM Product " +
                                                     "JOIN Category " +
                                                     "ON Category.CategoryName = Product.category " +
                                                     "WHERE Product.barCode IS NULL " +
                                                     "ORDER BY Product.productName");

            return output;
        }

        public List<QueryForAll> GetInfoAboutProduct(string barCode, int ListId)
        {
            var output = database.Query<QueryForAll>("SELECT Product.productName, Product.category, Product.amount, " +
                                                     "ProductList.BuyDate, ProductList.ExpDate, ProductList.Price, ProductList.Quantity, " +
                                                     "Lists.ListName, Lists.ListCategory " +
                                                     "FROM Product, ProductList, Lists " +
                                                     "WHERE (Product.barCode = ProductList.BarCode OR Product.Id = ProductList.BarCode) " +
                                                     "AND ProductList.ListId = Lists.ListId " +
                                                     "AND ProductList.BarCode = ? AND ProductList.ListId = ?", barCode, ListId);

            return output;
        }
    }
}
