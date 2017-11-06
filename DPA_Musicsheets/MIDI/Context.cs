using System;
using Sanford.Multimedia.Midi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.MIDI
{
    class Context
    {
        public Context()
        {
            musicSheet = new MusicSheet();
        }

        public TimeSignature currentTimesignature { get; set; }
        public MusicSheet musicSheet { get; set; }
        public MidiEvent nextEvent { get; set; }
        public Sequence _sequence { get; set; }

        public int _beatNote = 4;    // De waarde van een beatnote.
        public int _bpm = 120;       // Aantal beatnotes per minute.
        public int _beatsPerBar;     // Aantal beatnotes per maat.

        public int division;
        public int previousMidiKey = 60; // Central C;
        public int previousNoteAbsoluteTicks = 0;
        public double percentageOfBarReached = 0;
        public bool startedNoteIsClosed = true;        
    }
}
