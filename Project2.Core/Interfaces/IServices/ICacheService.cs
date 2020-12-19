using Project2.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Interfaces.IServices
{
    public interface ICacheService
    {
        T Get<T>(string key);

        void Set(string key, object data, CacheTimes minutesToCache);

        bool IsSet(string key);

        void Invalidate(string key);

        void ClearStartWith(string keyStartsWith);

        void ClearStartWith(List<string> keysStartsWith);
    }
}
