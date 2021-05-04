using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace exception_handler_middleware.Models
{
    public abstract class ApiExceptionBase : Exception
    {
        public Error[] Errors { get; set; }

        protected abstract HttpStatusCode HttpStatusCode { get; }

        public int StatusCode => (int)HttpStatusCode;
    }

    public class ApiExceptionResponse
    {
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
