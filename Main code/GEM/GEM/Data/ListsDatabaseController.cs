using GEM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace GEM.Data
{
    public class ListsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public ListsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Lists>();
        }

        public IEnumerator<Lists> GetAllLists()
        {
            lock (locker)
            {
                if (database.Table<Lists>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Lists>().GetEnumerator();
                }
            }
        }

        public List<Lists> GetListsPerUser(int i)
        {
            var output = database.Query<Lists>("SELECT ListName, ListCategory " +
                                               "FROM Lists " +
                                               "WHERE UserId = ?", i);
            return output;
        }

        public List<Lists> GetListId(int userId, string listName)
        {
            var output = database.Query<Lists>("SELECT ListId " +
                                               "FROM Lists " +
                                               "WHERE UserId = ? AND ListName = ?", userId, listName);
            return output;
        }
        
        public Lists GetLists()
        {
            lock (locker)
            {
                if (database.Table<Lists>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Lists>().First();
                }
            }    
        }

        public int SaveLists(Lists lists)
        {
            lock (locker)
            {
                if (lists.ListId != 0)
                {
                    database.Update(lists);
                    return lists.ListId;
                }
                else
                {
                    return database.Insert(lists);
                }
            }
        }

        public int DeleteLists(int id)
        {
            lock (locker)
            {
                return database.Delete<Lists>(id);
            }
        }

        public int DeleteListsData()
        {
            SQLiteCommand comm = database.CreateCommand("DELETE from Lists");

            lock (locker)
            {
                return comm.ExecuteNonQuery();
            }
        }
    }
}
