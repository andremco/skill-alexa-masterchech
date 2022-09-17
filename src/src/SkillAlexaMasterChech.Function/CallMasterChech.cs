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
using SkillAlexaMasterChech.Core.Services.WorkContentService;
using System.Linq;

namespace SkillAlexaMasterChech.Function
{
    public class CallMasterChech
    {
        private readonly IWorkContentService _workContentService;

        public CallMasterChech(IWorkContentService workContentService)
        {
            _workContentService = workContentService;
        }

        [FunctionName("CallMasterChech")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var requestType = skillRequest.GetRequestType();

            SkillResponse response = null;

            if (requestType == typeof(LaunchRequest) || requestType == typeof(IntentRequest))
            {
                var speech = await _workContentService.LoadRecipeForAlexa();

                if (!string.IsNullOrWhiteSpace(speech))
                {
                    response = ResponseBuilder.Tell(speech);
                    response.Response.ShouldEndSession = false;
                }
            }

            return new OkObjectResult(response);
        }
    }
}
