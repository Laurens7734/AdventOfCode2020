using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Allergen: IComparable<Allergen>
    {
        List<Food> isIn;
        public string name;
        public Ingredient causedBy;

        public Allergen(string nm)
        {
            name = nm;
            isIn = new List<Food>();
            causedBy = null;
        }

        public void AddFood(Food f)
        {
            isIn.Add(f);
        }

        public bool FindIngredient()
        {
            Dictionary<Ingredient, int> count = new Dictionary<Ingredient, int>();
            foreach (Ingredient i in isIn[0].GetIngredients())
            {
                if(!i.CausesaAllergie())
                    count.Add(i, 1);
            }
            
            foreach(Food f in isIn)
            {
                foreach(Ingredient i in f.GetIngredients())
                {
                    if (count.ContainsKey(i))
                    {
                        count[i]++;
                    }
                }
            }

            KeyValuePair<Ingredient, int> top = new KeyValuePair<Ingredient, int>(new Ingredient("water"),0);
            bool conflict = false;
            foreach(KeyValuePair<Ingredient,int> k in count)
            {
                if (k.Value > top.Value)
                {
                    top = k;
                    conflict = false;
                }
                else if(k.Value == top.Value)
                {
                    conflict = true;
                }
            }

            if (conflict)
            {
                return false;
            }
            else
            {
                causedBy = top.Key;
                causedBy.AddAlergen(this);
                return true;
            }
        }

        public int CompareTo(Allergen a)
        {
            return name.CompareTo(a.name);
        }
    }
}
