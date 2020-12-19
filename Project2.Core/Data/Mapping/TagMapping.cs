using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class TagMapping : EntityTypeConfiguration<Tag>
    {
        public TagMapping()
        {
            HasKey(x => x.id);
            Property(x => x.id).IsRequired();
            Property(x => x.name).IsRequired();
        }
    }
}
