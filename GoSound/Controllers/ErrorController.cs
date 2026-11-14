using Microsoft.AspNetCore.Mvc;
namespace GoSound.Controllers;
public class ErrorController : Controller
{
 [Route("Error/Status/{code:int}")]
 public IActionResult StatusPage(int code) { Response.StatusCode=code; return View("StatusPage",code); }
 public IActionResult ServerError() { Response.StatusCode=500; return View(); }
}
