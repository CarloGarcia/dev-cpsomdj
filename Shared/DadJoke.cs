namespace blazorwasm.Shared;

public class DadJoke
{
    public DateOnly DateSubmitted { get; set; }

    public int NoOfVotes { get; set; }

    public string? SubmittedBy { get; set; }

    public string? Joke { get; set; }
}
