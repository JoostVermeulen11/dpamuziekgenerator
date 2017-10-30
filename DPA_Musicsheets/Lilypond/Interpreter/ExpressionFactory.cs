using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Interpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter
{
    class ExpressionFactory
    {
        static Dictionary<TokenType, IExpression> expressions;
        static ExpressionFactory()
        {
            expressions = new Dictionary<TokenType, IExpression>();
            expressions.Add(TokenType.Note, new NoteExpression());
            expressions.Add(TokenType.relative, new RelativeExpression());
            expressions.Add(TokenType.timeSignaturedata, new TimeSignatureExpression());
            expressions.Add(TokenType.Maatstreep, new MaatExpression());
            expressions.Add(TokenType.Rust, new RustExpression());
            expressions.Add(TokenType.TempoValue, new TempoExpression());
            expressions.Add(TokenType.EndBlok, new EndblokExpression());
            expressions.Add(TokenType.Repeat, new RepeatExpression());
            expressions.Add(TokenType.ClefType, new ClefExpression());
        }

        public static IExpression getExpresionHandler(TokenType type)
        {
            if (expressions.ContainsKey(type))
            {
                return expressions[type].clone();
            }
            else
            {
                return null;
            }
        }
    }
}
