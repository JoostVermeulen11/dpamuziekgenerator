using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Tokenizer.checkers
{
    class NumberChecker : ITokenChecker
    {
        public bool canhandle(string input)
        {
            int n;
            return int.TryParse(input, out n);
        }

        public Token handle(string input)
        {
            return new Token(TokenType.Number, input);
        }
    }
}
