using Project2.Core.Data.Context;
using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Services;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;

namespace Project2.Core.loc
{
    /// <summary>
    ///     Bind the given interface in request scope
    /// </summary>
    public static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }

    public static class UnityHelper
    {
        public static IUnityContainer Container;

        public static void InitialiseUnityContainer()
        {
            Container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
            Container.BindInRequestScope<ICacheService, CacheService>();
            Container.BindInRequestScope<IConfigService, ConfigService>();

            //Container.Binh<IConfigService, ConfigService>();
            //Container.BindInRequestScope<ICacheService, CacheService>();
        }

        /// <summary>
        ///     Inject
        /// </summary>
        /// <returns></returns>
        public static void BuildUnityContainer()
        {
            Container.BindInRequestScope<IDataContext, DataContext>();
            Container.BindInRequestScope<IGuestService, GuestService>();
            Container.BindInRequestScope<IPermissionService, PermissionService>();
            Container.BindInRequestScope<ITagService, TagService>();
            Container.BindInRequestScope<IProjectService, ProjectService>();
            Container.BindInRequestScope<IReportService, ReportService>();
            Container.BindInRequestScope<ITimeStartService, TimeStartService>();
        }
    }
}
