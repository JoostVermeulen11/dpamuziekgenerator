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
    class RepeatExpression : IExpression
    {
        public IExpression clone()
        {
            return new RepeatExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Next.Next.Value.type == TokenType.Number)
            {
                context["repeat"] = true;
                Repeater repeat = new Repeater();
                repeat.repeats = Convert.ToInt16(token.Next.Next.Value.value);
                context.musicSheet.addmusicSymbol(repeat);
            }
        }
    }
}
