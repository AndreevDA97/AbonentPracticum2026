namespace WebApp.Api.Models;

/// <summary>
/// Результат выполнения утилиты.
/// </summary>
public class ExecutionResult
{
    public int Id { get; set; }
    public int UtilityId { get; set; }
    public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public Utility? Utility { get; set; }
}
