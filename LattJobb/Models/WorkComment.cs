using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gottknark.Models
{
    public class WorkComment
    {
        public int WorkCommentId { get; set; }
        [Required]
        public string Comment { get; set; }
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }

        public int? UserId {get; set;}
        public virtual UserProfile User { get; set; }
    }
}