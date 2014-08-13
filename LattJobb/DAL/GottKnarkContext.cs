using Gottknark.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Gottknark.DAL
{
    public class GottKnarkContext : DbContext
    {
        public GottKnarkContext() : base("defaultconnection") { }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkComment> Comments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Profile> AccountProfiles { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }

    }
}