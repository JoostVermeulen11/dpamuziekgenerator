using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Tokenizer.checkers
{
    class RustChecker : ITokenChecker
    {
        public bool canhandle(string input)
        {
            return input.Contains('r') && input.Length == 2;
        }

        public Token handle(string input)
        {
            return new Token(TokenType.Rust, input);
        }
    }
}
