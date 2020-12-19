using Project2.Core.Interfaces.IServices;
using Project2.Core.loc;
using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core
{
    public class Project2Configuration
    {
        private string _originVersion;

        public string OriginVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_originVersion))
                {
                    _originVersion = GetConfig("project2Version");
                }
                return _originVersion;
            }
        }

        // Database Connection Key
        public string OriginContext => GetConfig("DataContext");

        #region Singleton

        private static Project2Configuration _instance;
        private static readonly object InstanceLock = new object();
        private static IConfigService _configService;

        private Project2Configuration(IConfigService configService)
        {
            _configService = configService;
        }

        public static Project2Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            var configService = UnityHelper.Container.Resolve<IConfigService>();
                            _instance = new Project2Configuration(configService);
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Generic Get
        public string GetConfig(string key)
        {
            var dict = _configService.GetConfig();
            if (!string.IsNullOrWhiteSpace(key) && dict.ContainsKey(key))
            {
                return dict[key];
            }
            return string.Empty;
        }
        #endregion
    }
}
