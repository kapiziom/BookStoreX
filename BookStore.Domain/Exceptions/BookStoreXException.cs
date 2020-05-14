using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookStore.Domain.Exceptions
{
    public class BookStoreXException : Exception
    {
        // Holds Http status code: 404 NotFound, 400 BadRequest, ...
        public int StatusCode { get; }
        public string MessageDetail { get; set; }
        public object Value { get; set; }
        public object Result { get; set; }

        public BookStoreXException(int statusCode, string message = null) : base(message)
        {
            StatusCode = statusCode;
        }

        public BookStoreXException(int statusCode, string message = null, object result = null) : base(message)
        {
            StatusCode = statusCode;
            Result = result;
        }
    }
}
