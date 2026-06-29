// ШАБЛОН: как добавить тесты для своей утилиты
// 
// 1. Создайте файл Services/MyUtilityServiceTests.cs
// 2. Скопируйте код ниже и замените:
//    - MyUtilityService → имя вашего класса
//    - "my-endpoint" → endpoint вашей утилиты
//    - Тестовые данные → ваши сценарии
// 3. Запустите: dotnet test
//
// Какие тесты написать ОБЯЗАТЕЛЬНО:
// [ ] Пустой ввод
// [ ] Нормальный ввод (happy path)
// [ ] Граничные значения (очень длинная строка, спецсимволы)
// [ ] Некорректный ввод (ожидаете ошибку или graceful degradation)
//
// Шаблон (скопируйте в новый файл Services/MyUtilityServiceTests.cs):
//
// namespace WebApp.Api.Tests.Services;
//
// public class MyUtilityServiceTests
// {
//     private readonly MyUtilityService _service = new();
//
//     [Fact]
//     public void Execute_EmptyInput_ReturnsExpected()
//     {
//         var result = _service.Execute("");
//         // Assert... — проверьте, что вернулось ожидаемое значение
//     }
//
//     [Fact]
//     public void Execute_ValidInput_ReturnsCorrectResult()
//     {
//         var result = _service.Execute("ваши тестовые данные");
//         Assert.Contains("ожидаемый результат", result);
//     }
//
//     [Fact]
//     public void Execute_InvalidInput_HandlesGracefully()
//     {
//         var result = _service.Execute("некорректные данные");
//         // Утилита не должна падать — проверьте обработку ошибок
//     }
// }
