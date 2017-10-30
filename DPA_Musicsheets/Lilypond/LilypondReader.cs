using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using DPA_Musicsheets.Lilypond.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond
{
    class LilypondReader : IInputReader
    {
        public MusicSheet readNotes(string data)
        {
            LilypondTokenizer tokenizer = new LilypondTokenizer();
            tokenizer.proces(data);
            LinkedList<Token> tokens = tokenizer.getTokens();
            LilypondInterpreter interpreter = new LilypondInterpreter();
            return interpreter.process(tokens);
        }

        public IInputReader clone()
        {
            return new LilypondReader();
        }
    }
}
