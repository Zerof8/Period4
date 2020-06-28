using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GEM.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string barCode { get; set; }
        public string productName { get; set; }
        public string category { get; set; }
        public string amount { get; set; }

        public Product() { }
        public Product(string barCode, string productName, string category, string amount)
        {
            this.barCode = barCode;
            this.productName = productName;
            this.category = category;
            this.amount = amount;
        }

        public Product(string productName, string category, string amount)
        {
            this.productName = productName;
            this.category = category;
            this.amount = amount;
        }
    }
}
