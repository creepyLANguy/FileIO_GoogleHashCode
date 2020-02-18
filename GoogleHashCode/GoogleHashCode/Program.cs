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
                List<string> propertiesList =
                    Helpers.StringToList<string>(FileIO.Read_FirstLine(filename, relativePath: inputPath));

                List<string> valuesList =
                    FileIO.Read(filename, relativePath: inputPath, ignoreFirstLine: true);

                string resultsBuffer =
                    Logic.Run(propertiesList, valuesList);

                FileIO.Write(resultsBuffer, filename, outputExtension, relativePath: outputPath);
            }
        }
    }
}