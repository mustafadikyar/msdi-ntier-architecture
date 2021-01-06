using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msdi.Core.Helpers
{
    /// <summary>
    /// Utility functions for characters
    /// </summary>
    public static class CharHelpers
    {

        /// <summary>
        /// Returns a random uppercase character
        /// </summary>
        /// <returns></returns>
        public static char GetRandomUppercaseCharacter()
        {
            var chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var randomNumber = IntHelpers.GetRandomNumber(chars.Length);

            return chars[randomNumber];
        }

        /// <summary>
        /// Returns a random lowercase character
        /// </summary>
        /// <returns></returns>
        public static char GetRandomLowercaseCharacter()
        {
            var chars = @"abcdefghijklmnopqrstuvwxyz";

            var randomNumber = IntHelpers.GetRandomNumber(chars.Length);

            return chars[randomNumber];
        }

        /// <summary>
        /// Returns a random lowercase character
        /// </summary>
        /// <returns></returns>
        public static char GetRandomSpecialCharacter()
        {
            var chars = @"!§$%&/()=_?#";

            var randomNumber = IntHelpers.GetRandomNumber(chars.Length);

            return chars[randomNumber];
        }

        /// <summary>
        /// Returns a random character
        /// </summary>
        /// <returns></returns>
        public static char GetRandomCharacter()
        {
            var chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!§$%&/()=_?#1234567890";

            var randomNumber = IntHelpers.GetRandomNumber(chars.Length);

            return chars[randomNumber];
        }

    }
}
