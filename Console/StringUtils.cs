/*
 *
 * File Name: StringUtils - BimmrUtilities
 * 
 * Description: Utilities class to make Strings easier to work with
 * 
 * Revisions:
 *          11/25/15 - Randy Bimm - Created Class
 * 
 */
using System;
using System.Linq;

namespace BimmrCore.Console
{

    public static class StringUtils
    {
        public static Boolean containsOnlyLetters(string word)
        {
            Boolean foundNonLetter = word.Any(ch => !char.IsLetter(ch));
            return !foundNonLetter;
        }

    }
}
