using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Api.Services;

public class CipherTextUtilityService : IUtilityService
{
    public string Endpoint => "cipher-text";

    public string Execute(string input)
    {
        var model = JsonConvert.DeserializeObject<TestUtilityRequest>(input);

        string? encryptedcontent = null;

        switch (model.CipherType)
        {
            case CipherTypes.Aes: // AES
                var crypt = Aes.Create();
                byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                crypt.IV = IV;
                crypt.BlockSize = 128;
                HashAlgorithm hash = MD5.Create();
                crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(model.Key));
                
                var textBytes = Encoding.Unicode.GetBytes(model.Text);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                           new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(textBytes, 0, textBytes.Length);
                    }

                    encryptedcontent = Convert.ToBase64String(memoryStream.ToArray());
                }

                break;
            
            default:
                throw new NotImplementedException();
        }
        return JsonConvert.SerializeObject(new TestUtilityReponse { Result = encryptedcontent });
    }
}

public enum CipherTypes {
    Aes = 1,
    TripleDes = 2,
    Rabbit = 3,
    Rc4 = 4,
}

public class TestUtilityRequest
{
    public string Text { get; set; }
    public CipherTypes CipherType { get; set; }
    public string Key { get; set; }
}

public class TestUtilityReponse
{
    public string Result { get; set; }
}
