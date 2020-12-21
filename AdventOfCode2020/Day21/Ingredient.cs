using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{ 
    class Ingredient
    {
        public string Name { get; }
        Allergen causes;

        public Ingredient(string nm)
        {
            Name = nm;
            causes = null;
        }

        public bool CausesaAllergie()
        {
            if (causes == null)
                return false;
            return true;
        }

        public void AddAlergen(Allergen a)
        {
            if (causes == null)
                causes = a;
            else
                throw new Exception("second allergen in this food");
        }

        public override bool Equals(object obj)
        {
            if(obj is Ingredient i)
            {
                return i.Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 16;
        }
    }
}
