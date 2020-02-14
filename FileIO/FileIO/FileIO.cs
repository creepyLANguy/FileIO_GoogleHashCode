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

//AL.
//Also make sure you develop a batch runner for input of multiple files, runs, and named file outputs
//

using System;
using System.IO;
using System.Collections.Generic;

namespace FileIO
{
    class FileIO
    {
        private const string DEFAULT_INPUT_EXTENSION = ".in";
        private const string DEFAULT_INPUT_PATH = "";
        private const string DEFAULT_INPUT_DELIMITER = ",";

        private static string GetFullPath(
            string name,
            string extension,
            string relativePath
            )
        {
            return relativePath + name + extension;
        }

        public static List<string> Read(
            string name,
            string extension = DEFAULT_INPUT_EXTENSION,
            string relativePath = DEFAULT_INPUT_PATH
            )
        {
            List<string> list = new List<string>();

            string fullPath = GetFullPath(name, extension, relativePath);

            if (File.Exists(fullPath) == false)
            {
                Console.WriteLine("CAN NOT FIND FILE : \r\n" + fullPath);
            }

            using (StreamReader sr = File.OpenText(fullPath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    list.Add(s);
                }
            }

            return list;
        }

        public static Dictionary<string, string> Read(
            string name,
            string extension = DEFAULT_INPUT_EXTENSION,
            string relativePath = DEFAULT_INPUT_PATH,
            string delim = DEFAULT_INPUT_DELIMITER
            )
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string fullPath = GetFullPath(name, extension, relativePath);

            if (File.Exists(fullPath) == false)
            {
                Console.WriteLine("CAN NOT FIND FILE : \r\n" + fullPath);
            }

            using (StreamReader sr = File.OpenText(fullPath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    int delimIndex = s.IndexOf(delim);
                    string key = s.Substring(0, delimIndex);
                    string value = s.Substring(delimIndex + 1);
                    dictionary.Add(key, value);
                }
            }

            return dictionary;
        }

        public static void Write(
            string s,
            string name,
            string extension = DEFAULT_INPUT_EXTENSION,
            string relativePath = DEFAULT_INPUT_PATH
            )
        {
            Append(s, name, extension, relativePath);
        }

        public static void Append(
            string s,
            string name,
            string extension = DEFAULT_INPUT_EXTENSION,
            string relativePath = DEFAULT_INPUT_PATH)
        {
            string fullPath = GetFullPath(name, extension, relativePath);
            File.AppendAllText(fullPath, s);
        }

    }
}
