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
    class MaatExpression : IExpression
    {
        public IExpression clone()
        {
            return new MaatExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Value.type == TokenType.Maatstreep)
            {
                context.musicSheet.addmusicSymbol(new MaatStreep());
            }
        }
    }
}
