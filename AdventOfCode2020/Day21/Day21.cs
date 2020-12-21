using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day21:Day
    {
        Dictionary<string, Allergen> allergens = new Dictionary<string, Allergen>();
        Dictionary<string, Ingredient> ingredientMap = new Dictionary<string, Ingredient>();
        List<Food> allFoods = new List<Food>();
        public Day21(List<string> d)
        {
            foreach(string s in d)
            {
                string[] split = s.Split(" (contains ");
                string[] ingredients = split[0].Split(' ');
                string[] allergies = split[1].Substring(0, split[1].Length-1).Split(", ");
                List<Ingredient> ingredient = new List<Ingredient>();
                foreach(string q in ingredients)
                {
                    if (ingredientMap.ContainsKey(q))
                    {
                        ingredient.Add(ingredientMap[q]);
                    }
                    else
                    {
                        Ingredient i = new Ingredient(q);
                        ingredient.Add(i);
                        ingredientMap.Add(q, i);
                    }
                }
                Food food = new Food(ingredient);
                allFoods.Add(food);
                foreach(string t in allergies)
                {
                    if (allergens.ContainsKey(t)){
                        allergens[t].AddFood(food);
                    }
                    else
                    {
                        Allergen a = new Allergen(t);
                        a.AddFood(food);
                        allergens.Add(t, a);
                    }
                }
            }
            bool searching = true;
            List < Allergen > needIngredient = new List<Allergen>(allergens.Values);
            while (searching) 
            {
                List<Allergen> temp = new List<Allergen>();
                foreach (Allergen a in needIngredient)
                {
                    if (!a.FindIngredient())
                        temp.Add(a);
                }
                if (temp.Count == 0)
                    searching = false;
                else
                    needIngredient = temp;
            }
        }
        public string Answer1()
        {
            int count = 0;
            foreach(KeyValuePair<string,Ingredient> k in ingredientMap)
            {
                if (!k.Value.CausesaAllergie())
                {
                    foreach(Food f in allFoods)
                    {
                        if (f.HasIngredient(k.Value))
                            count++;
                    }
                }
                    
            }
            return count.ToString();
        }
        public string Answer2()
        {
            StringBuilder sb = new StringBuilder();
            List<Allergen> all = new List<Allergen>(allergens.Values);
            all.Sort();
            foreach(Allergen a in all)
            {
                sb.Append(a.causedBy.Name);
                sb.Append(",");
            }
            return sb.ToString().Substring(0,sb.Length-1);
        }
    }
}
