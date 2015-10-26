using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using WeChatService.Library.Services;
using WeChatService.Service.Services;
using Unity.Mvc5;

namespace WeChatService.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);

            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
           
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IProvinceService, ProvinceService>();
        }
    }
}