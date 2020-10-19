using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataAccess
{
    public class DataItemAccess : DataAccessClass<DataItem>
    {
        public DataItemAccess(string table, string database) : base(table, database)
        {

        }

    }
}
