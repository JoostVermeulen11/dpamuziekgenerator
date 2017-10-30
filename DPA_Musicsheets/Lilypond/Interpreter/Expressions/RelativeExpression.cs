using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter.Expressions
{
    class RelativeExpression : IExpression
    {
        private Dictionary<char, int> startWaarde;

        public RelativeExpression()
        {
            startWaarde = new Dictionary<char, int>();
            startWaarde.Add('c', 4);
        }

        public void evaluate(LinkedListNode<Tokenizer.Token> token, Context context)
        {
            if (token.Next.Value.type == enums.TokenType.Note)
            {
                string note = token.Next.Value.value;
                context.musicSheet.startOctaaf = startWaarde[note[0]];
                context["relative"] = true;
            }
        }

        public IExpression clone()
        {
            return new RelativeExpression();
        }
    }
}
