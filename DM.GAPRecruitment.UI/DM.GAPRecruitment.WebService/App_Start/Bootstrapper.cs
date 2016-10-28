using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Mvc;
using System.Data.Entity;
using DM.GAPRecruitment.DAL.Repositories;
using DM.GAPRecruitment.DAL.Context;
using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.BLL.Services;
using System.Web.Http;

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
            var builder = new ContainerBuilder();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType(typeof(SuperZapatosContext)).As(typeof(IContext)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(ArticleRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ArticleService).Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces().InstancePerRequest();


            
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}