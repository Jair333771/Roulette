using System.Net;

namespace RouletteApp.Core.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Description { get; set; }
    }
}