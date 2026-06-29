namespace WebApp.Api.Services;

/// <summary>
/// ИНТЕРФЕЙС, который должна реализовать КАЖДАЯ новая утилита.
/// 
/// Что нужно сделать студенту:
/// 1. Создать класс, реализующий этот интерфейс (см. SumNumbersService как пример)
/// 2. Endpoint — уникальный строковый ключ (например "my-tool")
/// 3. Execute(string input) — принимает входную строку, возвращает результат
/// 4. Зарегистрировать в Program.cs: builder.Services.AddSingleton&lt;IUtilityService, МойКласс&gt;()
/// </summary>
public interface IUtilityService
{
    /// <summary>Уникальный идентификатор утилиты (используется в URL)</summary>
    string Endpoint { get; }

    /// <summary>Бизнес-логика: принимает строку ввода, возвращает строку результата</summary>
    string Execute(string input);
}
