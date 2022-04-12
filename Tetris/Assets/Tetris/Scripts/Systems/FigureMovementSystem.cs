using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FigureMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent> _playerInputFilter = null;
        private readonly EcsFilter<MoveInputEvent> _moveInputEventFilter = null;
        private readonly EcsFilter<MovableFigureComponent, FigureComponent> _movableFiguresFilter = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            ref var currentControllableFigureEntity = ref _runtimeData.CurrentFigure;
            if (currentControllableFigureEntity == default || !currentControllableFigureEntity.IsAlive()) return;
            ref var movableFigureComponent = ref currentControllableFigureEntity.Get<MovableFigureComponent>();
            ref var remainingTimeToNextMove = ref movableFigureComponent.RemainingTimeToNextMove;
            ref var moveInput = ref _playerInputFilter.Get1(0).MoveInput;

            if (!_moveInputEventFilter.IsEmpty())
                remainingTimeToNextMove = _configuration.TimeAfterFirstMoveClick;
            else if (moveInput != 0)
            {
                if (remainingTimeToNextMove > 0)
                {
                    remainingTimeToNextMove -= Time.deltaTime;
                    return;
                }

                remainingTimeToNextMove = _configuration.TimeBetweenFigureMoves;
            }
            else return;
            
            ref var figureComponent = ref currentControllableFigureEntity.Get<FigureComponent>();
            
            ref var cellsMatrix = ref figureComponent.CellsMatrix;

            var cellsGameBoardWidth = _configuration.CellsGameBoardWidth;
            ref var cellsGameBoard = ref _runtimeData.CellsGameBoard;

            var canMove = true;
            for (var y = 0; y < cellsMatrix.GetLength(0); y++)
            {
                for (var x = 0; x < cellsMatrix.GetLength(1); x++)
                {
                    var cellEntity = cellsMatrix[y, x];
                    
                    if (cellEntity == default || !cellEntity.IsAlive()) continue;
                
                    ref var currentCellPosition = ref cellEntity.Get<PositionOnGameBoardComponent>().Position;
                    var newCellPositionOnGameBoard = currentCellPosition + new Vector2Int((int)moveInput, 0);

                    if (newCellPositionOnGameBoard.x > cellsGameBoardWidth - 1 || newCellPositionOnGameBoard.x < 0)
                    {
                        canMove = false;
                        break;
                    }

                    if (cellsGameBoard[newCellPositionOnGameBoard.y, newCellPositionOnGameBoard.x].activeSelf)
                    {
                        var newCellPositionOnMatrix = new Vector2Int(x + (int)moveInput, y);
                        if (newCellPositionOnMatrix.x < 0 || newCellPositionOnMatrix.x > cellsMatrix.GetLength(1) - 1)
                        {
                            canMove = false;
                            break;
                        }

                        var newCellPositionRelatingToFigure =
                            cellsMatrix[newCellPositionOnMatrix.y, newCellPositionOnMatrix.x];
                        if (newCellPositionRelatingToFigure == default || !newCellPositionRelatingToFigure.IsAlive())
                        {
                            canMove = false;
                            break;
                        }    
                    }
                }
            }

            if (!canMove) return;
            
            var requestEntity = _world.NewEntity();
            ref var updateGameBoardViewRequest = ref requestEntity.Get<UpdateGameBoardViewRequest>();
            ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
            cellPositionsToHide = new List<Vector2Int>();
            for (var y = 0; y < cellsMatrix.GetLength(0); y++)
            {
                for (var x = 0; x < cellsMatrix.GetLength(1); x++)
                {
                    var cellEntity = cellsMatrix[y, x];

                    if (cellEntity == default || !cellEntity.IsAlive()) continue;
                    
                    ref var currentCellPosition = ref cellEntity.Get<PositionOnGameBoardComponent>().Position;
                    cellPositionsToHide.Add(currentCellPosition);
                    var newCellPosition = currentCellPosition + new Vector2Int((int)moveInput, 0);
            
                    currentCellPosition = newCellPosition;    
                }
            }
        }
    }
}