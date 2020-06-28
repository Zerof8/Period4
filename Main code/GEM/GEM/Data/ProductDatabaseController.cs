using GEM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GEM.Data
{
    public class ProductDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public ProductDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Product>();
        }

        public IEnumerator<Product> GetProducts()
        {
            lock (locker)
            {
                if (database.Table<Product>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Product>().GetEnumerator();
                }
            }
        }
        
        public Product GetProduct()
        {
            lock (locker)
            {
                if (database.Table<Product>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Product>().First();
                }
            }    
        }

        public int SaveProduct(Product product)
        {
            lock (locker)
            {
                if (product.Id != 0)
                {
                    database.Update(product);
                    return product.Id;
                }
                else
                {
                    return database.Insert(product);
                }
            }
        }

        public List<Product> GetProductsWB()
        {
            var output = database.Query<Product>("SELECT productName, Category " +
                                               "FROM Product " +
                                               "WHERE barCode IS NULL " +
                                               "ORDER BY productName");
            return output;
        }

        public List<Product> SelectProduct(string BarCode)
        {
            var output = database.Query<Product>("SELECT productName, category, amount FROM Product WHERE barCode = ?", BarCode);
            return output;
        }

        public List<Product> SelectProductWB(string Name)
        {
            var output = database.Query<Product>("SELECT  productName, category, amount FROM Product WHERE productName = ?", Name);
            return output;
        }

        public int DeleteProduct(int id)
        {
            lock (locker)
            {
                return database.Delete<Product>(id);
            }
        }

        public int DeleteProductsData()
        {
            SQLiteCommand comm = database.CreateCommand("DELETE from Product");

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }
    }
}
