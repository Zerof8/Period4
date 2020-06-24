using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GEM.Models
{
    public class QueryForAll
    {
        public string ProductName { get; set; }
        public string FilePath { get; set; }
        public int Quantity { get; set; }
        public string BarCode { get; set; }
        public int ListId { get; set; }

        public QueryForAll() { }
    }
}
