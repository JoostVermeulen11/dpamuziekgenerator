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
        private char[] noteLookup = { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };
        private static List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };
        private int down = 0;
        private int up = 0;
        private Context context;
        private Note note;
        private Note lastNote;
        private string value;

        public void evaluate(LinkedListNode<Token> token, Context context)
        {
            if (token.Previous != null && token.Previous.Value.type != TokenType.relative)
            {
                this.context = context;
                this.note = new Note();
                this.lastNote = getLastNote(context.musicSheet);
                this.value = token.Value.value;

                //makeNote();
                //setOctaaf();
                //addNote();

                // Length
                int noteLength = Int32.Parse(Regex.Match(token.Value.value, @"\d+").Value);
                // Crosses and Moles
                int alter = 0;
                alter += Regex.Matches(token.Value.value, "is").Count;
                alter -= Regex.Matches(token.Value.value, "es|as").Count;
                // Octaves
                int distanceWithPreviousNote = notesorder.IndexOf(token.Value.value[0]) - notesorder.IndexOf(context.previousNote);
                if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
                {
                    distanceWithPreviousNote -= 7; // The number of notes in an octave
                }
                else if (distanceWithPreviousNote < -3)
                {
                    distanceWithPreviousNote += 7; // The number of notes in an octave
                }

                if (distanceWithPreviousNote + notesorder.IndexOf(context.previousNote) >= 7)
                {
                    context.previousOctave++;
                }
                else if (distanceWithPreviousNote + notesorder.IndexOf(context.previousNote) < 0)
                {
                    context.previousOctave--;
                }

                // Force up or down.
                context.previousOctave += token.Value.value.Count(c => c == '\'');
                context.previousOctave -= token.Value.value.Count(c => c == ',');

                context.previousNote = token.Value.value[0];

                var aap = new PSAMControlLibrary.Note(token.Value.value[0].ToString().ToUpper(), alter, context.previousOctave, (PSAMControlLibrary.MusicalSymbolDuration)noteLength, PSAMControlLibrary.NoteStemDirection.Up, PSAMControlLibrary.NoteTieType.None, new List<PSAMControlLibrary.NoteBeamType>() { PSAMControlLibrary.NoteBeamType.Single });
                aap.NumberOfDots += token.Value.value.Count(c => c.Equals('.'));

                note.duur = noteLength;
                note.octaaf = context.previousOctave;
                note.toonHoogte = token.Value.value[0].ToString();
                note.tied = TieType.None;
                note.punten = token.Value.value.Count(c => c.Equals('.'));

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




        public IExpression clone()
        {
            return new NoteExpression();
        }

        private void makeNote()
        {
            int pos = 0;
            while (pos < value.Length)
            {
                if (value[pos] == '~' && lastNote != null)
                {
                    //De noot hoort aan de vorige noot
                    note.setTied(TieType.stop);
                    lastNote.setTied(TieType.start);
                    pos++;
                    continue;
                }
                if (noteLookup.Contains(value[pos]) && note.getToonhoogte() == null)
                {
                    //Het eerste karakter is een noot dus we kunnen er hier vanuit gaan dat we een normale noot hebben
                    note.setToonhoogte(Convert.ToString(value[pos]));
                    pos++;
                    continue;
                }
                if (value[pos] == ',')
                {
                    down++;
                    note.kommas++;
                    pos++;
                    continue;
                }
                if (value[pos] == '\'')
                {
                    up++;
                    note.apostrof++;
                    pos++;
                    continue;
                }
                if (value.Length - 1 - pos > 1)
                {
                    if (value.Contains((char)10))
                    {
                        value = value.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                    }

                    string temp1 = value.Substring(pos, value.Length - 1);
                    string temp2 = value.Substring(pos, value.Length - 1);
                    if (value.Substring(pos, value.Length - 1).Contains("is") || value.Substring(pos, value.Length - 1).Contains("es"))
                    {
                        if (value.Substring(pos, value.Length - 1).Contains("is"))
                        {
                            note.setNootItem(NoteItem.Kruis);
                        }
                        else
                        {
                            note.setNootItem(NoteItem.Mol);
                        }
                        pos += 2;
                        continue;
                    }
                }


                int lastnumer = pos;
                while (true)
                {
                    if (lastnumer < value.Length - 1)
                    {
                        if (Char.IsNumber(value[lastnumer + 1]))
                        {
                            lastnumer++;
                        }
                        else
                        {
                            break;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                if (Char.IsNumber(value[pos]))
                {
                    if (pos == lastnumer)
                    {
                        note.setDuur(Char.GetNumericValue(value[pos]));
                        pos++;
                    }
                    else
                    {
                        note.setDuur(Convert.ToInt16(value.Substring(pos, lastnumer - pos + 1)));
                        pos = lastnumer + 1;
                    }
                    continue;
                }
                //kijken of er een punt is
                if (value[pos] == '.')
                {
                    note.punten = note.punten + 1;
                    pos++;
                    continue;
                }
                //mocht er een } staan dan ben je zo ie zo aan het einde van de noot en kan je dus verder met de volgende
                if (value[pos] == '}')
                {
                    pos++;
                    continue;
                }
                //Als je bij geen van de if statments ben gekomen klopt er iets niet en is het geen noot
                return;
            }
        }

        private void addNote()
        {
            context.musicSheet.addmusicSymbol(note);
        }

        private Note getLastNote(MusicSheet musicSheet)
        {
            LinkedListNode<IMusicSymbol> currentItem;
            if (context["repeat"])
            {
                Repeater repeater = (Repeater)musicSheet.items.Last.Value;
                currentItem = repeater.items.Last;
            }
            else
            {
                currentItem = musicSheet.items.Last;
            }

            while (currentItem != null)
            {
                try
                {
                    return (Note)currentItem.Value;
                }
                catch (InvalidCastException)
                {
                    currentItem = currentItem.Previous;
                }


            }
            return null;
        }
    }
}
