using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using CmsSystem.Data;
using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using CmsSystem.Service;
using CmsSystem.Web.Models;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace CmsSystem.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            ConfigAutoFac();
            ConfigAutoMapper();
        }

        private static void ConfigAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<UserSystemDbContext>().AsSelf().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void ConfigAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ActionRole, ActionRoleVm>().MaxDepth(2);
                cfg.CreateMap<Action, ActionVm>().MaxDepth(2);
                cfg.CreateMap<FunctionRole, FunctionRoleVm>().MaxDepth(2);
                cfg.CreateMap<Function, FunctionVm>().MaxDepth(2);
                cfg.CreateMap<ProductCategory, ProductCategoryVm>().MaxDepth(2);
                cfg.CreateMap<Product, ProductVm>().MaxDepth(2);
                cfg.CreateMap<RoleUser, RoleUserVm>().MaxDepth(2);
                cfg.CreateMap<Role, RoleVm>().MaxDepth(2);
                cfg.CreateMap<User, UserVm>().MaxDepth(2);
            });
        }
    }
}