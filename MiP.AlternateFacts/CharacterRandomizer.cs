using System;
using System.Collections.Generic;

namespace MiP.AlternateFacts
{
    public class CharacterRandomizer
    {
        private static readonly char[] AlphaNumericCharacters =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static readonly char[] AlphaNumericLowerCharacters =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        private readonly Randomizer _randomizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRandomizer"/> class.
        /// </summary>
        public CharacterRandomizer()
        {
            _randomizer = new Randomizer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRandomizer"/> class.
        /// </summary>
        /// <param name="randomizer">A randomizer to use for randomization.</param>
        public CharacterRandomizer(Randomizer randomizer)
        {
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
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
        /// Returns an alphanumeric character.
        /// </summary>
        /// <param name="alphaNums">Defines which kinds of alphanumerics to return.</param>
        public char AlphaNumeric(AlphaNums alphaNums = AlphaNums.All)
        {
            if ((alphaNums & AlphaNums.All) == 0)
                throw new ArgumentOutOfRangeException(nameof(alphaNums), "alphaNums must be a valid value of the AlphaNums enum.");

            switch (alphaNums)
            {
                case AlphaNums.Numeric:
                    return _randomizer.PickFrom(AlphaNumericCharacters, 0, 9);

                case AlphaNums.AlphaNumericLower:
                    return _randomizer.PickFrom(AlphaNumericLowerCharacters);

                case AlphaNums.AlphaNumericUpper:
                    return _randomizer.PickFrom(AlphaNumericCharacters, maxIndex: 35);

                case AlphaNums.AlphaLower:
                    return _randomizer.PickFrom(AlphaNumericLowerCharacters, 10);

                case AlphaNums.AlphaUpper:
                    return _randomizer.PickFrom(AlphaNumericCharacters, 10, 10 + 25);

                case AlphaNums.Alpha:
                    return _randomizer.PickFrom(AlphaNumericCharacters, 10);

                default: // Alpha.All
                    return _randomizer.PickFrom(AlphaNumericCharacters);
            }
        }

        /// <summary>
        /// Returns <paramref name="count"/> alphanumeric characters.
        /// </summary>
        /// <param name="alphaNums">Defines which kinds of alphanumerics to return.</param>
        /// <param name="count">Defines how many characters to return.</param>
        public IEnumerable<char> AlphaNumerics(AlphaNums alphaNums = AlphaNums.All, int count = 4)
        {
            for (var i = 0; i < count; i++)
            {
                yield return AlphaNumeric(alphaNums);
            }
        }
    }
}