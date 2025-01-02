using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruhshot
{
    internal class UndoManager
    {
        public List<List<Dictionary<string, dynamic>>> undoHistory = new List<List<Dictionary<string, dynamic>>>(); // typechecking!
        public int undoStep = 0;

        public void makeWaypoint(List<Dictionary<string, dynamic>> edits)
        {
            if (undoHistory.Count != 0) {
                List<List<Dictionary<string, dynamic>>> oldHistory = undoHistory;
				undoHistory = undoHistory.GetRange(0, undoStep + 1);
                oldHistory.Clear();
                oldHistory.TrimExcess();
            }
            undoHistory.Add(new List<Dictionary<string, dynamic>>(edits));
            undoStep = undoHistory.Count - 1;
        }
        public List<Dictionary<string, dynamic>> undo()
        {
            if (undoStep-1 >= 0)
            {
                undoStep--;
            }
            return new List<Dictionary<string, dynamic>>(undoHistory[undoStep]);
        }
        public List<Dictionary<string, dynamic>> redo()
        {
            if (undoStep + 1 <= undoHistory.Count-1)
            {
                undoStep++;
            }
            return new List<Dictionary<string, dynamic>>(undoHistory[undoStep]);
        }
    }
}
