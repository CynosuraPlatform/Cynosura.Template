using System;
using System.Linq;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Roles.Models;

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

        public static IQueryable<Role> Filter(this IQueryable<Role> queryable, RoleFilter filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Name));
            }
            return queryable;
        }
    }
}
