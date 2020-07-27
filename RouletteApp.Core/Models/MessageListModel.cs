using RouletteApp.Core.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace RouletteApp.Core.Models
{
    public class MessageListModel : IMessageListModel
    {
        public List<MessageModel> ErrorList { get; set; } = new List<MessageModel> {
            new MessageModel {
                Id = 1,
                Description = "The request has been successful",
                Status = HttpStatusCode.OK
            },
            new MessageModel {
                Id = 2,
                Description = "No results found",
                Status = HttpStatusCode.NotFound
            },
            new MessageModel {
                Id = 3,
                Description = "The request has been rejected",
                Status = HttpStatusCode.BadRequest
            },
            new MessageModel {
                Id = 4,
                Description = "The request was created successfully",
                Status = HttpStatusCode.Created
            },
            new MessageModel {
                Id = 5,
                Description = "The request has not been updated.",
                Status = HttpStatusCode.NotModified
            },
            new MessageModel {
                Id = 6,
                Description = "Request approved.",
                Status = HttpStatusCode.Accepted
            },
            new MessageModel {
                Id = 7,
                Description = "An unexpected error occurred while your request was being processed.",
                Status = HttpStatusCode.InternalServerError
            }
        };
    }
}