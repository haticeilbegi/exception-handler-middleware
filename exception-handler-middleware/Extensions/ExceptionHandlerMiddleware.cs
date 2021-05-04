using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exception_handler_middleware.Extensions
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder HandleExceptions(this IApplicationBuilder app)
        {
            return
                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next.Invoke();
                    }
                    catch (Exception ex)
                    {
                        string result = "";
                        int code = 500;

                        if (ex is NoContentException)
                        {
                            code = (ex as NoContentException).StatusCode;
                            result = JsonConvert.SerializeObject((ex as NoContentException).Errors);
                        }
                        else if (ex is InvalidArgumentException)
                        {
                            code = (ex as InvalidArgumentException).StatusCode;
                            result = JsonConvert.SerializeObject((ex as InvalidArgumentException).Errors);
                        }
                        else if (ex is UnauthorizedException)
                        {
                            code = (ex as UnauthorizedException).StatusCode;
                            result = JsonConvert.SerializeObject((ex as UnauthorizedException).Errors);
                        }
                        else if (ex is BadRequestException)
                        {
                            code = (ex as BadRequestException).StatusCode;
                            result = JsonConvert.SerializeObject((ex as BadRequestException).Errors);
                        }
                        else
                        {
                            // TODO : İYİLEŞTİRİLEBİLİR
                            //Serilog.Log.Error(ex, code.ToString());

                            var error = new Error { Code = Resource.InternalServerError.Code, Message = Resource.InternalServerError.Message, Data = ex };
                            result = JsonConvert.SerializeObject(new Error[] { error });
                        }

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = code;
                        await context.Response.WriteAsync(result);
                    }
                });
        }
    }
}
