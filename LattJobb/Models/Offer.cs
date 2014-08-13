using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gottknark.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Display(Name = "Bud")]
        [Required]
        public int Price { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "Användare")]
        public virtual UserProfile UserProfile { get; set; }

        public int WorkId { get; set; }
        public virtual Work Work { get; set; }
    }
}