using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode
{
    class Helpers
    {
        public static Dictionary<string, string> ListToDictionary(List<string> list, string keyValDelimiter)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (keyValDelimiter == null || keyValDelimiter.Length == 0)
            {
                keyValDelimiter = Defaults.DELIMITER_KEYVAL;
            }

            foreach (string line in list)
            {
                int delimIndex = line.IndexOf(keyValDelimiter);
                string key = line.Substring(0, delimIndex);
                string value = line.Substring(delimIndex + keyValDelimiter.Length);
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static List<T> StringToList<T>(string input, string delimiter = Defaults.DELIMITER_SPACE)
        {
            //return input.Split(delimiter).ToList<T>();

            List<string> l = input.Split(delimiter).ToList();

            List<T> list = new List<T>();
            foreach (string s in l)
            {
                list.Add(TConverter.ChangeType<T>(s));
            }
            return list;
        }
    }
}
