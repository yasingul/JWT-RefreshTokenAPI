using System.Net;

namespace RedisDemo
{
    public class ProductResponse
    {
        public HttpStatusCode StatusCode
        { get; set; }

        public string? Message
        { get; set; }

        public object? Data
        { get; set; }

        public bool IsDataFromCache
        { get; set; }

        public DateTime Timestamp
        { get; set; }
    }
}