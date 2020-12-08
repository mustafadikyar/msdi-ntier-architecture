using Autofac;
using AutoMapper;
using Msdi.Business.Mappings.Automapper.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.DependencyResolvers.Autofac
{
    public class AutomapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<MapperConfiguration>(context =>
            {
                var profiles = context.Resolve<IEnumerable<Profile>>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new BusinessProfile());
                });
                return config;
            }).SingleInstance().AutoActivate().AsSelf();

            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                return config.CreateMapper();
            }).As<IMapper>();
        }
    }
}
