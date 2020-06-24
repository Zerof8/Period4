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
            //if login is made need to add the listId
            var output = database.Query<QueryForAll>("SELECT ProductList.BarCode, ProductList.Quantity, ProductList.ListId, Product.productName, Category.FilePath " +
                                               "FROM Product, Category, ProductList " +
                                               "WHERE Category.CategoryName = Product.category AND Product.barCode = ProductList.BarCode");

            return output;
        }
    }
}
