using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using SkillAlexaMasterChech.Core.Models.AppSettings;
using SkillAlexaMasterChech.Core.Models.DataTableEntities;
using SkillAlexaMasterChech.Core.Services.AzDataTableService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAlexaMasterChech.Core.Services.WorkContentService
{
    public class WorkContentService : IWorkContentService
    {
        private readonly IAzDataTableService _azDataTableService;
        private readonly RecipeAppSettings _recipeAppSettings;
        private readonly ILogger _logger;

        public WorkContentService(IAzDataTableService azDataTableService,
            RecipeAppSettings recipeAppSettings,
            ILoggerFactory loggerFactory)
        {
            _azDataTableService = azDataTableService;
            _recipeAppSettings = recipeAppSettings;
            _logger = loggerFactory.CreateLogger("Work");
        }

        public async Task<string> LoadRecipeForAlexa()
        {
            var ingredients = await GetIngredients();
            var recipes = await GetRecipes();
            var recipe = recipes.FirstOrDefault();
            string speechResponse = string.Empty;

            if(ingredients.Any() && recipes.Any() && recipe != null)
            {
                var descriptionIngredients = ingredients.Select(i => i.Description.Trim()).ToArray();
                speechResponse += $"{recipe.Title}, ";
                speechResponse += "Ingredientes: ";
                speechResponse += string.Join(", ", descriptionIngredients);
            }

            return speechResponse;
        }

        public async Task<ICollection<IngredientEntity>> GetIngredients()
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, "Ingredients");
            _azDataTableService.TableClient = tableClient;

            var ingredients = await _azDataTableService.GetIngredients();

            if (ingredients.Any())
                ingredients = ingredients.OrderBy(p => new Random().Next()).Take(5).ToList();

            return ingredients;
        }

        public async Task<ICollection<RecipeEntity>> GetRecipes()
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, "Recipes");
            _azDataTableService.TableClient = tableClient;

            var recipes = await _azDataTableService.GetRecipes();

            if (recipes.Any())
                recipes = recipes.OrderBy(p => new Random().Next()).Take(5).ToList();

            return recipes;
        }
    }
}
