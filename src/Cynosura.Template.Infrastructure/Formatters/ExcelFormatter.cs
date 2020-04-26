using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Template.Core.Formatters;
using OfficeOpenXml;

namespace Cynosura.Template.Infrastructure.Formatters
{
    public class ExcelFormatter : IExcelFormatter
    {
        public Task<IEnumerable<T>> LoadFromAsync<T>(Stream stream, bool withHeader)
        {
            throw new NotImplementedException();
        }

        public async Task SaveToAsync<T>(Stream stream, IEnumerable<T> data, bool withHeader)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells.LoadFromCollection(data, withHeader);
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            await package.SaveAsAsync(stream);
        }
    }
}
