using System.ComponentModel.DataAnnotations;
namespace GoSound.Models;
public class Podcast
{
    public int Id { get; set; }
    [Required, MaxLength(120)] public string Title { get; set; } = "";
    [Required, MaxLength(2000)] public string Description { get; set; } = "";
    [MaxLength(10)] public string Language { get; set; } = "bg";
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string OwnerId { get; set; } = "";
    public ApplicationUser Owner { get; set; } = null!;
    public bool IsPublished { get; set; }
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
}
