using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GEM.Models
{
    public class ProductList
    {
        [PrimaryKey, AutoIncrement]
        public int ProductListId { get; set; }
        public string BarCode { get; set; }
        public int ListId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime ExpDate { get; set; }


        public ProductList() { }
        public ProductList(string BarCode, int ListId, double Price, int Quantity, DateTime BuyDate, DateTime ExpDate)
        {
            this.BarCode = BarCode;
            this.ListId = ListId;
            this.Price = Price;
            this.Quantity = Quantity;
            this.BuyDate = BuyDate;
            this.ExpDate = ExpDate;
        }
    }
}
