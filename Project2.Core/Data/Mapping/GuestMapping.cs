using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class GuestMapping : EntityTypeConfiguration<Guest>
    {
        public GuestMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Username).IsRequired();
            Property(x => x.Password).IsRequired();
            Property(x => x.Full_name).IsRequired();
            Property(x => x.Path).IsOptional();
            Property(x => x.Birthday).IsOptional();
            Property(x => x.Email).IsOptional();

            HasMany(x => x.ProjectsStudent)
                .WithOptional(x => x.GuestStudent)
                .Map(x => x.MapKey("StudentId"));

            HasMany(x => x.ProjectsTeacher)
                .WithOptional(x => x.GuestTeacher)
                .Map(x => x.MapKey("TeacherId"));     
        }
    }
}
