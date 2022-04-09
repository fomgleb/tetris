using Leopotam.Ecs;
using UnityEngine;

namespace Tetris.Data
{
    public class RuntimeData
    {
        public EcsEntity[,] ContainersGameBoard;
        public GameObject[,] CellsGameBoard;
        public EcsEntity FiguresSpawnerEntity;
        public EcsEntity CurrentControllableFigure;
        public float RemainingTimeToNextTick;
        public float RemainingTimeToNextFallSpeedUp;
    }
}