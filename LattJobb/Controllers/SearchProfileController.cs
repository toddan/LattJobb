using Gottknark.DAL;
using Gottknark.Models;
using Gottknark.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gottknark.Controllers
{
    public class SearchProfileController : Controller
    {
        private UnitOfWork unitOfWork;
        private ProfileRepository profileRepository;

        public SearchProfileController()
        {
            unitOfWork = new UnitOfWork();
            profileRepository = new ProfileRepository(unitOfWork.Context);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserTable()
        {
            var users = Roles.GetUsersInRole("Employee");
            var profiles = profileRepository.GetWhere(u => users.Contains(u.UserName) && u.Profile != null);

            return PartialView(profiles);
        }

        [HttpPost]
        public ActionResult UserTable(string tag)
        {
            if(string.IsNullOrEmpty(tag))
            {
                var _users = Roles.GetUsersInRole("Employee");
                var _profiles = profileRepository.GetWhere(u => _users.Contains(u.UserName) && u.Profile != null);
                return PartialView(_profiles);
            }

            var users = Roles.GetUsersInRole("Employee");
            var profiles = profileRepository
                .GetWhere(u => users.Contains(u.UserName) 
                    && u.Profile.Tags.Contains(tag)
                    || u.UserName.Contains(tag)
                    && u.Profile != null).ToList();

            return PartialView(profiles);
        }

    }
}
