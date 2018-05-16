using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Services.Models
{
    public static class PageModelExtensions
    {
        public static async Task<PageModel<TDst>> MapToPagedListAsync<TSrc, TDst>(this IQueryable<TSrc> queryable, IMapper mapper, int pageIndex, int pageSize)
        {
            var items = await queryable
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var mapped = items.Select(mapper.Map<TSrc, TDst>);
            var result = new PageModel<TDst>(mapped, await queryable.CountAsync(), pageIndex);
            return result;
        }

        public static async Task<PageModel<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int? pageIndex, int? pageSize)
        {
            var result = new PageModel<T>();
            if (pageIndex != null && pageSize != null)
            {
                result.PageItems = await queryable
                    .Skip(pageIndex.Value * pageSize.Value)
                    .Take(pageSize.Value)
                    .ToListAsync();
                result.TotalItems = await queryable.CountAsync();
                result.CurrentPageIndex = pageIndex.Value;
            }
            else
            {
                var items = await queryable
                    .ToListAsync();
                result.PageItems = items;
                result.TotalItems = items.Count;
            }
            return result;
        }

        public static PageModel<TDst> Map<TSrc, TDst>(this PageModel<TSrc> model, IMapper mapper)
        {
            return new PageModel<TDst>(model.PageItems.Select(mapper.Map<TSrc, TDst>), model.TotalItems, model.CurrentPageIndex);
        }
    }
}
