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
    public class ProductListDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public ProductListDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<ProductList>();
        }

        public IEnumerator<ProductList> GetProducts()
        {
            lock (locker)
            {
                if (database.Table<ProductList>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<ProductList>().GetEnumerator();
                }

                
            }
        }
        
        public Product GetProductLists()
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

        public List<ProductList> CheckForList(string barcode, int listid)
        {
            var output = database.Query<ProductList>("SELECT * " +
                                               "FROM ProductList " +
                                               "WHERE BarCode = ? AND ListId = ?", barcode, listid);
            return output;
        }

        public int UpdateQuantity(int oldQuantity, int newQuantity, string barcode, int listid, double PricePr, DateTime ExpDate)
        {
            newQuantity = oldQuantity + newQuantity;

            SQLiteCommand comm = database.CreateCommand("UPDATE ProductList " +
                                                        "SET quantity = ?, " +
                                                        "Price = ?, " +
                                                        "ExpDate = ? " +
                                                        "WHERE ListId = ? " +
                                                        "AND BarCode = ?", newQuantity, PricePr, ExpDate, listid, barcode);

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }

        public int SaveProductLists(ProductList productList)
        {
            lock (locker)
            {
                if (productList.ProductListId != 0)
                {
                    database.Update(productList);
                    return productList.ListId;
                }
                else
                {
                    return database.Insert(productList);
                }
            }
        }

        public int DeleteProduct(int ListId, string barcode)
        {
            SQLiteCommand comm = database.CreateCommand("DELETE from ProductList " +
                                                        "WHERE ListId = ? " +
                                                        "AND BarCode = ?", ListId, barcode);

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }

        public int DeleteProductListData()
        {
            SQLiteCommand comm = database.CreateCommand("DELETE from ProductList");

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }
    }
}
