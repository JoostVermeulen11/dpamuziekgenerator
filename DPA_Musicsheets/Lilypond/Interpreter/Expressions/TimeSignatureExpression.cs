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
    class TimeSignatureExpression : IExpression
    {
        public IExpression clone()
        {
            return new TimeSignatureExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Previous.Value.type == TokenType.timeSignature)
            {
                String[] splitValue = token.Value.value.Split('/');
                int[] timeSignature = new int[] { Convert.ToInt16(splitValue[0]), Convert.ToInt16(splitValue[1]) };
                context.musicSheet.addmusicSymbol(new TimeSignature(timeSignature));
            }
        }
    }
}
