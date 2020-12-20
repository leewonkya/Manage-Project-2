using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public ProjectMapping()
        {
            HasKey(x => x.id);
            Property(x => x.id).IsRequired();
            Property(x => x.name).IsOptional();
            Property(x => x.isActive).IsOptional();
            Property(x => x.require).IsOptional();

            HasMany(x => x.Tags)
                .WithMany(x => x.Projects)
                .Map(x =>
                {
                    x.MapLeftKey("ProjectId");
                    x.MapRightKey("TagId");
                    x.ToTable("ProjectTag");
                });

            HasMany(x => x.Reports)
                .WithOptional(x => x.Project)
                .Map(x => x.MapKey("ProjectId"))
                .WillCascadeOnDelete(true);
        }
    }
}
