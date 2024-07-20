using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class ExcelService : IExcelService
{
    public List<T> ReadFromExcel<T>(IFormFile file) where T : new()
    {
        return ExcelHelper.ReadFromExcel<T>(file);
    }
}