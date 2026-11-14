using GoSound.Data;
using GoSound.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GoSound.Controllers;
public class PodcastsController(GoSoundDbContext db) : Controller
{
 public async Task<IActionResult> Index(string? search, int? categoryId)
 {
  if (search?.Length > 120) return BadRequest("Търсенето е прекалено дълго.");
  var query = db.Podcasts.AsNoTracking().Include(p => p.Category).Include(p => p.Owner).Where(p => p.IsPublished);
  if (!string.IsNullOrWhiteSpace(search)) query = query.Where(p => p.Title.Contains(search));
  if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId.Value);
  return View(new CatalogViewModel { Search=search, CategoryId=categoryId,
   Categories=await db.Categories.AsNoTracking().OrderBy(c=>c.Name).ToListAsync(),
   Podcasts=await query.OrderBy(p=>p.Title).Take(24).ToListAsync() });
 }
 public async Task<IActionResult> Details(int id)
 {
  var podcast=await db.Podcasts.AsNoTracking().Include(p=>p.Category).Include(p=>p.Owner)
   .Include(p=>p.Episodes.Where(e=>e.IsPublished)).FirstOrDefaultAsync(p=>p.Id==id && p.IsPublished);
  if(podcast is null) return NotFound();
  return View(podcast);
 }
}
