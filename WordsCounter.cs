using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBSTest
{
    internal class WordsCounter
    {
        const char delimeter = ' ';

        //list of the common chars that normally come at the end of words
        char[] wordTerminators= new char[] {'.', ',', '?', '!'};

        //this method returns the words and their count based on the input
        //incase if an invalid input is supplied this will throw invalid input exception
        public Dictionary<string, int> CountWords(string input)
        {
            if (!IsValidInput(input))
                throw new InvalidInputException("Invalid input supplied.");

            Dictionary<string, int> toReturn = new Dictionary<string, int>();

            //change the new line with empty space so paragraphs can be dealt with
            input = input.Replace(System.Environment.NewLine, " ").Trim();

            string[] tokens = SplitInput(input);

            foreach (string token in tokens)
            {
                string tokenCopy = token.ToLower().Trim(wordTerminators);

                //there must at least be one char as key, this handles the situation where the 
                //word only contains any of the word terminator
                if (tokenCopy.Length >= 1)
                {
                    //if the word already in our dictionary, increment its count
                    if (toReturn.ContainsKey(tokenCopy))
                    {
                        toReturn[tokenCopy]++;
                    }
                    //if this is the first appearance of the word, start its count with 1
                    else
                    {
                        toReturn.Add(tokenCopy, 1);
                    }
                }
            }

            return toReturn;
        }

        //validate the input
        bool IsValidInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        //split the input based on the split character

        string[] SplitInput(string input)
        {
            return input.Split(new char[]{delimeter});
        }

    }

    internal class InvalidInputException : Exception
    {
        public InvalidInputException() { }

        public InvalidInputException(string message) : base(message){}
    }
}
