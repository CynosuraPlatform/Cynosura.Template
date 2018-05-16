using System;
using System.Linq;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Data
{
    public class BaseEntityRepository<T> : EntityRepository<T> where T : BaseEntity
    {
        private readonly IUserContext _userContext;

        protected int? UserId => _userContext.GetUserAsync().Result?.Id;

        public BaseEntityRepository(IDatabaseFactory databaseFactory, IUserContext userContext)
            : base(databaseFactory)
        {
            _userContext = userContext;

            ((DataContext)Context).SavingChanges += OnSavingChanges;
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            var entities = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                .Where(e => e.Entity is T)
                .Select(e => new
                {
                    Entity = (T)e.Entity,
                    State = e.State
                })
                .ToList();

            if (entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    entity.Entity.ModificationDate = DateTime.UtcNow;
                    entity.Entity.ModificationUserId = UserId;

                    if (entity.State == EntityState.Added)
                    {
                        entity.Entity.CreationDate = entity.Entity.ModificationDate;
                        entity.Entity.CreationUserId = entity.Entity.ModificationUserId;
                    }
                }
            }
        }
    }
}
