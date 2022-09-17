using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SkillAlexaMasterChech.Core.Models.AppSettings;
using SkillAlexaMasterChech.Core.Services.AzDataTableService;
using SkillAlexaMasterChech.Core.Services.WorkContentService;
using System;

[assembly: FunctionsStartup(typeof(JobAlexaMasterChech.Function.Startup))]
namespace JobAlexaMasterChech.Function
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            string azConnectionDataTable = Environment.GetEnvironmentVariable("AzConnectionDataTable");

            var recipeSettings = new RecipeAppSettings
            {
                AzConnectionDataTable = azConnectionDataTable
            };

            builder.Services.AddSingleton(recipeSettings);
            builder.Services.AddSingleton<IAzDataTableService, AzDataTableService>();
            builder.Services.AddSingleton<IWorkContentService, WorkContentService>();
        }
    }
}
