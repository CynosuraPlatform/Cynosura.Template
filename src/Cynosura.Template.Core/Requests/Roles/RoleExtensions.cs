using System;
using System.Linq;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.Roles
{
    public static class RoleExtensions
    {
        public static IOrderedQueryable<Role> OrderBy(this IQueryable<Role> queryable, string propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }
    }
}
