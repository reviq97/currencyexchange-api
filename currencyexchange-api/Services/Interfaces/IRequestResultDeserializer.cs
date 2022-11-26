using currencyexchange_api.Models;

namespace currencyexchange_api.Services.Interfaces
{
    public interface IRequestResultDeserializer
    {
        Task<T> DeserializeXmlToObject<T>(string deserializedObject);
    }
}