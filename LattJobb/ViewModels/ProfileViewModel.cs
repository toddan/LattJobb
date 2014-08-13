using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gottknark.Models;
using System.Web.Mvc;

namespace Gottknark.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public UserRating UserRating { get; set; }

        public List<SelectListItem> RatingOptions
        {
            get
            {
                var options = new List<SelectListItem>
                {
                    new SelectListItem{Value = "1", Text = "1"},
                    new SelectListItem{Value = "2", Text = "2"},
                    new SelectListItem{Value = "3", Text = "3"},
                    new SelectListItem{Value = "4", Text = "4"},
                    new SelectListItem{Value = "5", Text = "5"},
                };
                return options;
            }
        }
    }
}