using System;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace RestClientApp
{
    /// <summary>
    /// JSON serializer for items.
    /// </summary>
    public class JSONSerializer<T>
    {
        /// <summary>
        /// Generate object from JSON.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>deserialized object</returns>
        public T Deserialize<T>(string obj)
        {
            return new JavaScriptSerializer().Deserialize<T>(obj);
        }

        /// <summary>
        /// Gets string from json.
        /// </summary>
        /// <param name="obj">serialiable object</param>
        /// <returns>serialized object</returns>
        public StringContent Serialize<T>(T obj)
        {
            return new StringContent(new JavaScriptSerializer().Serialize(obj), Encoding.UTF8, "api/json");
        }
    }
}
