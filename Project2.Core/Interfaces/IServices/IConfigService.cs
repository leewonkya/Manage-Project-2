using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Interfaces.IServices
{
    public partial interface IConfigService
    {
        Dictionary<string, string> GetConfig();
    }
}
