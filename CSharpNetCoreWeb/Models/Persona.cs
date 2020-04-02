using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSharpNetCoreWeb.Models
{
    public class Persona
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Email obbligatoria")]
        public string Email { get; set; }

        public bool Active { get; set; }

        public List<Persona> list { get; set; }
    }
}
