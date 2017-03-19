using System;
using System.Collections.Generic;

namespace MiP.AlternateFacts
{
    /// <summary>
    /// Declares how characters should be replaced with <see cref="TextRandomizer.StringFromTemplate"/>.
    /// </summary>
    public class StringTemplateSettings
    {
        private readonly Dictionary<char, AlphaNums> _replaceChars = new Dictionary<char, AlphaNums>();

        private readonly Dictionary<char, Func<string>> _replaceFuncs = new Dictionary<char, Func<string>>();

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="useDefaults">
        /// When false, initializes the following default replacements:
        /// <code>
        /// # ... Numeric
        /// ? ... Alpha
        /// * ... AlphaNumeric
        /// N ... AlphaNumericUpper
        /// n ... AlphaNumericLower
        /// A ... AlphaUpper
        /// a ... AlphaLower
        /// </code>
        /// When false, no replacements are defined.
        /// </param>
        public StringTemplateSettings(bool useDefaults = true)
        {
            if (useDefaults)
            {
                _replaceChars['#'] = AlphaNums.Numeric;
                _replaceChars['?'] = AlphaNums.Alpha;
                _replaceChars['*'] = AlphaNums.AlphaNumeric;
                _replaceChars['N'] = AlphaNums.AlphaNumericUpper;
                _replaceChars['n'] = AlphaNums.AlphaNumericLower;
                _replaceChars['A'] = AlphaNums.AlphaUpper;
                _replaceChars['a'] = AlphaNums.AlphaLower;
            }
        }

        /// <summary>
        /// Removes a character replacement rule, the character will not be replaced (and produce itself).
        /// </summary>
        /// <param name="charNotToReplace">The character to remove the replacement rule for.</param>
        public StringTemplateSettings DoNotReplace(char charNotToReplace)
        {
            if (_replaceChars.ContainsKey(charNotToReplace))
                _replaceChars.Remove(charNotToReplace);
            if (_replaceFuncs.ContainsKey(charNotToReplace))
                _replaceFuncs.Remove(charNotToReplace);

            return this;
        }

        /// <summary>
        /// Adds a rule to replace a character with a random alphanumeric character defined by <see cref="AlphaNums"/>.
        /// </summary>
        /// <param name="replaceChar">The character to create the replacement rule for.</param>
        /// <param name="alphaNums">Defines which alphanumeric characters may be a replacement.</param>
        public StringTemplateSettings Replace(char replaceChar, AlphaNums alphaNums)
        {
            _replaceChars[replaceChar] = alphaNums;
            return this;
        }

        /// <summary>
        /// Adds a rule to replace a character with a string returned by the <paramref name="replaceWithFunc"/>.
        /// </summary>
        /// <param name="replaceChar">The character to create the replacement rule for.</param>
        /// <param name="replaceWithFunc"> Produces the string which will replace the character.</param>
        public StringTemplateSettings Replace(char replaceChar, Func<string> replaceWithFunc)
        {
            _replaceFuncs[replaceChar] = replaceWithFunc;
            return this;
        }

        internal IEnumerable<char> Replace(char replaceChar, Func<AlphaNums, char> alphaNumFunc)
        {
            if (_replaceChars.ContainsKey(replaceChar))
            {
                yield return alphaNumFunc(_replaceChars[replaceChar]);
            }

            else if (_replaceFuncs.ContainsKey(replaceChar))
            {
                var str = _replaceFuncs[replaceChar]();
                foreach (var ch in str)
                {
                    yield return ch;
                }
            }

            else
            {
                yield return replaceChar;
            }
        }
    }
}