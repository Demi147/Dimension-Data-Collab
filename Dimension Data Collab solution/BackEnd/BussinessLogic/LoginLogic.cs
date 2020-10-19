using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;

namespace BackEnd.BussinessLogic
{
    public static class LoginLogic
    {
        public static void RegisterUser(string name, string email, string password)
        {
            DataAccessClass<PersonModel> dataAccess = new LoginDataAccess(SettingsHolder.LoginCollectionName,SettingsHolder.LoginDataBaseName);

            //create new user
            var user = new PersonModel
            {
                Name = name,
                Email = email,
                PasswordHash = password,
                Role = "user"
            };
            //set role to user
            //ask data access to add the user to data base

            dataAccess.InsertRecord(user);
        }

        public static async Task<List<PersonModel>> GetAllUsers()
        {
            DataAccessClass<PersonModel> dataAccess = new LoginDataAccess(SettingsHolder.LoginCollectionName, SettingsHolder.LoginDataBaseName);

            return await dataAccess.GetAllRecords(1, 10);
        }

        public static async Task<(bool,ObjectId)> TryLogin(string email, string PasswordHash)
        {
            var dataAccess = new LoginDataAccess(SettingsHolder.LoginCollectionName, SettingsHolder.LoginDataBaseName);
            var userExists = await dataAccess.CheckUserExists(email,PasswordHash);
            return userExists;
        }

        public static async Task<ClaimsPrincipal> GetPrinciple(ObjectId _id)
        {
            var dataAccess = new LoginDataAccess(SettingsHolder.LoginCollectionName, SettingsHolder.LoginDataBaseName);
            //get the full user object
            var user = await dataAccess.GetRecordById(_id);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var identity = new ClaimsIdentity(claims, "Claim");

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
