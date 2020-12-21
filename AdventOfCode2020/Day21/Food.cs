using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Food
    {
        List<Ingredient> ingredients;

        public Food(List<Ingredient> ing)
        {
            ingredients = ing;
        }

        public List<Ingredient> GetIngredients()
        {
            return ingredients;
        }

        public bool HasIngredient(Ingredient i)
        {
            return ingredients.Contains(i);
        }
    }
}
