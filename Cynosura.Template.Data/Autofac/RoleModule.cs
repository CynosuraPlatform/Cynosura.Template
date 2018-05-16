﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Cynosura.Core.Data;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Autofac
{
    public class RoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EntityRepository<Role>>().As<IEntityRepository<Role>>();
        }
    }
}
