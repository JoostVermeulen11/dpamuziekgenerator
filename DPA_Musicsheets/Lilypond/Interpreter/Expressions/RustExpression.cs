using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter.Expressions
{
    class RustExpression : IExpression
    {
        public IExpression clone()
        {
            return new RustExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            RustNote note = new RustNote();
            string input = token.Value.value;
            note.octaaf = context.musicSheet.startOctaaf;
            if (input[input.Length - 1] == '}')
            {
                input = input.Remove(input.Length - 1);
            }
            note.duur = Convert.ToInt16(input.Substring(1));
            context.musicSheet.addmusicSymbol(note);
        }
    }
}
