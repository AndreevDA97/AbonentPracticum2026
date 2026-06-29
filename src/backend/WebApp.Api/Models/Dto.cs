namespace WebApp.Api.Models;

/// <summary>
/// DTO для выполнения утилиты.
/// </summary>
public class ExecuteRequest
{
    public string Input { get; set; } = string.Empty;
}

/// <summary>
/// DTO результата выполнения.
/// </summary>
public class ExecuteResponse
{
    public string Output { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
    public string? Error { get; set; }
}
