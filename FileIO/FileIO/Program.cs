using System;
using System.Collections.Generic;

namespace FileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputFileNames = {
                "small",
                "medium",
                "large",
                "larger"
            };

            foreach(string fileName in inputFileNames)
            {
                List<string> list = FileIO.Read(fileName);

                string resultsBuffer = "";
                //Process the list and assign results to resultsBuffer or something... 

                FileIO.Write(resultsBuffer, fileName);
            }
        }
    }
}
