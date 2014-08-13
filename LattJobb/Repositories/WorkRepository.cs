using Gottknark.DAL;
using Gottknark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gottknark.Repositories
{
    public class WorkRepository : GenericRepository<Work>
    {
        public WorkRepository(GottKnarkContext context) : base(context) { }

        public void InsertWorkComment(WorkComment workcomment)
        {
            var work = GetWhere(x => x.WorkId == workcomment.WorkId).FirstOrDefault();
            work.Comments.Add(workcomment);
        }

        public IEnumerable<Work> FindByTag(string tag)
        {
            return GetWhere(x => x.Tags.Contains(tag)).ToList();
        }

        public void AddWorkOffer(Offer offer)
        {
            var work = GetWhere(x => x.WorkId == offer.WorkId).FirstOrDefault();
            work.Offers.Add(offer);
        }
    }
}