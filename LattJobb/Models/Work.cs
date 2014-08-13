using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gottknark.Models
{
    public class Work
    {
        public Work()
        {
            Comments = new List<WorkComment>();
        }
        public int WorkId { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public string About { get; set; }
        [Required]
        [Display(Name = "Pris")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Region")]
        public string Region { get; set; }

        public string Image { get; set; }

        public string Tags { get; set; }

        public bool Closed { get; set; }

        public virtual List<WorkComment> Comments { get; set; }

        public virtual List<Offer> Offers { get; set; }

        public int UserId { get; set; }
        [Display(Name = "Användare")]
        public virtual UserProfile User { get; set; }
    }
}