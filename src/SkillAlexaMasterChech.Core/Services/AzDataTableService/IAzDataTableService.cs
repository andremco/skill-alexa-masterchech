using Azure.Data.Tables;
using SkillAlexaMasterChech.Core.Models.DataTableEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAlexaMasterChech.Core.Services.AzDataTableService
{
    public interface IAzDataTableService
    {
        TableClient TableClient { get; set; }
        Task<ICollection<IngredientEntity>> GetIngredients();
        Task<ICollection<RecipeEntity>> GetRecipes();
    }
}
