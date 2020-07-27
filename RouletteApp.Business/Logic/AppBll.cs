using RouletteApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApp.Business.Logic
{
    public abstract class AppBll
    {
        public ResponseModel response;
        public MessageListModel messageList;

        public AppBll(ResponseModel response, MessageListModel messageList)
        {
            this.response = response;
            this.messageList = messageList;
        }

        public void SetObjectResponse<T>(T obj, int statusCode)
        {
            if (obj != null)
            {
                response.Data = obj;
            }

            response.Message = messageList.ErrorList
                .Where(x => (int)x.Status == statusCode)
                .FirstOrDefault();
        }

        public void SetListResponse<T>(IEnumerable<T> list, int statusCode)
        {

            if (list.Count() > 0)
            {
                response.Data = list;
            }

            response.Message = messageList.ErrorList
                .Where(x => (int)x.Status == statusCode)
                .FirstOrDefault();
        }
    }
}