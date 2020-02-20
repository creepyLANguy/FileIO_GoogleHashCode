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
                };

            string inputPath = "hashcode";

            string outputPath = "results";
            string outputExtension = ".out";

            foreach (string filename in inputFilenames)
            {
                List<int> propertiesList =
                    Helpers.StringToList<int>(FileIO.Read_FirstLine(filename, relativePath: inputPath));

                List<string> librariesList =
                    FileIO.Read(filename, relativePath: inputPath, ignoreFirstLine: true);

                string bookScores = librariesList[0];

                librariesList.RemoveAt(0);

                string resultsBuffer =
                    Logic.Run(propertiesList, bookScores, librariesList);

                FileIO.Write(resultsBuffer, filename, outputExtension, relativePath: outputPath);
            }
        }
    }
}