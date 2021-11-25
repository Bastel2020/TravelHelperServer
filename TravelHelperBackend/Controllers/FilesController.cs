using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFilesRepository _filesRepository;
        public FilesController(IFilesRepository repository)
        {
            _filesRepository = repository;
        }

        [HttpGet("Photos/{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var repsonse = await _filesRepository.GetPhoto(id);
            if (repsonse == null)
                return NotFound("Файл не найден.");
            if (repsonse.Length == 0)
                return StatusCode(410, "Запись файла найдена, но сам файл не обнаружен.");
            return File(repsonse, "image/jpeg");
        }
    }
}
