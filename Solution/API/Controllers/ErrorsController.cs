using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ErrorsController : ApiController
{
    [HttpGet("error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
