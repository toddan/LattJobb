using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gottknark.Models
{
    public class UserRating
    {
        public int UserRatingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public int? UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int CommenterId { get; set; }
        public virtual UserProfile Commenter { get; set; }

    }
}