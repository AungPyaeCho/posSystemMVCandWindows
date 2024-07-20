
internal interface IExcelUpload
{
    IEnumerable<object> ReadFromExcel<T>(IFormFile file);
}