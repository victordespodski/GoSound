using Microsoft.AspNetCore.Mvc;
namespace GoSound.Controllers;
public class HomeController : Controller
{
 public IActionResult Index() => View();
 public IActionResult About() => View();
}
