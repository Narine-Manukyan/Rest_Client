using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestClientApp
{
    /// <summary>
    /// Interface for the following HTTP methods GET, PUT, POST, DELETE.
    /// </summary>
    public interface IRestClient<T>
    {
        /// <summary>
        /// Get all items from storage.
        /// </summary>
        /// <param name="resource">the resource</param>
        /// <returns>Collection of T</returns>
        Task<IEnumerable<T>> Get(String resource);
        
        /// <summary>
        /// Insert a new item in storage.
        /// </summary>
        /// <param name="dto">T that will be inserted.</param>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was inserted, else returns false</returns>
        Task Post(T dto, String resource);

        /// <summary>
        /// Delete the specified item from storage.
        /// </summary>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was deleted, else returns false</returns>
        Task Delete(String resource);

        /// <summary>
        /// Updating the specified item in storage.
        /// </summary>
        /// <param name="newDto">new T</param>
        /// <param name="resource">the resource</param>
        /// <returns>True if item was Updated, else returns false</returns>
        Task Put(T newDto, String resource);
    }
}
