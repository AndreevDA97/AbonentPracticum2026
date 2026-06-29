using System.Globalization;

namespace WebApp.Api.Services;

/// <summary>
/// ПРИМЕР для студентов: сумма чисел, переданных по одному на строку.
/// 
/// Как создать свою утилиту:
/// 1. Скопируйте этот файл, переименуйте класс
/// 2. Реализуйте IUtilityService (Endpoint + Execute)
/// 3. Зарегистрируйте в Program.cs: builder.Services.AddSingleton&lt;IUtilityService, ВашКласс&gt;()
/// </summary>
public class SumNumbersService : IUtilityService
{
    public string Endpoint => "sum-numbers";

    public string Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "0";

        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        double sum = 0;
        var errors = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
            {
                sum += num;
            }
            else
            {
                errors.Add($"  Строка {i + 1}: «{line}» — не число");
            }
        }

        var result = $"Сумма: {sum}";
        if (errors.Count > 0)
            result += $"\n\nПропущены строки:\n{string.Join('\n', errors)}";

        return result;
    }
}
