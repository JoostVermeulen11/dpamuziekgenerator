using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter
{
    class LilypondInterpreter
    {
        public MusicSheet process(LinkedList<Token> tokens)
        {
            Context context = new Context();
            LinkedListNode<Token> currentToken = tokens.First;
            while (currentToken != null)
            {
                IExpression handler = ExpressionFactory.getExpresionHandler(currentToken.Value.type);
                if (handler != null)
                {
                    handler.evaluate(currentToken, context);
                }
                currentToken = currentToken.Next;
            }
            return context.musicSheet;
        }
    }
}
