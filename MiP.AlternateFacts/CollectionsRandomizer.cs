using System.Collections.Generic;
using System.Linq;

namespace MiP.AlternateFacts
{
    public class CollectionsRandomizer
    {
        private Randomizer _randomizer;

        public CollectionsRandomizer(Randomizer randomizer)
        {
            _randomizer = randomizer;
        }
        
        /// <summary>
        /// Shuffles an IEnumerable source.
        /// </summary>
        public IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            return source.OrderBy(_ => _randomizer.Int32());
        }
    }
}