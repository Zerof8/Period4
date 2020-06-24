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
    public class CategoryDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public CategoryDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Category>();
        }

        public IEnumerator<Category> GetCategories()
        {
            lock (locker)
            {
                if (database.Table<Category>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Category>().GetEnumerator();
                }
            }
        }
        
        public Category GetProductLists()
        {
            lock (locker)
            {
                if (database.Table<Category>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Category>().First();
                }
            }    
        }

        public int SaveProductLists(Category category)
        {
            lock (locker)
            {
                if (category.CategoryId != 0)
                {
                    database.Update(category);
                    return category.CategoryId;
                }
                else
                {
                    return database.Insert(category);
                }
            }
        }

        public int DeleteCategories()
        {
            lock (locker)
            {
                SQLiteCommand comm = database.CreateCommand("DELETE from Category");

                lock (locker)
                {
                    return comm.ExecuteNonQuery();
                }
            }
        }
    }
}
