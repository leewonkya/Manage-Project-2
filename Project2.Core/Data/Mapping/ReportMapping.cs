﻿using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Data.Mapping
{
    public class ReportMapping : EntityTypeConfiguration<Report>
    {
        public ReportMapping()
        {
            HasKey(x => x.id);
            Property(x => x.id).IsRequired();
            Property(x => x.title).IsRequired();
            Property(x => x.content).IsRequired();
            Property(x => x.files).IsOptional();
            Property(x => x.create_at).IsRequired();
        }
    }
}