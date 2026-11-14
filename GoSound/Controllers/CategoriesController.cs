using GoSound.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GoSound.Controllers;
public class CategoriesController(GoSoundDbContext db) : Controller
{
 public async Task<IActionResult> Index() => View(await db.Categories.AsNoTracking().OrderBy(c=>c.Name).ToListAsync());
}
