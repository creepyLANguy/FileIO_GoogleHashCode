using System;
using System.Collections.Generic;

namespace FileIO
{
    class Program
    {
        public static string ProcessList(List<string> list)
        {
            string buffer = "";
            foreach (var s in list)
            {
                buffer += s + "\r\n";
            }
            return buffer;
        }

        static void Main(string[] args)
        {
            string[] inputFileNames =
                {
                    "a_example",
                    "b_small",
                    "c_medium",
                    "d_quite_big",
                    "e_also_big"
                };

            string inputPath = "hashcode";

            string outputPath = "results";

            foreach (string fileName in inputFileNames)
            {
                List<string> list = FileIO.Read(fileName, relativePath: inputPath);

                string resultsBuffer = ProcessList(list);

                if (resultsBuffer.Length > 0)
                {
                    FileIO.Write(resultsBuffer, fileName, ".txt", relativePath: outputPath);
                }
            }
        }
    }
}
