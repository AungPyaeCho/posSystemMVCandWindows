using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public interface IExcelService
{
    List<T> ReadFromExcel<T>(IFormFile file) where T : new();
}

