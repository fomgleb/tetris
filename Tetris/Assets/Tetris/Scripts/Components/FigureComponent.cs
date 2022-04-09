using System.Collections.Generic;
using Leopotam.Ecs;
using Tetris.Data;
using UnityEngine;

namespace Tetris.Scripts.Components
{
    public struct FigureComponent
    {
        public RotationState[] RotationStates;
        public uint CurrentRotationStateIndex;
        public EcsEntity[,] CellsMatrix;
        public Color Color;
    }
}