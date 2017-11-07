using DPA_Musicsheets.classes;
using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter.Expressions
{
    class NoteExpression : IExpression
    {
        private static List<Char> noteLookup = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };
        private Context context;
        private Note note;
        private string value;

        public IExpression clone()
        {
            return new NoteExpression();
        }

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Previous != null && token.Previous.Value.type != TokenType.relative)
            {
                this.context = context;
                this.note = new Note();
                this.value = token.Value.value;

                         // Length
                int noteLength = Int32.Parse(Regex.Match(value, @"\d+").Value);
                // Crosses and Moles
                int alter = 0;
                alter += Regex.Matches(value, "is").Count;
                alter -= Regex.Matches(value, "es|as").Count;
                // Octaves
                int distanceWithPreviousNote = noteLookup.IndexOf(value[0]) - noteLookup.IndexOf(context.previousNote);
                if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
                {
                    distanceWithPreviousNote -= 7; // The number of notes in an octave
                }
                else if (distanceWithPreviousNote < -3)
                {
                    distanceWithPreviousNote += 7; // The number of notes in an octave
                }

                if (distanceWithPreviousNote + noteLookup.IndexOf(context.previousNote) >= 7)
                {
                    context.previousOctave++;
                }
                else if (distanceWithPreviousNote + noteLookup.IndexOf(context.previousNote) < 0)
                {
                    context.previousOctave--;
                }

                // Force up or down.
                context.previousOctave += value.Count(c => c == '\'');
                context.previousOctave -= value.Count(c => c == ',');

                context.previousNote = value[0];

                var aap = new PSAMControlLibrary.Note(value[0].ToString().ToUpper(), alter, context.previousOctave, (PSAMControlLibrary.MusicalSymbolDuration)noteLength, PSAMControlLibrary.NoteStemDirection.Up, PSAMControlLibrary.NoteTieType.None, new List<PSAMControlLibrary.NoteBeamType>() { PSAMControlLibrary.NoteBeamType.Single });
                aap.NumberOfDots += value.Count(c => c.Equals('.'));

                note.duur = noteLength;
                note.octaaf = context.previousOctave;
                note.toonHoogte = value[0].ToString();
                note.tied = TieType.None;
                note.punten = value.Count(c => c.Equals('.'));

                if(alter == 1)
                {
                    note.nootItem = NoteItem.Kruis;
                }
                else if(alter == -1)
                {
                    note.nootItem = NoteItem.Mol;
                }
                // apostrof hoeft niet.
                // kommas moeten nog.

                this.addNote();
            }
        }
               
        private void addNote()
        {
            context.musicSheet.addmusicSymbol(note);
        }
    }
}
