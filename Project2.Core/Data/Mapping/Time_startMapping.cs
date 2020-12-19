using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class Time_startMapping : EntityTypeConfiguration<Time_start>
    {
        public Time_startMapping()
        {
            HasKey(x => x.id);
            Property(x => x.id).IsRequired();
            Property(x => x.name).IsRequired();
            Property(x => x.start_at).IsRequired();
            Property(x => x.end_at).IsOptional();
            Property(x => x.numberProject).IsRequired();
            Property(x => x.numberProjectInTeacher).IsRequired();
            Property(x => x.isFinish).IsOptional();

            HasMany(x => x.Projects)
                .WithOptional(x => x.time_Start)
                .Map(x => x.MapKey("TimeStartId"))
                .WillCascadeOnDelete(true);
        }
    }
}
