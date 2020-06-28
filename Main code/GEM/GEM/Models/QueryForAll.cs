using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GEM.Models
{
    public class QueryForAll
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string FilePath { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string BarCode { get; set; }
        public int ListId { get; set; }
        public string Amount { get; set; }
        public double Price { get; set; }
        public string ListName { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime ExpDate { get; set; }

        public QueryForAll() { }
    }
}
