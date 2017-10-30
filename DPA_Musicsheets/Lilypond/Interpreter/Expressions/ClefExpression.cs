using DPA_Musicsheets.classes;
using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter.Expressions
{
    class ClefExpression : IExpression
    {
        private Dictionary<String, ClefType> clefLookup = new Dictionary<string, ClefType>();

        public ClefExpression()
        {
            clefLookup.Add("treble", ClefType.GClef);
            clefLookup.Add("bass", ClefType.FClef);
            clefLookup.Add("alto", ClefType.CClef);
        }

        public IExpression clone()
        {
            return new ClefExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Previous.Value.type == TokenType.Clef && token.Value.type == TokenType.ClefType)
            {
                Clef clef = new Clef(clefLookup[token.Value.value], 2);
                context.musicSheet.addmusicSymbol(clef);
            }
        }
    }
}
