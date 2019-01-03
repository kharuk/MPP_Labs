using System;

namespace Model
{
    public class Message
    {
        public RequestType Request { get; set; }
        public object Data { get; set; }

        public Message(RequestType request, object data)
        {
            Request = request;
            Data = data;
        }
    }
}
