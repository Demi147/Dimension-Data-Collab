using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace BackEnd.BussinessLogic
{
    public class DataLogic : DataAccessClass<DataItem>
    {
        public DataLogic(string conString, string table, string database) :base(conString, table, database)
        {

        }

        //Custom data logic

        public async Task<(List<int>, List<int>)> GetAgeCount()
        {
            var x = new List<int>();
            var y = new List<int>();
            var temp = (x,y);
            //get distinct ages
            var xdata = await collection.DistinctAsync(Q=>Q.Age, new BsonDocument());
            x = xdata.ToList();
            foreach (var item in x)
            {

            }
            return temp;
        }

        public async Task<(long, long)> GetGenderCount()
        {
            var x = await collection.CountDocumentsAsync(new BsonDocument("Gender","Male"));
            var y = await collection.CountDocumentsAsync(new BsonDocument("Gender", "Female"));

            var temp = (x, y);
            return temp;
        }
    }
}
