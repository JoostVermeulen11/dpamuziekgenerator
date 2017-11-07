using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Memento
{
    class Memento
    {
        private FileHandler fileHandler;
        Node Root { get; set; }
        Node Current { get; set; }
        Node Last { get; set; }

        public Memento(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public void NewNode(string EditText)
        {
            if (Root == null)
            {
                Root = new Node(EditText);
                Current = Root;
            }
            else
            {
                Current.Next = new Node(EditText);
                Current.Next.Last = Current;
                Current = Current.Next;
                Last = Current;
            }

            fileHandler.RedrawStaff();
        }

        public void Back()
        {
            if (Current != Root && Root != null)
            {
                Current = Current.Last;
                fileHandler.EditorText = Current.EditString;
                fileHandler.RedrawStaff();
            }
        }

        public void Forward()
        {
            if (Current != Last && Root != null)
            {
                Current = Current.Next;
                fileHandler.EditorText = Current.EditString;
                //fileHandler.SetEditText(Current.EditString);
                fileHandler.RedrawStaff();
            }
        }

    }
}
