using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BussinessLogic
{
    public class LoginLogic : DataAccessClass<PersonModel>
    {
        public LoginLogic(string conString, string table, string database) : base(conString,table,database)
        {

        }

        //login sucsesfull, the login principal , error message
        public async Task<(bool, ClaimsPrincipal, string)> TryLogin(string email, string pass)
        {
            (bool, ClaimsPrincipal, string) temp = (false, null, "none");

            //TODO: ENCRYPT PASSWORD HERE
            var data = await collection.FindAsync(new BsonDocument(new List<BsonElement>()
            {
                new BsonElement("Email",email),
                new BsonElement("PasswordHash",pass),
            }
            ));
            if (data==null)
            {
                temp.Item3 = "Could not find your account, Please make sure you entered the right details.";
                return temp;
            }
            var result = data.First();

      
            try
            {
                var princ = GetPrinciple(result);
                temp.Item1 = true;
                temp.Item2 = princ;
            }
            catch
            {
                temp.Item3 = "Fatal error logging in. Please provide valid details";
            }


            return temp;
        }

        private ClaimsPrincipal GetPrinciple(PersonModel _model)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _model.Name),
                new Claim(ClaimTypes.Role, _model.Role),
                new Claim(ClaimTypes.Email, _model.Email)
            };

            var identity = new ClaimsIdentity(claims, "Claim");

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public async Task<PersonModel> GetUserByEmail(string email)
        {
            var data = await collection.FindAsync(new BsonDocument(new List<BsonElement>()
            {
                new BsonElement("Email",email),
            }
            ));
            var result = data.First();

            return result;
        }

        public void UpdatePersonEmailName(ObjectId id,string name,string email)
        {
            var filter = new BsonDocument("_id", id);
            var update = Builders<PersonModel>.Update.Set("Name", name).Set("Email",email);
            collection.UpdateOne(filter, update);
        }

        public void UpdatePersonEmailNameRole(ObjectId id, string name, string email,string role)
        {
            var filter = new BsonDocument("_id", id);
            var update = Builders<PersonModel>.Update.Set("Name", name).Set("Email", email).Set("Role", role);
            collection.UpdateOne(filter, update);
        }

        private async Task<bool> CheckIfEmailExists(string email)
        {
            var data = await collection.FindAsync(new BsonDocument(new List<BsonElement>()
            {
                new BsonElement("Email",email),
            }
            ));

            if (data !=null) return true; else return false; // PS . i hate doing if statements like this, but im getting lazy
        }

        public override async Task<bool> InsertRecord(PersonModel record)
        {
            //check if email exists.....
            bool emailExist = await CheckIfEmailExists(record.Email);
            if (!emailExist)
            {
                return await base.InsertRecord(record);
            }
            return false;
        }
    }
}
