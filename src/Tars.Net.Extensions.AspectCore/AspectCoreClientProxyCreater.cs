﻿using AspectCore.DynamicProxy;
using System;
using Tars.Net.Clients;

namespace Tars.Net.Extensions.AspectCore
{
    public class AspectCoreClientProxyCreater : IClientProxyCreater
    {
        private readonly IProxyTypeGenerator generator;
        private readonly IAspectContextFactory contextFactory;
        private readonly ClientProxyAspectBuilderFactory aspectBuilderFactory;

        public AspectCoreClientProxyCreater(IProxyTypeGenerator generator, IAspectContextFactory contextFactory, ClientProxyAspectBuilderFactory aspectBuilderFactory)
        {
            this.generator = generator;
            this.contextFactory = contextFactory;
            this.aspectBuilderFactory = aspectBuilderFactory;
        }

        public object Create(Type type)
        {
            var proxyType = generator.CreateInterfaceProxyType(type);
            return Activator.CreateInstance(proxyType, new AspectActivatorFactory(contextFactory, aspectBuilderFactory));
        }
    }
}