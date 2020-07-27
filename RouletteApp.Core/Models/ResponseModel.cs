using RouletteApp.Core.Interfaces;
using System;

namespace RouletteApp.Core.Models
{
    public class ResponseModel : IResponseModel
    {
        public Exception Exception { get; set; }
        public MessageModel Message { get; set; }
        public object Data { get; set; }
    }
}