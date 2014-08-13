using Gottknark.DAL;
using Gottknark.Models;
using Gottknark.Repositories;
using Gottknark.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebMatrix.WebData;
using System.Web.Security;

namespace Gottknark.Controllers
{
    [Authorize]
    public class WorksController : Controller
    {
        private WorkRepository workRepository;
        private ProfileRepository profileRepository;

        private UnitOfWork unitOfWork;

        public WorksController()
        {
            unitOfWork = new UnitOfWork();
            workRepository = new WorkRepository(unitOfWork.Context);
            profileRepository = new ProfileRepository(unitOfWork.Context);
        }

        public ActionResult Index(int? page)
        {
            int pagesize = 1;
            int pagenumber;

            var works = workRepository.GetAll().Reverse();
            var user = profileRepository.GetByID(WebSecurity.CurrentUserId);
            if (user.Profile != null)
            {
                    pagesize = user.Profile.NumerOfResults;
                    if (pagesize == 0)
                        pagesize = 10;
            }

            pagenumber = (page ?? 1);
            return View(works.ToPagedList(pagenumber, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string tag, int? page)
        {
            int pagesize = 10;
            int pagenumber = (page ?? 1);

            var user = profileRepository.GetByID(WebSecurity.CurrentUserId);

            if(user.Profile != null)
            {
                pagesize = user.Profile.NumerOfResults;
            }

            if(string.IsNullOrEmpty(tag))
            {
                var works = workRepository.GetAll().Reverse();
                return View(works.ToPagedList(pagenumber, pagesize));
            }

            var workswithtags = workRepository.GetWhere(x => x.Tags.Contains(tag));
            
            return View(workswithtags.ToPagedList(pagenumber, pagesize));
        }

        public ActionResult Details(int id)
        {
            var work = workRepository.GetByID(id);

            if(work == null)
            {
                return HttpNotFound();
            }

            var workviewmodel = new WorkViewModel
            {
                Work = work,
                WorkComment = new WorkComment() { WorkId = work.WorkId},
                Offer = new Offer() { WorkId = work.WorkId}
            };

            workviewmodel.Work.Offers.Reverse();
            workviewmodel.Work.Comments.Reverse();

            return View(workviewmodel);
        }

        [Authorize(Roles="Employer")]
        public ActionResult Create()
        {
            return View(new Work());
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public ActionResult Create(Work work, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    image.SaveAs(HttpContext.Server.MapPath("~/Content/workimages/") + image.FileName);
                    work.Image = "/Content/workimages/" + image.FileName;
                }
                work.Closed = false;
                work.UserId = WebSecurity.CurrentUserId;
                workRepository.Insert(work);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(work);
        }

        [HttpPost]
        public ActionResult AddComment(WorkViewModel workviewmodel)
        {
            var work = workRepository.GetByID(workviewmodel.WorkComment.WorkId);
            if (ModelState.IsValid)
            {
                workviewmodel.WorkComment.UserId = WebSecurity.CurrentUserId;
                workRepository.InsertWorkComment(workviewmodel.WorkComment);
                unitOfWork.Save();
                return RedirectToAction("Details", new { id = workviewmodel.WorkComment.WorkId });
            }
            workviewmodel.Work = work;
            return View("~/Views/Works/Details.cshtml", workviewmodel);
        }

        [HttpPost]
        public ActionResult MakeOffer(WorkViewModel workviewmodel)
        {
            var work = workRepository.GetByID(workviewmodel.Offer.WorkId);
            if(ModelState.IsValid)
            {
                workviewmodel.Offer.UserId = WebSecurity.CurrentUserId;
                workRepository.AddWorkOffer(workviewmodel.Offer);
                unitOfWork.Save();
                return RedirectToAction("Details", new { id = workviewmodel.Offer.WorkId });
            }
            workviewmodel.Work = work;
            return View("~/Views/Works/Details.cshtml",workviewmodel);
        }

        [HttpPost]
        public ActionResult CloseWork(int id)
        {
            var work = workRepository.GetByID(id);
            if(WebSecurity.CurrentUserId != work.UserId)
            {
                return RedirectToAction("Index");
            }

            work.Closed = true;
            workRepository.Update(work);
            unitOfWork.Save();

            return RedirectToAction("Details", new { id = work.WorkId });
        }
    }
}
