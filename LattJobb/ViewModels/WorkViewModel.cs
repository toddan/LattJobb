using Gottknark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gottknark.ViewModels
{
    public class WorkViewModel
    {
        private Work work;
        public Work Work 
        {
            get
            {
                work.Comments.Reverse();
                return work;
            }
            set
            {
                work = value;
            }
        }

        public WorkComment WorkComment { get; set; }

        public Offer Offer { get; set; }
    }
}