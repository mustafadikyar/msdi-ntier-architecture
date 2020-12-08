using Autofac;
using Castle.DynamicProxy;
using Msdi.Business.Abstract;
using Msdi.Business.Concrete.Managers;
using Msdi.DataAccess.Abstract;
using Msdi.DataAccess.Concrete;
using Msdi.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            //var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            //{
            //    Selector = new AspectInterceptorSelector()
            //}).SingleInstance();
        }
    }
}
