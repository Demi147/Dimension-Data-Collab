using BackEnd.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dimension_Data_Collab.MiddleWare
{
    public static class LoginHelperClass
    {
        

        public async static Task<bool> CheckLogin(PersonModel _person)
        {
            return true;
        } 
    }
}
