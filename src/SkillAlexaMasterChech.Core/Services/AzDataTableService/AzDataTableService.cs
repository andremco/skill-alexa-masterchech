using Azure;
using Azure.Data.Tables;
using SkillAlexaMasterChech.Core.Models.DataTableEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAlexaMasterChech.Core.Services.AzDataTableService
{
    public class AzDataTableService : IAzDataTableService
    {
        public TableClient TableClient { get; set; }

        public AzDataTableService()
        {
        }

        public async Task<ICollection<IngredientEntity>> GetIngredients()
        {
            if (TableClient == null) throw new NullReferenceException("TableClient");

            var queryResults = TableClient.QueryAsync<IngredientEntity>();

            var ingredients = new List<IngredientEntity>();

            await foreach (Page<IngredientEntity> page in queryResults.AsPages())
            {
                var result = page.Values;

                ingredients.AddRange(result.ToList());

                break;
            }

            return ingredients;
        }

        public async Task<ICollection<RecipeEntity>> GetRecipes()
        {
            if (TableClient == null) throw new NullReferenceException("TableClient");

            var queryResults = TableClient.QueryAsync<RecipeEntity>();

            var recipes = new List<RecipeEntity>();

            await foreach (Page<RecipeEntity> page in queryResults.AsPages())
            {
                var result = page.Values;

                recipes.AddRange(result.ToList());

                break;
            }

            return recipes;
        }
    }
}
