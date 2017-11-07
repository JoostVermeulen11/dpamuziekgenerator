using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Managers
{
    public class SequenceEventArgs
    {
        public Sequence PlayableSequence { get; set; }
        public string Message { get; set; }
    }
}