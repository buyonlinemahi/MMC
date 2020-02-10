using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MMCApp.Infrastructure.DependencyResolution;
using System.Web;

namespace MMC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        protected void Application_Start()
        {
            container = new UnityContainer().LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();
        }
        //protected void Application_End()
        //{
        //    container.Dispose();
        //} 
    }
}


