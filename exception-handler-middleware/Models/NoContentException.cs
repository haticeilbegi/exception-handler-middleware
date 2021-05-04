using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace exception_handler_middleware.Models
{
    public class NoContentException : ApiExceptionBase
    {
        public NoContentException(string code, string message, object data = null)
        {
            Errors = new Error[] { new Error { Code = code, Message = message, Data = data } };
        }

        public NoContentException(Error[] errors)
        {
            Errors = errors;
        }

        protected override HttpStatusCode HttpStatusCode => HttpStatusCode.NoContent;
    }
}
