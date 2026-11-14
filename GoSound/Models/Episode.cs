using System.ComponentModel.DataAnnotations;
namespace GoSound.Models;
public class Episode
{
    public int Id { get; set; }
    public int PodcastId { get; set; }
    public Podcast Podcast { get; set; } = null!;
    [Required, MaxLength(160)] public string Title { get; set; } = "";
    [MaxLength(4000)] public string Description { get; set; } = "";
    [MaxLength(200)] public string? AudioKey { get; set; }
    public int? DurationSeconds { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
