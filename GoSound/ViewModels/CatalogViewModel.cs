using GoSound.Models;
namespace GoSound.ViewModels;
public class CatalogViewModel
{
 public string? Search { get; set; }
 public int? CategoryId { get; set; }
 public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();
 public IReadOnlyList<Podcast> Podcasts { get; set; } = new List<Podcast>();
}
