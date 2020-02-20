using System;
using System.Collections.Generic;
using System.IO;

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
         * Read in just the forst line (usually contains properties)
         * Ignore the first line (properties) and read rest of file
         * 
         * Save file
         * Save location default (execution path) or relative path to execution path
         * Output filename default to input filename plus suffix, or specifiy output filename
         * Output file extension default (.in?) or specify extension
         *
         * Implicit Error handling
         */

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
            if (extension.Length > 0 && extension[0] != Defaults.DOT)
            {
                extension += Defaults.DOT;
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
            string relativePath = Defaults.PATH,
            bool ignoreFirstLine = false
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
            catch (Exception e)
            {
                Console.WriteLine("FAILED TO OPEN " + fullPath
                    + "\r\n\r\n" + e.ToString());
            }

            if (ignoreFirstLine)
            {
                list.RemoveAt(0);
            }

            return list;
        }


        public static Dictionary<K, V> Read<K, V>(
            string keyValDelimiter,
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH,
            bool ignoreFirstLine = false
            )
        {
            List<string> list = Read(filename, extension, relativePath, ignoreFirstLine);
            return Helpers.ListToDictionary<K, V, string>(list, keyValDelimiter);
        }


        public static string Read_FirstLine(
            string filename,
            string extension = Defaults.EXTENSION,
            string relativePath = Defaults.PATH
            )
        {
            //I know this is silly, but I don't want to duplicate the Read code.
            List<string> list = Read(filename, extension, relativePath, false);
            return list[0];
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
                Console.WriteLine(
                    "FAILED TO WRITE TO " + fullPath + "\r\n" + e.ToString());

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

}
