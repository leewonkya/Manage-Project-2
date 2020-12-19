using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project2.Core.Services
{
    using SiteConfig;

    public partial class ConfigService : IConfigService
    {
        private readonly ICacheService _cacheService;

        public ConfigService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        #region Settings

        public Dictionary<string, string> GetConfig()
        {
            const string key = "SiteConfig";
            var siteConfig = _cacheService.Get<Dictionary<string, string>>(key);
            if (siteConfig == null)
            {
                siteConfig = new Dictionary<string, string>();
                //var root = SiteConfig.Instance.GetSiteConfig();
                var root = SiteConfig.Instance.GetSiteConfig();
                var nodes = root?.SelectNodes("/project2/settings/setting");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        //<emoticon symbol="O:)" image="angel-emoticon.png" />  
                        if (node.Attributes != null)
                        {
                            var keyAttr = node.Attributes["key"];
                            var valueAttr = node.Attributes["value"];
                            if (keyAttr != null && valueAttr != null)
                            {
                                siteConfig.Add(keyAttr.InnerText, valueAttr.InnerText);
                            }
                        }
                    }

                    _cacheService.Set(key, siteConfig, CacheTimes.OneDay);
                }

            }
            return siteConfig;
        }

        #endregion
    }
}
