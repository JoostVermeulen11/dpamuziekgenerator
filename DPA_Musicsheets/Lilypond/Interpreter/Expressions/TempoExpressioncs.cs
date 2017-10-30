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
    class TempoExpression : IExpression
    {
        public IExpression clone()
        {
            return new TempoExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Previous.Value.type == TokenType.Tempo)
            {
                String[] tempoArr = token.Value.value.Split('=');
                context.musicSheet.addmusicSymbol(new Tempo(Convert.ToInt16(tempoArr[1]), Convert.ToInt16(tempoArr[0])));
            }
        }
    }
}
