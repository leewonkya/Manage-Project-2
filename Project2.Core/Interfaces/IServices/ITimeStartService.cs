using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Core.Models.Entities;
namespace Project2.Core.Interfaces.IServices
{
    public interface ITimeStartService
    {
        List<Time_start> getTimeStart();

        Time_start getTime();
    }
}
