using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers;

[Route("api/files")]
public class FilesController : Controller
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
    }

    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        var filePath = "image-7.jpg";
        if (!System.IO.File.Exists(filePath)) return NotFound();

        if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
            contentType = "application/octet-stream";

        var fileStream = System.IO.File.ReadAllBytes(filePath);
        return File(fileStream, contentType, Path.GetFileName(filePath));
    }
}