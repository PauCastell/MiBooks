using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MyBooks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController: ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public FileController(IFileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
        }


        [HttpGet("input")]
        public IActionResult GetFileBooks()
        {
            //TODO: Implementar control de errores y validaciones. Y obtener extensión del Entorno.
            var path = _configuration["BookInputIntPath"] ?? "";
            var files = _fileService.GetFileBookFromPath(path, "*.epub");
            return Ok(files);
        }
    }
}
