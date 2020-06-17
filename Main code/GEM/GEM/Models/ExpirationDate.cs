using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;

namespace GEM.Models
{
    public class ExpirationDate
    {
        [PrimaryKey, AutoIncrement]
        public int ExpDateId { get; set; }
        public string PrBarCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpDate { get; set; }

        public ExpirationDate() { }
        public ExpirationDate(string PrBarCode, DateTime StartDate, DateTime ExpDate)
        {
            this.PrBarCode = PrBarCode;
            this.StartDate = StartDate;
            this.ExpDate = ExpDate;
        }
    }
}
