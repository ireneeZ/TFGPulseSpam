using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace PulseSpamApi.Models
{
    public class UsuarioRegistro
    {
        [Required, EmailAddress]
        public String Email { get; set; }

        public String UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }
}
