using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace currencyexchange_api.Services
{
    public class RequestResultSerializer : IRequestResultDeserializer
    {

        public async Task<T> DeserializeXmlToObject<T>(string deserializedObject)
        {
            var xml = XDocument.Parse(deserializedObject);
            var serializer = new XmlSerializer(typeof(T));

            var doc = (T)serializer.Deserialize(xml.CreateReader());

            return doc;
        }
    }
}
