namespace WebApp.Api.Models;

/// <summary>
/// Встраиваемая утилита веб-приложения.
/// </summary>
public class Utility
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string Category { get; set; } = "Прочее";
    public int Difficulty { get; set; } = 1; // 1-3
    public bool IsImplemented { get; set; } = false;
}
