﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cynosura.Template.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public event EventHandler SavingChanges;

        public DataContext(DbContextOptions<DataContext> options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public override int SaveChanges()
        {
            OnSavingChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnSavingChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            builder.ApplyAllConfigurations(assemblies);
        }

        protected virtual void OnSavingChanges()
        {
            SavingChanges?.Invoke(this, EventArgs.Empty);
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => SaveChangesAsync();
    }
}
