using System.Collections.Generic;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FigureFallSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TickEvent> _tickEvent = null;
        private readonly EcsFilter<MovableFigureComponent, FigureComponent> _figuresFilter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            if (_tickEvent.IsEmpty()) return;
            
            foreach (var entityIndex in _figuresFilter)
            {
                ref var figureComponent = ref _figuresFilter.Get2(entityIndex);
                ref var cellsMatrix = ref figureComponent.CellsMatrix;

                var requestEntity = _world.NewEntity();
                ref var updateGameBoardViewRequest = ref requestEntity.Get<UpdateGameBoardViewRequest>();
                ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
                cellPositionsToHide = new List<Vector2Int>();
                for (int y = 0; y < cellsMatrix.GetLength(0); y++)
                {
                    for (int x = 0; x < cellsMatrix.GetLength(1); x++)
                    {
                        var entity = cellsMatrix[y, x];
                        if (entity == default || !entity.IsAlive()) continue;
                    
                        ref var positionOnGameBoard = ref entity.Get<PositionOnGameBoardComponent>().Position;
                        cellPositionsToHide.Add(positionOnGameBoard);
                        var newPosition = new Vector2Int(positionOnGameBoard.x, positionOnGameBoard.y + 1);
                        positionOnGameBoard = newPosition;
                    }
                }
            }
        }
    }
}