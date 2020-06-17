using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;

namespace GEM.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string FilePath { get; set; }

        public Category() { }
        public Category(string CategoryName, string FilePath)
        {
            this.CategoryName = CategoryName;
            this.FilePath = FilePath;
        }
    }
}
