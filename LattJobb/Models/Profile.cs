using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Gottknark.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Info { get; set; }

        public string Image { get; set; }

        // This should be refactored into a new table with relations n stuff, men PALLA!
        public string Tags { get; set; }

        public int NumerOfResults { get; set; }
    }
}