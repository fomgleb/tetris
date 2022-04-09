using System.Linq;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FigureStopFallSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TickEvent> _tickEvent = null;
        private readonly EcsFilter<CheckFigureForStopRequest> _checkFigureForStopRequest = null;
        private readonly EcsFilter<MovableFigureComponent, FigureComponent> _figuresFilter = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            if (_tickEvent.IsEmpty() && _checkFigureForStopRequest.IsEmpty()) return;
            
            foreach (var entityIndex in _figuresFilter)
            {
                ref var figureEntity = ref _figuresFilter.GetEntity(entityIndex);
                ref var figureComponent = ref _figuresFilter.Get2(entityIndex);
                ref var cellsMatrix = ref figureComponent.CellsMatrix;
                var cellsGameBoard = _runtimeData.CellsGameBoard;
                var cellsGameBoardHeight = _configuration.CellsGameBoardHeight;

                var figureMustStop = false;

                for (int y = 0; y < cellsMatrix.GetLength(0); y++)
                {
                    for (int x = 0; x < cellsMatrix.GetLength(1); x++)
                    {
                        var cellEntity = cellsMatrix[y, x];
                        
                        if (cellEntity == default || !cellEntity.IsAlive()) continue;
                    
                        ref var cellPositionOnGameBoard = ref cellEntity.Get<PositionOnGameBoardComponent>().Position;
                        var bottomCellPositionOnGameBoard = new Vector2Int(cellPositionOnGameBoard.x, cellPositionOnGameBoard.y + 1);

                        if (bottomCellPositionOnGameBoard.y > cellsGameBoardHeight - 1)
                        {
                            figureMustStop = true;
                            break;
                        }
                    
                        if (cellsGameBoard[bottomCellPositionOnGameBoard.y, bottomCellPositionOnGameBoard.x].activeSelf)
                        {
                            var bottomCellPositionOnMatrix = new Vector2Int(x, y + 1);
                            if (bottomCellPositionOnMatrix.y > cellsMatrix.GetLength(0) - 1)
                            {
                                figureMustStop = true;
                                break;
                            }

                            var bottomCellRelatingToFigure = cellsMatrix[bottomCellPositionOnMatrix.y,
                                bottomCellPositionOnMatrix.x];
                            
                            if (bottomCellRelatingToFigure == default || !bottomCellRelatingToFigure.IsAlive())
                            {
                                figureMustStop = true;
                                break;
                            }
                        }
                    }
                }

                if (figureMustStop)
                {
                    figureEntity.Destroy();
                    _runtimeData.FiguresSpawnerEntity.Get<SpawnFigureRequest>();                    
                }
            }
        }
    }
}