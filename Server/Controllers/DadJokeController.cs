using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using blazorwasm.Shared;

namespace blazorwasm.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
public class DadJokeController : ControllerBase
{
    private readonly ILogger<DadJokeController> _logger;

    public DadJokeController(ILogger<DadJokeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<DadJoke> Get()
    {
        return new[]
        {
            new DadJoke { DateSubmitted = new DateOnly(2023, 11, 17), NoOfVotes = 10, SubmittedBy = "John", Joke = "Why did the tomato turn red? Because it saw the salad dressing!" },
            new DadJoke { DateSubmitted = new DateOnly(2023, 11, 16), NoOfVotes = 5, SubmittedBy = "Jane", Joke = "Why did the scarecrow win an award? Because he was outstanding in his field!" },
            new DadJoke { DateSubmitted = new DateOnly(2023, 11, 15), NoOfVotes = 3, SubmittedBy = "Bob", Joke = "Why did the coffee file a police report? It got mugged!" }
        };
    }
}
