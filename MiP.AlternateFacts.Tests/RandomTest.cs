using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace MiP.AlternateFacts.Tests
{
    public abstract class RandomTest
    {
        protected static void CanReturn<T>(Expression<Func<T>> randomExpression, params T[] expectedValue)
        {
            var expected = new HashSet<T>(expectedValue);

            var random = randomExpression.Compile();

            for (var i = 0; i < 1000; i++)
            {
                var current = random();
                Console.WriteLine(current);

                if (expected.Contains(current))
                {
                    expected.Remove(current);
                    if (expected.Count == 0)
                        return;
                }
            }

            Assert.Fail(randomExpression + " did never return expected values [{0}].", string.Join(",", expected));
        }

        protected static void NeverReturns<T>(Expression<Func<T>> randomExpression, params T[] notExpectedValues)
        {
            var random = randomExpression.Compile();

            for (var i = 0; i < 1000; i++)
            {
                var current = random();
                Console.WriteLine(current);

                var unexpected = notExpectedValues.Where(v => v.Equals(current)).ToArray();

                if (unexpected.Length > 0)
                    Assert.Fail(randomExpression + " returned unexpected value [{0}].", unexpected[0]);
            }
        }
        
        protected static void ReturnsChecked<T>(Expression<Func<T>> randomExpression, Expression<Func<T, bool>> checkExpression)
        {
            var random = randomExpression.Compile();
            var check = checkExpression.Compile();

            for (var i = 0; i < 1000; i++)
            {
                var current = random();
                Console.WriteLine(current);

                var ok = check(current);
                
                if (!ok)
                    Assert.Fail(randomExpression + " returned unexpected value [{0}].", current);
            }
        }

    }
}