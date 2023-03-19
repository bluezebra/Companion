using Microsoft.AspNetCore.Mvc;

namespace Companion.Controllers;

[ApiController]
[Route("/")]
public class CompanionController : ControllerBase
{
    private readonly IConfiguration _config;

    public CompanionController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> Post(ChatRequest request)
    {
        var api = new OpenAI_API.OpenAIAPI(_config["OpenAI:ApiKey"]);
        var result = await api.Completions.GetCompletion(request.Question);

        var chatResponse = new ChatResponse(result);

        return chatResponse;
    }
}

public record ChatRequest(string Question);

public record ChatResponse(string Answer);
