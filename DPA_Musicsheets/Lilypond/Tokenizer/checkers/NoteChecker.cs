using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Tokenizer.checkers
{
    class NoteChecker : ITokenChecker
    {
        private char[] noteLookup = { 'c', 'd', 'e', 'f', 'g', 'a', 'b', '\'', ',', '~', '.', 'i', 's' };
        public bool canhandle(string input)
        {
            input = input.ToLower();

            if (input.Length >= 2 && !input.Contains("="))
            {
                int counter = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (isNumber(input[i].ToString()))
                    {
                        counter++;
                    }
                    else if (noteLookup.Contains(input[i]))
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter >= 2)
                {
                    return true;
                }
            }

            return false;
        }

        public Token handle(string input)
        {
            return new Token(TokenType.Note, input);
        }

        private Boolean isNumber(String input)
        {
            int n;
            return int.TryParse(input, out n);
        }
    }
}
