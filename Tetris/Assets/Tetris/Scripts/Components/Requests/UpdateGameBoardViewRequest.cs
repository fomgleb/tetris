using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Scripts.Components.Requests
{
    public struct UpdateGameBoardViewRequest
    {
        public List<Vector2Int> CellPositionsToHide;
    }
}