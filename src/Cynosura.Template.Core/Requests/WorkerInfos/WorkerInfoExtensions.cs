﻿using System;
using System.Linq;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public static class WorkerInfoExtensions
    {
        public static IOrderedQueryable<WorkerInfo> OrderBy(this IQueryable<WorkerInfo> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "ClassName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ClassName)
                        : queryable.OrderBy(e => e.ClassName);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Name);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<WorkerInfo> Filter(this IQueryable<WorkerInfo> queryable, WorkerInfoFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Text) || e.ClassName.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.ClassName))
            {
                queryable = queryable.Where(e => e.ClassName.Contains(filter.ClassName));
            }
            return queryable;
        }
    }
}
