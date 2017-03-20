using System;
using System.Collections.Generic;
using System.Linq;

namespace MiP.AlternateFacts
{
    public class TextRandomizer
    {
        private static readonly StringTemplateSettings DefaultStringTemplateSettings = new StringTemplateSettings();

        private readonly Randomizer _randomizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRandomizer"/> class.
        /// </summary>
        public TextRandomizer()
        {
            _randomizer = new Randomizer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRandomizer"/> class.
        /// </summary>
        /// <param name="randomizer">A randomizer to use for randomization.</param>
        public TextRandomizer(Randomizer randomizer)
        {
            if (randomizer == null)
                throw new ArgumentNullException(nameof(randomizer));

            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets a random character between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The lowest char of the range to get (lower bound).</param>
        /// <param name="max">The highest char of the range to get (upper bound).</param>
        public char Char(char min = char.MinValue, char max = char.MaxValue)
        {
            return (char) _randomizer.Random.Next(min, max + 1);
        }

        /// <summary>
        /// Gets an array of random <see cref="char"/>.
        /// </summary>
        /// <param name="min">The lowest <see cref="char"/> to get.</param>
        /// <param name="max">The highest <see cref="char"/> to get.</param>
        /// <param name="count">The number of characters to get.</param>
        public IEnumerable<char> Chars(char min = char.MinValue, char max = char.MaxValue, int count = 4)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Char(min, max);
            }
        }


        /// <summary>
        /// Returns a random string containing <paramref name="min"/> and <paramref name="max"/> characters, 
        /// picking random characters from <paramref name="allowedChars"/>.
        /// </summary>
        /// <param name="allowedChars">Defines the allowed characters for the string.</param>
        /// <param name="min">Minimum number of characters for the string. Zero is allowed. Default=1.</param>
        /// <param name="max">Maximum number of characters for the string.</param>
        public string String(string allowedChars, int min = 1, int max = 16)
        {
            var count = _randomizer.Int32(min, max);

            var chars = Enumerable.Range(1, count)
              .Select(_ => _randomizer.PickFrom(allowedChars))
              .ToArray();

            return new string(chars);
        }
        
        /// <summary>
        /// Replaces special characters in a template using the <paramref name="settings"/> for rules how to replace the characters.
        /// </summary>
        /// <param name="format">The string used as a template. Special characters in the string are replaced to produce a new random string from the template.</param>
        /// <param name="settings">Defines rules on how to replace characters.</param>
        /// <remarks>
        /// The default replacements are
        /// <code>
        /// # ... Numeric
        /// ? ... Alpha
        /// * ... AlphaNumeric
        /// N ... AlphaNumericUpper
        /// n ... AlphaNumericLower
        /// A ... AlphaUpper
        /// a ... AlphaLower
        /// </code>
        /// </remarks>
        public string StringFromTemplate(string format, StringTemplateSettings settings = null)
        {
            settings = settings ?? DefaultStringTemplateSettings;

            var chars = format.SelectMany(c => settings.Replace(c, _randomizer)).ToArray();

            return new string(chars);
        }
    }
}