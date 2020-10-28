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
            var result = data.First();


            try
            {
                if (result != null)
                {
                    //generate principal here?
                    var princ = GetPrinciple(result);
                    temp.Item1 = true;
                    temp.Item2 = princ;
                }
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
    }
}
