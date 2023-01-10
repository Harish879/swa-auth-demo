using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Messages
    {
        private readonly ILogger _logger;

        public Messages(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Messages>();
        }

        [Function("Messages")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var claimsPrincipal = req.Parse();
            var response = req.CreateResponse(HttpStatusCode.OK);
            
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Welcome to Azure Functions!");
            if(claimsPrincipal.Identity!.IsAuthenticated)
            {
                response.WriteString($"Hello {claimsPrincipal.Identity.Name}!");
            }
            else
            {
                response.WriteString("Hello Anonymous!");
            }
            return response;
        }
    }
}
