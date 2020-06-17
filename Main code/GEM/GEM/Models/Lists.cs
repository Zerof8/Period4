using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GEM.Models
{
    public class Lists
    {
        [PrimaryKey, AutoIncrement]
        public int ListId { get; set; }
        public int UserId { get; set; }
        public string ListName { get; set; }
        public string ListCategory { get; set; }

        public Lists() { }
        public Lists(int UserId, string ListName, string ListCategory)
        {
            this.UserId = UserId;
            this.ListName = ListName;
            this.ListCategory = ListCategory;
        }
    }
}
