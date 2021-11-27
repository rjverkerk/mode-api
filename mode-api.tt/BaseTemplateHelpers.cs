using Newtonsoft.Json;
using System.Dynamic;
using System.Xml.Linq;

namespace mode_api.tt
{
    public static class Model
    {
        private static string _modelPath = @"C:\Source\mode-api\mode-api.tt\TTProcess.XML";

        private static dynamic Get() {
            XDocument doc = XDocument.Load(@"C:\Source\mode-api\mode-api.tt\TTProcess.XML");
            string jsonText = JsonConvert.SerializeXNode(doc);
            dynamic model =  JsonConvert.DeserializeObject<ExpandoObject>(jsonText);

            return model;
        }
    }
}
