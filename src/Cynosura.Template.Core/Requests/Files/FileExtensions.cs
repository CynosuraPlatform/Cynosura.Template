using System;
using System.Linq;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;

namespace Cynosura.Template.Core.Requests.Files
{
    public static class FileExtensions
    {
        public static IOrderedQueryable<File> OrderBy(this IQueryable<File> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "ContentType":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ContentType)
                        : queryable.OrderBy(e => e.ContentType);
                case "Url":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Url)
                        : queryable.OrderBy(e => e.Url);
                case "Group":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Group.Name)
                        : queryable.OrderBy(e => e.Group.Name);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<File> Filter(this IQueryable<File> queryable, FileFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.ContainsTrim(filter.Text) || e.ContentType.ContainsTrim(filter.Text) || e.Url!.ContainsTrim(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.ContainsTrim(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.ContentType))
            {
                queryable = queryable.Where(e => e.ContentType.ContainsTrim(filter.ContentType));
            }

            if (!string.IsNullOrEmpty(filter?.Url))
            {
                queryable = queryable.Where(e => e.Url!.ContainsTrim(filter.Url));
            }
            if (filter?.GroupId != null)
            {
                queryable = queryable.Where(e => e.GroupId == filter.GroupId);
            }
            return queryable;
        }

        public static void Validate(this string? accept, string filename, string contentType)
        {
            if (string.IsNullOrEmpty(accept))
                return;
            var valid = accept.Split(",")
                .Select(a => a.Trim())
                .Any(a => filename.EndsWith(a) || MatchContentType(contentType, a));
            if (!valid)
            {
                throw new ServiceException("Invalid file format");
            }
        }

        private static bool MatchContentType(string contentType, string pattern)
        {
            if (pattern == null)
                return false;
            if (contentType == pattern)
                return true;

            var patternParts = pattern.Split("/");
            if (patternParts.Length == 2 && patternParts[1] == "*" && contentType.StartsWith(patternParts[0] + "/"))
            {
                return true;
            }
            return false;
        }

        public static byte[] ConvertToBytes(this System.IO.Stream input)
        {
            input.Position = 0;
            using var ms = new System.IO.MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
