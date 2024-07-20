using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class ExcelHelper
{
    public static List<T> ReadFromExcel<T>(IFormFile file) where T : new()
    {
        List<T> items = new List<T>();

        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                var properties = typeof(T).GetProperties();

                // Read headers from the first row
                var headers = Enumerable.Range(1, colCount)
                                        .Select(col => worksheet.Cells[1, col].Text)
                                        .ToList();

                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                {
                    T item = new T();
                    bool hasData = false;

                    for (int col = 1; col <= colCount; col++)
                    {
                        var header = headers[col - 1];
                        var prop = properties.FirstOrDefault(p => p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));

                        if (prop != null)
                        {
                            var cellValue = worksheet.Cells[row, col].Text;

                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                hasData = true; // Mark that this row has data

                                try
                                {
                                    var convertedValue = Convert.ChangeType(cellValue, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                                    prop.SetValue(item, convertedValue);
                                }
                                catch (Exception ex)
                                {
                                    throw new FormatException($"Error converting value '{cellValue}' to type '{prop.PropertyType}': {ex.Message}");
                                }
                            }
                        }
                    }

                    if (hasData) // Only add items that have data
                    {
                        items.Add(item);
                    }
                }
            }
        }

        return items;
    }
}
