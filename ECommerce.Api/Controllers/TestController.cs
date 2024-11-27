using ECommerce.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IStorageServices storageServices,IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Test(IFormFile formFile)
        {
            var result = await storageServices.SaveImage(formFile);
            
            return Ok(result);
        }
    }
}
