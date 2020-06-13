using GEM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public int DeleteProduct(int id)
        {
            lock (locker)
            {
                return database.Delete<Product>(id);
            }
        }
    }
}
