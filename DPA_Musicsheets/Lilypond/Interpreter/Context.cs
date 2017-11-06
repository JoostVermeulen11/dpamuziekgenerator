using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond.Interpreter
{
    class Context
    {
        private Dictionary<string, Boolean> _variables;
        public MusicSheet musicSheet { get; set; }
        public int previousOctave = 4;
        public char previousNote = 'c';
        public Clef currentClef = null;

        public Context()
        {
            _variables = new Dictionary<string, Boolean>();
            musicSheet = new MusicSheet();
        }

        public Boolean this[string variableName]
        {
            get
            {
                if (_variables.ContainsKey(variableName))
                {
                    return _variables[variableName];
                }
                else
                {
                    return false;
                }
            }
            set { _variables[variableName] = value; }
        }
    }
}
