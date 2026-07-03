using Newtonsoft.Json;
using System.Globalization;

namespace WebApp.Api.Services;

public class TestUtilityService : IUtilityService
{
    public string Endpoint => "test-utility";

    public string Execute(string input)
    {
        var model = JsonConvert.DeserializeObject<TestUtilityRequest>(input);

        // ...
        return JsonConvert.SerializeObject(new TestUtilityReponse { Result1 = 123, Result2 = "OK!" });
    }
}

public class TestUtilityRequest
{
    public string Field1 { get; set; }
    public string Field2 { get; set; }
}

public class TestUtilityReponse
{
    public int Result1 { get; set; }
    public string Result2 { get; set; }
}
