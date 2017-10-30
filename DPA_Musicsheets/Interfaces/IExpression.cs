using DPA_Musicsheets.Lilypond.Interpreter;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Interfaces
{
    interface IExpression
    {
        void evaluate(LinkedListNode<Token> token, Context context);

        IExpression clone();
    }
}
