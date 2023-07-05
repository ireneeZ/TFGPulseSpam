using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Model
{
    public class UsuarioLogin
    {
        [Required, EmailAddress]
        public String Email { get; set; }

        [Required, DataType(DataType.Password)]
        public String Password { get; set; }

    }
}
