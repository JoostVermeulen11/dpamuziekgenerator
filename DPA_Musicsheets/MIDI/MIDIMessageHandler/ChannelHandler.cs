using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.classes;
using DPA_Musicsheets.enums;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler
{
    class ChannelHandler : IMessageTypeHandler
    {
        private MusicSheet musicSheet;
        private MidiEvent nextEvent;
        private Context context;
        private String[] noteLookup = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        private Dictionary<double, double> nootLengtLookup;

        public ChannelHandler()
        {
            nootLengtLookup = new Dictionary<double, double>();
            nootLengtLookup.Add(1, 1);
            nootLengtLookup.Add(2, 0.5);
            nootLengtLookup.Add(4, 0.25);
            nootLengtLookup.Add(8, 0.125);
            nootLengtLookup.Add(16, 0.0625);
            nootLengtLookup.Add(32, 0.03125);
        }

        public IMessageTypeHandler clone()
        {
            return new ChannelHandler();
        }

        public void handle(Context context, MidiEvent midiEvent)
        {
            this.context = context;
            musicSheet = context.musicSheet;
            nextEvent = context.nextEvent;

            if (nextEvent == null)
            {
                //Omdat er nog geen nextevent is word dat de huidige en gaan we door omdat anders de noot niet bepaalt kan worden
                nextEvent = midiEvent;
            }
            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
            if (channelMessage.Command.ToString().Equals("NoteOn"))
            {
                processNote(midiEvent);
            }
            context.nextEvent = midiEvent;
        }

        private void processNote(MidiEvent currentEvent)
        {
            var channelMessage = currentEvent.MidiMessage as ChannelMessage;
            //de noot is afgelopen dus kan die nu toegevoegd worden aan de music sheet
            if (channelMessage.Data2 == 0)
            {
                int octaaf = (int)Math.Floor((Double)channelMessage.Data1 / 12);
                String toonHoogte = noteLookup[channelMessage.Data1 - (octaaf * 12)];
                NoteItem item = NoteItem.Geen;
                if (toonHoogte.Contains("#"))
                {
                    item = NoteItem.Kruis;
                    toonHoogte = toonHoogte.Replace("#", "");
                }

                Note note = new Note(octaaf - 1, toonHoogte, 0, item, TieType.None);
                berekenDuur(currentEvent, note);

                musicSheet.addmusicSymbol(note);

                calculateBarline(currentEvent);
            }
            else
            {
                //check of er rusts zijn
                if (nextEvent.AbsoluteTicks != currentEvent.AbsoluteTicks)
                {
                    // er is een rust
                    RustNote rust = new RustNote();
                    berekenDuur(currentEvent, rust);
                    musicSheet.addmusicSymbol(rust);
                }
                context.previousMidiKey = channelMessage.Data1;
                context.startedNoteIsClosed = false;
            }
        }

        private void calculateBarline(MidiEvent currentEvent)
        {
            double percentageOfBar = 0;
            double deltaTicks = currentEvent.AbsoluteTicks - context.previousNoteAbsoluteTicks;

            if (deltaTicks <= 0)
            {
                percentageOfBar = 0;
            }

            double percentageOfBeatNote = deltaTicks / context.division;
            percentageOfBar = (1.0 / context._beatsPerBar) * percentageOfBeatNote;
            
            context.previousNoteAbsoluteTicks = currentEvent.AbsoluteTicks;
            context.percentageOfBarReached += percentageOfBar;

            if (context.percentageOfBarReached >= 1)
            {
                musicSheet.addmusicSymbol(new MaatStreep());
                context.percentageOfBarReached -= 1;
            }
            context.startedNoteIsClosed = true;
        }
      
        private void berekenDuur(MidiEvent currentEvent, AbstractNote note)
        {
            double deltaTicks = Math.Abs(nextEvent.AbsoluteTicks - currentEvent.AbsoluteTicks);
            double percentageOfBeatNote = deltaTicks / context._sequence.Division;
            double percentageOfWholeNote = (1.0 / context.currentTimesignature.timeSignature[1]) * percentageOfBeatNote;

            for (int noteLength = 32; noteLength >= 1; noteLength /= 2)
            {
                double absoluteNoteLength = (1.0 / noteLength);

                if (percentageOfWholeNote <= absoluteNoteLength)
                {
                    note.duur = noteLength;
                    if (percentageOfWholeNote != nootLengtLookup[note.duur])
                    {
                        //er is een punt
                        note.punten++;
                        note.duur = note.duur * 2;
                    }
                    return;
                }
            }

        }
    }
}
