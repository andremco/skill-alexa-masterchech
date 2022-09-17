using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SkillAlexaMasterChech.Core.Services.WorkContentService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillAlexaMasterChech.Function
{
    public class TestMasterChech
    {
        private readonly IWorkContentService _workContentService;

        public TestMasterChech(IWorkContentService workContentService)
        {
            _workContentService = workContentService;
        }

        [FunctionName("TestMasterChech")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation($"Skill Alexa MasterChech Test executed at: {DateTime.Now}");

                var speechResponse = await _workContentService.LoadRecipeForAlexa();

                return new OkObjectResult(new { speechResponse });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error Skill Alexa MasterChech");

                throw ex;
            }
        }
    }
}
