using System;
using System.Linq;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Core.Requests.Users
{
    public static class UserExtensions
    {
        public static IOrderedQueryable<User> OrderBy(this IQueryable<User> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "UserName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.UserName)
                        : queryable.OrderBy(e => e.UserName);
                case "Email":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Email)
                        : queryable.OrderBy(e => e.Email);
                case "FirstName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.FirstName)
                        : queryable.OrderBy(e => e.FirstName);
                case "LastName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.LastName)
                        : queryable.OrderBy(e => e.LastName);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<User> Filter(this IQueryable<User> queryable, UserFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.UserName!.ContainsTrim(filter.Text) || e.Email!.ContainsTrim(filter.Text) || e.FirstName!.ContainsTrim(filter.Text) || e.LastName!.ContainsTrim(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.UserName))
            {
                queryable = queryable.Where(e => e.UserName!.ContainsTrim(filter.UserName));
            }
            if (!string.IsNullOrEmpty(filter?.Email))
            {
                queryable = queryable.Where(e => e.Email!.ContainsTrim(filter.Email));
            }
            if (!string.IsNullOrEmpty(filter?.FirstName))
            {
                queryable = queryable.Where(e => e.FirstName!.ContainsTrim(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter?.LastName))
            {
                queryable = queryable.Where(e => e.LastName!.ContainsTrim(filter.LastName));
            }
            if (filter?.RoleId != null)
            {
                queryable = queryable.Where(e => e.Roles.Any(r => r.Id == filter.RoleId));
            }
            return queryable;
        }
    }
}
