﻿using Autofac;

namespace HashtagAggregator.DependencyInjection
{
    public class EFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<SqlApplicationDbContext>().As<IApplicationContext>();
        }
    }
}