using System.ComponentModel.DataAnnotations;

namespace demo_blazor_swa_auth.Shared;

public class DadJoke
{
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime DateSubmitted { get; set; }
    public int NoOfVotes { get; set; }
    public string? SubmittedBy { get; set; }
    [Required]
    public string? Joke { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
}
