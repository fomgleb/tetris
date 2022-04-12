using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using UnityEditor;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class NextFigureBoardViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FigureSpawnedEvent> _figureSpawnedEventsFilter = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            if (_figureSpawnedEventsFilter.IsEmpty()) return;

            var nextFigureData = _configuration.ExistingFigures[_runtimeData.NextFigureIndex];

            for (var y = 0; y < _runtimeData.NextFigureBoard.GetLength(0); y++)
                for (var x = 0; x < _runtimeData.NextFigureBoard.GetLength(1); x++)
                {
                    _runtimeData.NextFigureBoard[y, x].SetActive(false);
                    
                    if (!nextFigureData.rotationStates[0].matrix[y].line[x]) continue;

                    _runtimeData.NextFigureBoard[y, x].SetActive(true);
                    _runtimeData.NextFigureBoard[y, x].GetComponent<SpriteRenderer>().color = nextFigureData.color;
                }
        }
    }
}