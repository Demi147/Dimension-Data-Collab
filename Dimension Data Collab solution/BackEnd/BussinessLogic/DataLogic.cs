using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BussinessLogic
{
    public class DataLogic : DataAccessClass<DataItem>
    {
        public DataLogic(string conString, string table, string database) :base(conString, table, database)
        {

        }
    }
}
