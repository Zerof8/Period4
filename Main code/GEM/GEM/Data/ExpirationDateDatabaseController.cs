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
    public class ExpirationDateDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public ExpirationDateDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<ExpirationDate>();
        }

        public IEnumerator<ExpirationDate> GetExpirationDates()
        {
            lock (locker)
            {
                if (database.Table<ExpirationDate>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<ExpirationDate>().GetEnumerator();
                }

                
            }
        }
        
        public ExpirationDate GetExpirationDate()
        {
            lock (locker)
            {
                if (database.Table<ExpirationDate>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<ExpirationDate>().First();
                }
            }    
        }

        public List<ExpirationDate> GetAverage(string barCode)
        {
            var output = database.Query<ExpirationDate>("SELECT ExpDate, StartDate " +
                                                        "FROM ExpirationDate " +
                                                        "WHERE (ExpDate IS NOT NULL AND StartDate IS NOT NULL) " +
                                                        "AND PrBarCode = ?", barCode);
            return output;
        }
        public int SaveExpirationDate(ExpirationDate expirationDate)
        {
            lock (locker)
            {
                if (expirationDate.ExpDateId != 0)
                {
                    database.Update(expirationDate);
                    return expirationDate.ExpDateId;
                }
                else
                {
                    return database.Insert(expirationDate);
                }
            }
        }

        public int DeleteExpDates()
        {
            SQLiteCommand comm = database.CreateCommand("DELETE from ExpirationDate");

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }
    }
}
