using System.Net;

namespace DAL
{
    /// <summary>
    /// Describes the returned result from the database
    /// </summary>
    /// <typeparam name="T">type of result</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Contains the return value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Determines the status of the operation
        /// </summary>
        public bool isOk { get; set; }
    }
}
