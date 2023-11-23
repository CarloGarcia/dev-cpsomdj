using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using demo_blazor_swa_auth.Shared;
using System.Text;
using System.Text.Json;

namespace demo_blazor_swa_auth.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
public class VoteController : ControllerBase
{
    private readonly ILogger<DadJokeController> _logger;
    private readonly IConfiguration _configuration;

    public VoteController(ILogger<DadJokeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("votes/{userId}")]    
    public IEnumerable<Vote> Get(string userId)
    {
        var azureFunctionBaseUrl = _configuration.GetValue<string>("AzureFunctionBaseUrl");
        var azureFunctionKey = _configuration.GetValue<string>("AzureFunction_GetVoteByUserId_Key");

        var httpClient = new HttpClient();
        var url = $"{azureFunctionBaseUrl}api/votes/{userId}?code={azureFunctionKey}";
        var response = httpClient.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"DadJokeController.Get failed {response.StatusCode}");
            throw new Exception($"DadJokeController.Get failed {response.StatusCode}");
        }

        var json = response.Content.ReadAsStringAsync().Result;
        var votes = JsonSerializer.Deserialize<List<Vote>>(json);
        return votes;
    }

    // POST api/<DadJokeController> 
    [HttpPost]
    public void Post([FromBody] Vote vote)
    {
        var azureFunctionBaseUrl = _configuration.GetValue<string>("AzureFunctionBaseUrl");
        var azureFunctionKey = _configuration.GetValue<string>("AzureFunction_VoteForDadJoke_Key");

        var httpClient = new HttpClient();
        var url = $"{azureFunctionBaseUrl}api/VoteForDadJoke?code={azureFunctionKey}";

        var json = JsonSerializer.Serialize(vote);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = httpClient.PostAsync(url, content).Result;

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"DadJokeController.Put failed {response.StatusCode}");
            throw new Exception($"DadJokeController.Put failed {response.StatusCode}");
        }
    }
}
