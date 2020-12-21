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
    public class GuestService : IGuestService
    {
        private IDataContext context;

        public GuestService(IDataContext context)
        {
            this.context = context;
        }

        public Guest GetGuest()
        {
            return context.Guests.Take(1).SingleOrDefault();
        }

        public Guest GetGuestByIdPermission(int id)
        {
            return context.Guests.Include(x => x.Permission).Where(x => x.Permission.Id == id).SingleOrDefault();
        }

        public Guest GetGuestById(int id)
        {
            return context.Guests.Include(x => x.Permission).Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<Guest> getListGuests()
        {
            return context.Guests.OrderBy(x => x.Permission.Id).ToList();
        }

        public int getPermissionIdByUsernameAndPassword(string username, string password)
        {
            return context.Guests.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).Select(x => x.Permission.Id).SingleOrDefault();
        }

        public int getGuestIdByUsernameAndPassword(string username, string password)
        {
            return context.Guests.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).Select(x => x.Id).SingleOrDefault();
        }

        public List<Guest> getListGuestByIdPermission(int id)
        {
            return context.Guests.Include(x => x.Permission).Where(x => x.Permission.Id == id).ToList();
        }

    }
}