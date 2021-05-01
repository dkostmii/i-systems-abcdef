using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace simple_blog_api.Types
{
    public class ErrorMessage
    {
        public String message { get; set; }
        public int statusCode { get; set; }

        public static JsonResult PrepareErrorResponse(String errorMessage, HttpStatusCode statusCode)
        {
            ErrorMessage message = new ErrorMessage
            {
                message = errorMessage,
                statusCode = (int)statusCode
            };

            JsonResult res = new JsonResult(message);
            res.StatusCode = message.statusCode;
            return res;
        }
    }
}
