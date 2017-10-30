using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter.Expressions
{
    class EndblokExpression : IExpression
    {
        public IExpression clone()
        {
            return new EndblokExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (context["repeat"])
            {
                context["repeat"] = false;
            }
        }
    }
}
