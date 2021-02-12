// Test argument parsing.

using Xunit;

using GwLineCounter;

namespace GwLineCounterTest
{
    public class ArgParserTests
    {
        string defaultRoot = ".";
        string defaultFileSpec = "*.*";
        
        [Fact]
        public void NoArguments_UsesDefaults()
        {
            ArgParser myParser = new ArgParser();
            //string[] args = new string[0];
            string[] args = {};
            bool actualResult = myParser.ParseArgs(args);

            Assert.True(actualResult);
            Assert.Equal(defaultRoot, myParser.rootDir);
            Assert.Equal(defaultFileSpec, myParser.fileSpec);
        }

        [Fact]
        public void OneArgument_UsesArg()
        {
            ArgParser myParser = new ArgParser();
            string[] args = {"mydir"};
            bool actualResult = myParser.ParseArgs(args);

            Assert.True(actualResult);
            Assert.Equal("mydir", myParser.rootDir);
            Assert.Equal(defaultFileSpec, myParser.fileSpec);
        }

        [Fact]
        public void TwoArguments_UsesArgs()
        {
            ArgParser myParser = new ArgParser();
            string[] args = {"mydir", "*.csv"};
            bool actualResult = myParser.ParseArgs(args);

            Assert.True(actualResult);
            Assert.Equal("mydir", myParser.rootDir);
            Assert.Equal("*.csv", myParser.fileSpec);
        }

        [Fact]
        public void ThreeArguments_Fails()
        {
            ArgParser myParser = new ArgParser();
            string[] args = {"mydir", "*.csv", "huh?"};
            bool actualResult = myParser.ParseArgs(args);

            Assert.False(actualResult);
        }

    }
}
