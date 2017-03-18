using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiP.AlternateFacts.Tests
{
    public abstract class RandomTest
    {
        protected static void CanReturn<T>(Expression<Func<T>> randomExpression, T expectedValue)
        {
            var random = randomExpression.Compile();

            for (var i = 0; i < 1000; i++)
            {
                var current = random();
                Console.WriteLine(current);
                if (current.Equals(expectedValue))
                    return;
            }

            Assert.Fail(randomExpression + " did never return expected value [{0}].", expectedValue);
        }

        protected static void NeverReturns<T>(Expression<Func<T>> randomExpression, T notExpectedValue)
        {
            var random = randomExpression.Compile();

            for (var i = 0; i < 1000; i++)
            {
                var current = random();
                if (current.Equals(notExpectedValue))
                    Assert.Fail(randomExpression + " returned unexpected value [{0}].", notExpectedValue);
            }
        }
    }
}