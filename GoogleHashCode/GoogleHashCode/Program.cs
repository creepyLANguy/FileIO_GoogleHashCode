using System.Collections.Generic;

namespace GoogleHashCode
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] inputFilenames =
                {
                    "a_example",
                    "b_small",
                    "c_medium",
                    "d_quite_big",
                    "e_also_big"
                };

            string inputPath = "hashcode";

            string outputPath = "results";
            string outputExtension = ".txt";

            foreach (string filename in inputFilenames)
            {
                List<int> propertiesList =
                    Helpers.StringToList<int>(FileIO.Read_FirstLine(filename, relativePath: inputPath));

                List<string> pizzas =
                    FileIO.Read(filename, relativePath: inputPath, ignoreFirstLine: true);

                string resultsBuffer =
                    Logic.Run(propertiesList[0], propertiesList[1], pizzas);

                FileIO.Write(resultsBuffer, filename, outputExtension, relativePath: outputPath);
            }
        }
    }
}