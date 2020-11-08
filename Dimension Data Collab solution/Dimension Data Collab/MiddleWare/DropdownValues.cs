using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dimension_Data_Collab.MiddleWare
{
    public class DropdownValues
    {
        public static List<string> Genders { get; } = new List<string> {"Male","Female"};
        public static List<string> BusinessTravel { get; } = new List<string> { "Travel_Frequently", "Travel_Rarely", "Non-Travel" };
        public static List<string> Department { get; } = new List<string> { "Research & Development", "Sales" , "Human Resources" };
        public static List<string> EducationField { get; } = new List<string> { "Life Sciences", "Medical", "Marketing", "Technical Degree", "Human Resources", "Other" };
        public static List<string> MaritalStatus { get; } = new List<string> { "Married", "Single", "Divorced"};
    }
}
