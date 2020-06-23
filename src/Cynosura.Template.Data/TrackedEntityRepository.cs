﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Enums;
using Cynosura.Template.Core.Security;

namespace Cynosura.Template.Data
{
    public class TrackedEntityRepository<T> : BaseEntityRepository<T> where T : TrackedEntity
    {
        public TrackedEntityRepository(IDatabaseFactory databaseFactory, IUserInfoProvider userInfoProvider) 
            : base(databaseFactory, userInfoProvider)
        {
        }
        
        protected override void TrackChange(BaseEntity entity, ChangeAction action)
        {
            var entityType = entity.GetType();
            var entry = Context.Entry(entity);
            var from = entry.State != EntityState.Added ?
                JsonSerializer.Serialize(entry.OriginalValues.ToObject()) :
                "";
            var to = entry.State != EntityState.Deleted ?
                JsonSerializer.Serialize(entry.CurrentValues.ToObject()) :
                "";
            var change = new EntityChange()
            {
                EntityName = entityType.Name,
                EntityId = entity.Id,
                Action = action,
                From = from,
                To = to,
                CreationDate = DateTime.UtcNow,
                CreationUserId = UserId,
            };
            Context.Set<EntityChange>().Add(change);
        }
    }
}
