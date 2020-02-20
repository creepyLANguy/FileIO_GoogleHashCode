using System;
using System.Collections.Generic;

namespace GoogleHashCode
{
    class Logic
    {
        public static string Run(int maxSlices, int types, List<string> pizzas)
        {
            string buffer = "";

            List<int> pizzaList = Helpers.StringToList<int>(pizzas[0]);


            int maxSum = 0;

            for (int runner = pizzaList.Count - 1; runner >= 0; --runner)
            {
                int sum = 0;
                int typesUsed = 0;
                string pizzasUsed = "";

                for (int i = runner; i >= 0; --i)
                {
                    if ((sum + pizzaList[i]) <= maxSlices)
                    {
                        sum += pizzaList[i];
                        pizzasUsed = i + " " + pizzasUsed;
                        ++typesUsed;
                    }
                }

                if (sum >= maxSum)
                {
                    maxSum = sum;
                    buffer = typesUsed + "\n" + pizzasUsed;
                    //Console.WriteLine(maxSum + "/" + maxSlices);

                    if (sum == maxSlices)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(maxSum + "/" + maxSlices);
            return buffer;
        }

    }
}
