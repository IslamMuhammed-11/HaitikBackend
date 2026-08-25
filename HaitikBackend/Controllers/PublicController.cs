using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublicController : ControllerBase
{


    [HttpGet("track/{token}")]
    public async Task<IActionResult> TrackOrder(string token)
    {
        throw new NotImplementedException();
    }


}
