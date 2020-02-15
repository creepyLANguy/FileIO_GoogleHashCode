using System;
using System.IO;
using System.Collections.Generic;

namespace GoogleHashCode
{
    class FileIO
    {
        /*
         * Requirements:
         * 
         * Read in a text file
         * Default location (execution path) or specify location relative to execution path
         * Default file extension (.in?) or specify file extension
         * Read lines into list
         * Read key value pairs into dictionary
         * Read kv pairs with default delimiter (','?) or specifiy delim
         * 
         * Save file
         * Save location default (execution path) or relative path to execution path
         * Output filename default to input filename plus suffix, or specifiy output filename
         * Output file extension default (.in?) or specify extension
         *
         * Implicit Error handling
         */

        private struct Defaults
        {
            public const string EXTENSION = ".in";
            public const string PATH = "";
            public const string DELIMITER = ",";
        };

        private static string GetFullPath(
            string filename,
            string extension,
            string relativePath
            )
        {
            if (Path.EndsInDirectorySeparator(relativePath) == false)
            {
                relativePath += Path.DirectorySeparatorChar;
            }
            if (extension.Length > 0 && extension[0] != '.')
            {
                extension += '.';
            }
            if (filename.Contains(extension))
            {
                extension = "";
            }

            return relativePath + filename + extension;
        }


        public static List<string> Read(
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            string fullPath = GetFullPath(filename, extension, relativePath);

            List<string> list = new List<string>();
            try
            {
                if (File.Exists(fullPath) == false)
                {
                    throw new Exception("FILE NOT FOUND : " + fullPath);
                }

                Console.WriteLine("Opening " + fullPath);
                using StreamReader sr = File.OpenText(fullPath);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    list.Add(s);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("FAILED TO OPEN " + fullPath
                    + "\r\n\r\n" + e.ToString());
            }

            return list;
        }


        public static Dictionary<string, string> Read(
            string keyValDelimiter,// = Defaults.DELIMITER
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (keyValDelimiter.Length == 0)
            {
                keyValDelimiter = Defaults.DELIMITER;
            }

            List<string> list = Read(filename, extension, relativePath);
            foreach(string line in list)
            {
                int delimIndex = line.IndexOf(keyValDelimiter);
                string key = line.Substring(0, delimIndex);
                string value = line.Substring(delimIndex + keyValDelimiter.Length);
                dictionary.Add(key, value);
            }

            return dictionary;
        }


        public static bool Write(
            string contents,
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH,
            bool appendOnly = false
            )
        {
            string fullPath = GetFullPath(filename, extension, relativePath);
            try
            {
                //if (Directory.Exists(relativePath) == false)
                {
                    Directory.CreateDirectory(relativePath);
                }

                Console.WriteLine("Attempting to write to " + fullPath);
                if (appendOnly)
                {
                    File.AppendAllText(fullPath, contents);
                }
                else
                {
                    File.WriteAllText(fullPath, contents);
                }
                Console.WriteLine("Successfully written to " + fullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED TO WRITE TO " + fullPath
                    + "\r\n\r\n" + e.ToString());

                return false;
            }

            return true;
        }


        public static bool Append(
            string contents,
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            return Write(contents, filename, extension, relativePath, true);
        }

    }


    class Program
    {
        private static string ProcessList(List<string> list)
        {
            string buffer = "";
            foreach (string s in list)
            {
                buffer += s + "\r\n";
            }
            return buffer;
        }


        public static void Main(string[] args)
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