using Leopotam.Ecs;
using UnityEngine;

namespace Tetris.Data
{
    public class RuntimeData
    {
        public EcsEntity[,] ContainersGameBoard;
        public GameObject[,] CellsGameBoard;
        public GameObject[,] NextFigureBoard;
        public EcsEntity FiguresSpawnerEntity;
        public EcsEntity CurrentFigure;
        public uint NextFigureIndex;
        public float RemainingTimeToNextTick;
        public float RemainingTimeToNextFallSpeedUp;
        public EcsEntity PlayerInputEntity;
        public int Score = 0;
        public int HighScore = 0;
    }
}