using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Data;
using WebApp.Api.Models;
using WebApp.Api.Services;

namespace WebApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilitiesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly UtilityOrchestrator _orchestrator;

    public UtilitiesController(AppDbContext db, UtilityOrchestrator orchestrator)
    {
        _db = db;
        _orchestrator = orchestrator;
    }

    /// <summary>
    /// Получить список всех утилит.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Utility>>> GetAll()
    {
        var utilities = await _db.Utilities.OrderBy(u => u.Id).ToListAsync();

        // Отмечаем, какие утилиты уже реализованы
        var implemented = _orchestrator.GetImplementedEndpoints().ToHashSet();
        foreach (var u in utilities)
            u.IsImplemented = implemented.Contains(u.Endpoint);

        return Ok(utilities);
    }

    /// <summary>
    /// Получить утилиту по endpoint'у.
    /// </summary>
    [HttpGet("{endpoint}")]
    public async Task<ActionResult<Utility>> GetByEndpoint(string endpoint)
    {
        var utility = await _db.Utilities.FirstOrDefaultAsync(u => u.Endpoint == endpoint);
        if (utility is null)
            return NotFound();

        utility.IsImplemented = _orchestrator.GetImplementedEndpoints().Contains(endpoint);
        return Ok(utility);
    }

    /// <summary>
    /// Выполнить утилиту.
    /// </summary>
    [HttpPost("{endpoint}/execute")]
    public async Task<ActionResult<ExecuteResponse>> Execute(string endpoint, [FromBody] ExecuteRequest request)
    {
        var utility = await _db.Utilities.FirstOrDefaultAsync(u => u.Endpoint == endpoint);
        if (utility is null)
            return NotFound(new ExecuteResponse { Success = false, Error = "Утилита не найдена." });

        var result = _orchestrator.Execute(endpoint, request.Input);

        if (result.Success)
        {
            _db.ExecutionResults.Add(new ExecutionResult
            {
                UtilityId = utility.Id,
                Input = request.Input,
                Output = result.Output
            });
            await _db.SaveChangesAsync();
        }

        return Ok(result);
    }

    /// <summary>
    /// История выполнений конкретной утилиты.
    /// </summary>
    [HttpGet("{endpoint}/history")]
    public async Task<ActionResult<List<ExecutionResult>>> GetHistory(string endpoint, [FromQuery] int limit = 20)
    {
        var utility = await _db.Utilities.FirstOrDefaultAsync(u => u.Endpoint == endpoint);
        if (utility is null)
            return NotFound();

        var history = await _db.ExecutionResults
            .Where(r => r.UtilityId == utility.Id)
            .OrderByDescending(r => r.ExecutedAt)
            .Take(limit)
            .ToListAsync();

        return Ok(history);
    }
}
