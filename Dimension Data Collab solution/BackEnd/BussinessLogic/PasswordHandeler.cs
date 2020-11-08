using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BussinessLogic
{
    public class PasswordHandeler
    {
        public static string EncryptPassword(string pass)
        {
            byte[] encData_byte = new byte[pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(pass);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}
