using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode
{
    class Helpers
    {
        public static Dictionary<K, V> ListToDictionary<K, V, L>(List<string> list, string keyValDelimiter)
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>();

            if (keyValDelimiter == null || keyValDelimiter.Length == 0)
            {
                keyValDelimiter = Defaults.DELIMITER_KEYVAL;
            }

            foreach (string line in list)
            {
                int delimIndex = line.IndexOf(keyValDelimiter);

                string key_string = line.Substring(0, delimIndex);
                K key = TConverter.ChangeType<K>(key_string);

                string value_string = line.Substring(delimIndex + keyValDelimiter.Length);
                V value = TConverter.ChangeType<V>(value_string);

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
