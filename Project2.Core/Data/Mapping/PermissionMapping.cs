using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class PermissionMapping : EntityTypeConfiguration<Permission>
    {
        public PermissionMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).IsRequired();

            HasMany(x => x.Guests)
                .WithRequired(x => x.Permission)
                .Map(x => x.MapKey("PermissionId"))
                .WillCascadeOnDelete(false);
        }
    }
}
