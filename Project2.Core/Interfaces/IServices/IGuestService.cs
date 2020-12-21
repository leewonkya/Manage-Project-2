using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Interfaces.IServices
{
    public interface IGuestService
    {
        Guest GetGuest();

        Guest GetGuestById(int id);

        Guest GetGuestByIdPermission(int id);
        
        int getGuestIdByUsernameAndPassword(string username, string password);

        int getPermissionIdByUsernameAndPassword(string username, string password);

        IEnumerable<Guest> getListGuests();

        List<Guest> getListGuestByIdPermission(int id);
    }
}
