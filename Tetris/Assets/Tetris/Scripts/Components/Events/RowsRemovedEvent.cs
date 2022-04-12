using System.Collections.Generic;

namespace Tetris.Scripts.Components.Events
{
    public struct RowsRemovedEvent
    {
        public List<uint> RemovedRowsIndexes;
    }
}