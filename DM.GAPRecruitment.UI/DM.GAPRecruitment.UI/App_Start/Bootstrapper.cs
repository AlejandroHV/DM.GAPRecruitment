using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using System.Data.Entity;


namespace DM.GAPRecruitment.UI.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            //var builder = new ContainerBuilder();
            ////builder.RegisterControllers(typeof(HomeController).Assembly);
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType(typeof(SuperZapatosContext)).As(typeof(IContext)).InstancePerLifetimeScope();
            //builder.RegisterAssemblyTypes(typeof(ArticleRepository).Assembly)
            //   .Where(t => t.Name.EndsWith("Repository"))
            //   .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(ArticleService).Assembly)
            //  .Where(t => t.Name.EndsWith("Service"))
            //  .AsImplementedInterfaces().InstancePerRequest();



            //builder.RegisterFilterProvider();
            //IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}