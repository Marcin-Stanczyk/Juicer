using Juicer.Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Data
{
    interface IRecipeData
    {
        IEnumerable<Recipe> GetRecipesByName(string name);

        Recipe GetRecipeById(int id);

        Recipe Update(Recipe updatedRecipe);

        Recipe Add(Recipe newRecipe);

        Recipe Delete(int id);

        int Commit();

    }
}
