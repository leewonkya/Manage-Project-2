using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Interfaces
{
    public partial interface IDataContext : IDisposable
    {
        DbSet<Guest> Guests { get; set; }

        DbSet<Permission> Permissions { get; set; }

        DbSet<Project> Projects { get; set; }

        DbSet<Report> Reports { get; set; }

        DbSet<Tag> Tags { get; set; }

        DbSet<Time_start> Time_Starts { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Rollback();

        DbEntityEntry Entry(object entity);
    }
}
