using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEM.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
