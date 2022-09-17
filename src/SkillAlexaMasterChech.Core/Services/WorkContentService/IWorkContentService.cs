using SkillAlexaMasterChech.Core.Models.DataTableEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAlexaMasterChech.Core.Services.WorkContentService
{
    public interface IWorkContentService
    {
        Task<string> LoadRecipeForAlexa();
        Task<ICollection<IngredientEntity>> GetIngredients();
    }
}
