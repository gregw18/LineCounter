// Interface and implementation for counting number of non-blank lines in a file.

using System;
using Xunit;

using GwLineCounterTest;

namespace testCounter
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2+2);
        }

        [Fact]
        public void FailingText()
        {
            Assert.Equal(5, 2+2);
        }
    }
}
