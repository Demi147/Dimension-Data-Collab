using BackEnd.CustomStuff;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class PersonModel
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MinLength(4)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Email is required")]
        [MinLength(6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }


        public string Role { get; set; } = "user";

        public bool Validated { get; set; } = false;
    }
}
