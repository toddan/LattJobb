using Gottknark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gottknark.DAL
{
    public class UnitOfWork
    {
        private GottKnarkContext context;

        public UnitOfWork()
        {
            context = new GottKnarkContext();
        }

        public GottKnarkContext Context
        {
            get
            {
                return context;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}