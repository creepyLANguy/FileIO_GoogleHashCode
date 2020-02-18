using System.Collections.Generic;

namespace GoogleHashCode
{
    class Logic
    {
        public static string Run(int maxSlices, int types, List<string> pizzas)
        {
            string buffer = "";

            List<int> pizzaList = Helpers.StringToList<int>(pizzas[0]);

            int sum = 0;
            int typesUsed = 0;
            string pizzasUsed = "";

            for (int i = pizzaList.Count - 1; i >= 0; --i)
            {
                if ((sum + pizzaList[i]) <= maxSlices)
                {
                    sum += pizzaList[i];
                    pizzasUsed = i + " " + pizzasUsed;
                    ++typesUsed;
                }
            }

            buffer += typesUsed + "\n";
            buffer += pizzasUsed;
            return buffer;
        }

    }
}
