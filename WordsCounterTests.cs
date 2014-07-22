using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UBSTest
{
    [TestFixture]
    internal class WordsCounterTests
    {
        [Test]
        [ExpectedException(typeof(InvalidInputException))]
        public void InvalidInputTest()
        {
            WordsCounter counter = new WordsCounter();
            var results = counter.CountWords(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidInputException))]
        public void EmptyInputTest()
        {
            WordsCounter counter = new WordsCounter();
            var results = counter.CountWords("      ");
        }

        [Test]
        public void SingleCharTest()
        {
            WordsCounter counter = new WordsCounter();
            Dictionary<string, int> results = counter.CountWords("a");

            Assert.AreEqual(1, results.Count);

            var enumerator = results.GetEnumerator();

            enumerator.MoveNext();
            KeyValuePair<string, int> kvp = enumerator.Current;
            Assert.AreEqual("a", kvp.Key);
            Assert.AreEqual(1, kvp.Value);

        }

        [Test]
        public void OnlyInvalidCharsTest()
        {
            WordsCounter counter = new WordsCounter();
            Dictionary<string, int> results = counter.CountWords(string.Format(". ! ? , {0}", System.Environment.NewLine));

            Assert.AreEqual(0, results.Count);
            
        }

        [Test]
        public void InputWithNewLineTest()
        {
            string input = string.Format("This is the first line.{0}This is the second line.", System.Environment.NewLine);
            WordsCounter counter = new WordsCounter();
            Dictionary<string, int> results = counter.CountWords(input);

            foreach (KeyValuePair<string, int> kvp in results)
            {
                switch(kvp.Key)
                {
                    case "this": 
                        Assert.AreEqual(kvp.Value, 2); 
                        break;
                    case "is":
                        Assert.AreEqual(kvp.Value, 2);
                        break;
                    case "the":
                        Assert.AreEqual(kvp.Value, 2);
                        break;
                    case "first":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                    case "line":
                        Assert.AreEqual(kvp.Value, 2);
                        break;
                    case "second":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                }
            }

        }

        [Test]
        public void ValidInputTest()
        {
            string input = "This is a statement, and so is this";
            WordsCounter counter = new WordsCounter();
            Dictionary<string, int> results = counter.CountWords(input);

            foreach (KeyValuePair<string, int> kvp in results)
            {
                switch (kvp.Key)
                {
                    case "this":
                        Assert.AreEqual(kvp.Value, 2);
                        break;
                    case "is":
                        Assert.AreEqual(kvp.Value, 2);
                        break;
                    case "a":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                    case "statement":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                    case "and":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                    case "so":
                        Assert.AreEqual(kvp.Value, 1);
                        break;
                }
            }
        }
    }
}
