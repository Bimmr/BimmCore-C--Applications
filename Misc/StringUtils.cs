using System.Linq;

namespace BimmrCore.Misc
{

    public static class StringUtils
    {

        /// <summary>
        /// Check if the word contains any letters
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool containsOnlyLetters(string word)
        {
            bool foundNonLetter = word.Any(ch => !char.IsLetter(ch));
            return !foundNonLetter;
        }

    }
}
