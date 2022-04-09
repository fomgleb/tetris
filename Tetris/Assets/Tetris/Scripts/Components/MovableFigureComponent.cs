using UnityEngine;

namespace Tetris.Scripts.Components
{
    public struct MovableFigureComponent
    {
        public Vector2Int MoveDirection;
        public float RemainingTimeToNextMove;
    }
}