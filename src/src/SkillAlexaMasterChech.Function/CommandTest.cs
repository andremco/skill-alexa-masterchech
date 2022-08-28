using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET;

namespace SkillAlexaMasterChech.Function
{
    public static class CommandTest
    {
        [FunctionName("CommandTest")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            log.LogInformation(json);

            var requestType = skillRequest.GetRequestType();

            SkillResponse response = null;
            response = ResponseBuilder.Tell("Welcome to the new world!");

            if (requestType == typeof(LaunchRequest))
            {
                response = ResponseBuilder.Tell("Welcome to the new world!");
                response.Response.ShouldEndSession = false;
            }

            return new OkObjectResult(response);
        }
    }
}
