namespace GoSound.Models;
public class ListeningProgress
{
 public string UserId { get; set; } = "";
 public ApplicationUser User { get; set; } = null!;
 public int EpisodeId { get; set; }
 public Episode Episode { get; set; } = null!;
 public int PositionSeconds { get; set; }
 public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
