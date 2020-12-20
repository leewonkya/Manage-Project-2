using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Services
{
    public class TagService : ITagService
    {
        private IDataContext context;

        public TagService(IDataContext context)
        {
            this.context = context;
        }

        public List<Tag> GetTags(int id)
        {
            return context.Tags.Where(x => x.id == id).ToList();
        }

        public List<Tag> GetTags()
        {
            return context.Tags.ToList();
        }
    }
}
