using System.ComponentModel.DataAnnotations;
namespace GoSound.Models;
public class Category
{
    public int Id { get; set; }
    [Required, MaxLength(60)] public string Name { get; set; } = "";
    public ICollection<Podcast> Podcasts { get; set; } = new List<Podcast>();
}
