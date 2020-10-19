using BackEnd.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DataAccess
{
    public class LoginDataAccess : DataAccessClass<PersonModel>
    {
        public LoginDataAccess(string table, string database) : base(table, database)
        {

        }

        public async Task<(bool, ObjectId)> CheckUserExists(string email, string PasswordHash)
        {
            (bool, ObjectId) temp = (false, new ObjectId());

            var data = await collection.FindAsync(new BsonDocument(new List<BsonElement>()
            {
                new BsonElement("Email",email),
                new BsonElement("PasswordHash",PasswordHash),
            }
            ));

            try
            {
                var res = data.First();
                if (res != null)
                {
                    temp.Item1 = true;
                    temp.Item2 = res._id;
                    return temp;
                }
                else
                {
                    return temp;
                }
            }
            catch (Exception)
            {
                return temp;
            }
        }
    }
}
