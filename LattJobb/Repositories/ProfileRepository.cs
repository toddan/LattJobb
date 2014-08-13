using Gottknark.DAL;
using Gottknark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gottknark.Repositories
{
    public class ProfileRepository : GenericRepository<UserProfile>
    {
        public ProfileRepository(GottKnarkContext context) : base(context) { }

        public void SetProfile(int userid, Profile profile)
        {
            var user = GetWhere(u => u.UserId == userid).FirstOrDefault();
            user.Profile = profile;
        }

        public void AddUserRating(int userid, UserRating userrating)
        {
            var user = GetWhere(u => u.UserId == userid).FirstOrDefault();
            user.UserRatings.Add(userrating);
        }
    }
}