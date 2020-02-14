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

using System;
using System.IO;
using System.Collections.Generic;

namespace FileIO
{
    struct Defaults
    {
        public const string EXTENSION = ".in";
        public const string PATH = "";
        public const string DELIMITER = ",";
    };


    class FileIO
    {
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
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            return Read_As_List(name, extension, relativePath);
        }


        public static List<string> Read_As_List(
            string name,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            string fullPath = GetFullPath(name, extension, relativePath);

            //if (File.Exists(fullPath) == false)
            //{
            //    throw new Exception("FILE NOT FOUND : " + fullPath);
            //}

            List<string> list = new List<string>();
            try
            {
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


        public static Dictionary<string, string> Read_As_Dictionary(
            string name,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH,
            string delim = Defaults.DELIMITER
            )
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            List<string> list = Read_As_List(name, extension, relativePath);
            foreach(string s in list)
            {
                int delimIndex = s.IndexOf(delim);
                string key = s.Substring(0, delimIndex);
                string value = s.Substring(delimIndex + 1);
                dictionary.Add(key, value);
            }

            return dictionary;
        }


        public static bool Write(
            string s,
            string name,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH,
            bool append = false
            )
        {
            string fullPath = GetFullPath(name, extension, relativePath);
            try
            {
                Console.WriteLine("Attempting to write to " + fullPath);
                if (append)
                {
                    File.AppendAllText(fullPath, s);
                }
                else
                {
                    File.WriteAllText(fullPath, s);
                }
                Console.WriteLine("Successfully written to " + fullPath);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED TO WRITE TO " + fullPath
                    + "\r\n\r\n" + e.ToString());

                return false;
            }
        }


        public static bool Append(
            string s,
            string name,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            return Write(s, name, extension, relativePath, true);
        }

    }
}
