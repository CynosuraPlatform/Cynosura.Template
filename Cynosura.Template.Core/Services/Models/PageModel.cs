using System.Collections.Generic;

namespace Cynosura.Template.Core.Services.Models
{
    public class PageModel<T>
    {
        public IList<T> PageItems { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPageIndex { get; set; }

        public PageModel()
        {

        }

        public PageModel(IList<T> pageItems, int totalItems, int currentPageIndex)
        {
            PageItems = pageItems;
            TotalItems = totalItems;
            CurrentPageIndex = currentPageIndex;
        }

        public PageModel(IEnumerable<T> pageItems, int totalItems, int currentPageIndex)
        {
            PageItems = new List<T>(pageItems);
            TotalItems = totalItems;
            CurrentPageIndex = currentPageIndex;
        }
    }
}
