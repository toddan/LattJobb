using Gottknark.DAL;
using Gottknark.Models;
using Gottknark.Repositories;
using Gottknark.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Gottknark.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private UnitOfWork unitOfWork;
        private ProfileRepository profileRepository;

        public ProfileController()
        {
            unitOfWork = new UnitOfWork();
            profileRepository = new ProfileRepository(unitOfWork.Context);
        }

        public ActionResult Index()
        {
            var userprofile = profileRepository.GetByID(WebSecurity.CurrentUserId);

            if (userprofile == null)
            {
                return HttpNotFound();
            }

            if(userprofile.Profile == null)
            {
                return RedirectToAction("Edit");
            }

            var profileviewmodel = new ProfileViewModel
            {
                UserProfile = userprofile,
                UserRating = new UserRating() { UserId = userprofile.UserId}
            };
            profileviewmodel.UserProfile.UserRatings.Reverse();
            return View(profileviewmodel);
        }

        public ActionResult Edit()
        {
            var userprofile = profileRepository.GetByID(WebSecurity.CurrentUserId);
            if (userprofile == null)
            {
                return HttpNotFound();
            }

            return View(userprofile.Profile);
        }

        [HttpPost]
        public ActionResult Edit(Profile profile, HttpPostedFileBase image)
        {
            if( ModelState.IsValid)
            {
                if(image != null)
                {
                    image.SaveAs(HttpContext.Server.MapPath("~/Content/profileimages/") + image.FileName);
                    profile.Image = "/Content/profileimages/" + image.FileName;
                }
                profileRepository.SetProfile(WebSecurity.CurrentUserId, profile);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        public ActionResult ShowProfile(int id)
        {
            var userprofile = profileRepository.GetByID(id);
            if (userprofile.Profile == null)
            {
                return HttpNotFound();
            }

            var profileviewmodel = new ProfileViewModel
            {
                UserProfile = userprofile,
                UserRating = new UserRating() { UserId = userprofile.UserId}
            };

            TempData["message"] = "test";

            profileviewmodel.UserProfile.UserRatings.Reverse();

            return View(profileviewmodel);
        }

        public ActionResult AddRating(ProfileViewModel profileviewmodel)
        {
            var commenter = profileRepository.GetByID(WebSecurity.CurrentUserId);
            if(ModelState.IsValid)
            {
                profileviewmodel.UserRating.Commenter = commenter;
                profileRepository.AddUserRating(profileviewmodel.UserRating.UserId.Value, profileviewmodel.UserRating);
                unitOfWork.Save();
                return RedirectToAction("ShowProfile", new { id = profileviewmodel.UserProfile.UserId });
            }
            return View(profileviewmodel);
        }

    }
}
