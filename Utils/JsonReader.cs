

using Newtonsoft.Json.Linq;

namespace SeleniumWithCsharp.Utils
{
    class Json_Reader
    {
      

        public string ExtractData(String TokenName)
        {
            String myJsonString = File.ReadAllText("Data/Testdata.json");
            var JsonObject = JToken.Parse(myJsonString);
            return JsonObject.SelectToken(TokenName).Value<string>();        }       
    }
}
