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

    public VoteController(ILogger<DadJokeController> logger)
    {
        _logger = logger;
    }

    // [HttpGet]
    // public IEnumerable<DadJoke> Get()
    // {
    //     var azureFunctionBaseUrl = "http://localhost:7135/"; // Environment.GetEnvironmentVariable("AzureFunctionBaseUrl");
    //     var azureFunctionKey = "Y8tzM_4SZBCU4SLYtOrsqOenuXLLo_SjhFoiWiVENrWMAzFuSWGMPw=="; // Environment.GetEnvironmentVariable("AzureFunction_GetDadJokes_Key");

    //     var httpClient = new HttpClient();
    //     var url = $"{azureFunctionBaseUrl}/api/GetDataJokes?code={azureFunctionKey}";
    //     var response = httpClient.GetAsync(url).Result;

    //     if (!response.IsSuccessStatusCode)
    //     {
    //         _logger.LogError($"DadJokeController.Get failed {response.StatusCode}");
    //         throw new Exception($"DadJokeController.Get failed {response.StatusCode}");
    //     }

    //     var json = response.Content.ReadAsStringAsync().Result;
    //     var dadJokes = JsonSerializer.Deserialize<List<DadJoke>>(json);
    //     return dadJokes;
    // }

    // POST api/<DadJokeController> 
    [HttpPost]
    public void Post([FromBody] Vote vote)
    {
        var azureFunctionBaseUrl = "http://localhost:7135/"; // Environment.GetEnvironmentVariable("AzureFunctionBaseUrl");
        var azureFunctionKey = "1k_cmYAIGPJlh7-5MFgNyyIHRlshA5cwFuN6eflifDTAAzFuADyw-g=="; // Environment.GetEnvironmentVariable("AzureFunction_VoteForDadJoke_Key");

        var httpClient = new HttpClient();
        var url = $"{azureFunctionBaseUrl}api/VoteForDadJoke?code={azureFunctionKey}";

        var json = JsonSerializer.Serialize(vote);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = httpClient.PutAsync(url, content).Result;

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"DadJokeController.Put failed {response.StatusCode}");
            throw new Exception($"DadJokeController.Put failed {response.StatusCode}");
        }
    }
}
