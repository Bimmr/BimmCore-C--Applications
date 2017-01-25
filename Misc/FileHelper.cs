using System.IO;
using System.Collections.Generic;


namespace BimmCore.Misc
{
    /// <summary>
    /// FileHelper Class
    /// </summary>
    public class FileHelper
    {
        private string path;

        /// <summary>
        /// Create new instance of FileHelper
        /// </summary>
        /// <param name="path"></param>
        public FileHelper(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Create the file
        /// </summary>
        public void createFile()
        {
            File.WriteAllText(path, "");
        }
        /// <summary>
        /// Add a value to the file
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value associated with the key</param>
        public void add(string key, string value)
        {
            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine(key + ":" + value);
            tw.Close();
        }
        public void clear()
        {
            TextWriter tw = new StreamWriter(path, false);
            tw.WriteLine("");
            tw.Close();
        }

        /// <summary>
        /// Get a value from the filed
        /// </summary>
        /// <param name="key">The Key</param>
        /// <returns>The value associated with the key</returns>
        public string get(string key)
        {
            TextReader tr = new StreamReader(path, true);
            string line;
            while (!(line = tr.ReadLine()).StartsWith(key)) ;
            tr.Close();
            return line.Split(':')[1];
        }

        /// <summary>
        /// Get all items in the file
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> getAll()
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            TextReader tr = new StreamReader(path, true);
            string line;
            while ((line = tr.ReadLine()) != null)
                entries.Add(line.Split(':')[0], line.Split(':')[0]);
            tr.Close();
            return entries;
        }

        public List<string> getAllAsList()
        {
            List<string> items = new List<string>(); TextReader tr = new StreamReader(path, true);
            string line;
            while ((line = tr.ReadLine()) != null)
                items.Add(line);
            tr.Close();
            return items;

        }
    }

}
