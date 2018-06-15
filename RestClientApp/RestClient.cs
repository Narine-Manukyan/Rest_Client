using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestClientApp
{
    /// <summary>
    /// Implementation of a REST client library which consumes RESTful APIs.
    /// </summary>
    public class RestClient<T> : IRestClient<T> where T : class, new()
    {
        /// <summary>
        /// Serializer object for serialize and deserialize.
        /// </summary>
        public readonly JSONSerializer<T> serializer;

        /// <summary>
        /// Format of serializing.
        /// </summary>
        public const string Format = "JSON";

        /// <summary>
        /// The parameterless constructor.
        /// </summary>
        public RestClient()
        {
            this.serializer = new JSONSerializer<T>();
        }

        /// <summary>
        /// Delete the specified item from storage.
        /// </summary>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was deleted, else returns false</returns>
        public Task Delete(string resource)
        {
            // Doing HTTP request due to HttpClient.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["Url"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"api/{Format}"));
                var result = client.DeleteAsync(resource);
                return result;
            }
        }

        /// <summary>
        /// Get all items from storage.
        /// </summary>
        /// <param name="resource">the resource</param>
        /// <returns>Collection of T</returns>
        public async Task<IEnumerable<T>> Get(string resource)
        {
            // Doing HTTP request due to HttpClient.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["Url"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"api/{Format}"));
                var response = await client.GetAsync(resource);
                if (response.IsSuccessStatusCode)
                {
                    var temp = await response.Content.ReadAsStringAsync();
                    var result = this.serializer.Deserialize<IEnumerable<T>>(temp).ToList();
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Insert a new item in storage.
        /// </summary>
        /// <param name="dto">T that will be inserted.</param>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was inserted, else returns false</returns>
        public Task Post(T dto, string resource)
        {
            // Doing HTTP request due to HttpClient.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["Url"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"api/{Format}"));
                var temp = this.serializer.Serialize(dto);
                var result = client.PostAsync(resource, temp);
                return result;
            }
        }

        /// <summary>
        /// Updating the specified item in storage.
        /// </summary>
        /// <param name="newDto">new T</param>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was Updated, else returns false</returns>
        public Task Put(T newDto, string resource)
        {
            // Doing HTTP request due to HttpClient.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["Url"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"api/{Format}"));
                var temp = this.serializer.Serialize(newDto);
                var result = client.PutAsync(resource, temp);
                return result;
            }
        }
    }
}
