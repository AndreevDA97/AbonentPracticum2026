using WebApp.Api.Models;

namespace WebApp.Api.Services;

/// <summary>
/// Оркестратор утилит: регистрирует сервисы и маршрутизирует вызовы.
/// Студенту: здесь НИЧЕГО менять не нужно. Просто добавьте свой сервис в Program.cs.
/// </summary>
public class UtilityOrchestrator
{
    private readonly Dictionary<string, IUtilityService> _services = new();

    /// <summary>
    /// Регистрирует сервис утилиты по его endpoint'у.
    /// </summary>
    public void Register(IUtilityService service)
    {
        _services[service.Endpoint] = service;
    }

    /// <summary>
    /// Выполняет утилиту по endpoint'у.
    /// </summary>
    public ExecuteResponse Execute(string endpoint, string input)
    {
        if (!_services.TryGetValue(endpoint, out var service))
            return new ExecuteResponse { Success = false, Error = $"Утилита «{endpoint}» не найдена или ещё не реализована." };

        try
        {
            var output = service.Execute(input);
            return new ExecuteResponse { Output = output, Success = true };
        }
        catch (Exception ex)
        {
            return new ExecuteResponse { Success = false, Error = $"Ошибка выполнения: {ex.Message}" };
        }
    }

    /// <summary>
    /// Возвращает список всех зарегистрированных (реализованных) утилит.
    /// </summary>
    public IEnumerable<string> GetImplementedEndpoints() => _services.Keys;
}
