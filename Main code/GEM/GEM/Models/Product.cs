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
        public string category { get; set; }
        public int amount { get; set; }
        public string calculatedExpDate { get; set; }

        public Product() { }
        public Product(string barCode, string category, int amount, string calculatedExpDate)
        {
            this.barCode = barCode;
            this.category = category;
            this.amount = amount;
            this.calculatedExpDate = calculatedExpDate;
        }
    }
}
