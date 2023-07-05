using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace PulseSpamApi.Models
{
    public class UsuarioLogin
    {
        [Required, EmailAddress]
        public String Email { get; set; }

        [Required, DataType(DataType.Password)]
        public String Password { get; set; }

        public bool IsAdmin { get; set; } = false;

    }
}
