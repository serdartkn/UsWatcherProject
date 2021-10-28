using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("file"))] IFormFile file, [FromForm] string fileName)
        {
            // Method intentionally left empty.
        }
    }
}
