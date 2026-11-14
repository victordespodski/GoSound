namespace GoSound.Models;
public class Follow
{
 public string UserId { get; set; } = "";
 public ApplicationUser User { get; set; } = null!;
 public int PodcastId { get; set; }
 public Podcast Podcast { get; set; } = null!;
}
