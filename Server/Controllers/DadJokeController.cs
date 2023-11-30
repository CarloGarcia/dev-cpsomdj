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
public class DadJokeController : ControllerBase
{
    private readonly ILogger<DadJokeController> _logger;
    private readonly IConfiguration _configuration;

    public DadJokeController(ILogger<DadJokeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<DadJoke> Get()
    {
        var azureFunctionBaseUrl = _configuration.GetValue<string>("AzureFunctionBaseUrl");
        var azureFunctionKey = _configuration.GetValue<string>("AzureFunction_GetDadJokes_Key");

        var httpClient = new HttpClient();
        var url = $"{azureFunctionBaseUrl}api/GetDadJokes?code={azureFunctionKey}";
        var response = httpClient.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"DadJokeController.Get failed {response.StatusCode}");
            throw new Exception($"DadJokeController.Get failed {response.StatusCode}");
        }

        var json = response.Content.ReadAsStringAsync().Result;
        var dadJokes = JsonSerializer.Deserialize<List<DadJoke>>(json);
        return dadJokes;
    }

    // POST api/<DadJokeController> 
    [HttpPost]
    public void Post([FromBody] DadJoke dadJoke)
    {
        // var azureFunctionBaseUrl = _configuration.GetValue<string>("AzureFunctionBaseUrl");
        // var azureFunctionKey = _configuration.GetValue<string>("AzureFunction_AddDadJoke_Key");

        // var httpClient = new HttpClient();        
        // var url = $"{azureFunctionBaseUrl}api/AddDadJoke?code={azureFunctionKey}";

        // var json = JsonSerializer.Serialize(dadJoke);
        // var content = new StringContent(json, Encoding.UTF8, "application/json");
        // var response = httpClient.PostAsync(url, content).Result;

        // if (!response.IsSuccessStatusCode)
        // {
        //     _logger.LogError($"DadJokeController.Post failed {response.StatusCode}");
        //     throw new Exception($"DadJokeController.Post failed {response.StatusCode}");
        // }        
    }
}
