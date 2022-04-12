using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Components.Tags;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FigureRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotateInputEvent> _rotateInputEvent = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            if (_rotateInputEvent.IsEmpty()) return;

            ref var currentControllableFigure = ref _runtimeData.CurrentFigure;
            if (currentControllableFigure == default || !currentControllableFigure.IsAlive()) return;
            ref var figureComponent = ref currentControllableFigure.Get<FigureComponent>();
            ref var rotationStates = ref figureComponent.RotationStates;
            if (rotationStates.Length == 1) return;
            ref var currentRotationStateIndex = ref figureComponent.CurrentRotationStateIndex;
            var nextRotationStateIndex = currentRotationStateIndex == rotationStates.Length - 1
                ? 0
                : currentRotationStateIndex + 1;
            ref var nextRotationState = ref rotationStates[nextRotationStateIndex];
            
            ref var cellsMatrix = ref figureComponent.CellsMatrix;

            var leftTopCellPositionOnMatrixOnGameBoard = GetLeftTopPositionOnGameBoard(cellsMatrix);

            var cellsEntitiesPositions = GetCellsEntitiesPositions(cellsMatrix);
            var requestEntity = _world.NewEntity();
            ref var updateGameBoardViewRequest = ref requestEntity.Get<UpdateGameBoardViewRequest>();
            ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
            cellPositionsToHide = cellsEntitiesPositions;

            for (var y = 0; y < nextRotationState.matrix.GetLength(0); y++)
                for (var x = 0; x < nextRotationState.matrix[0].line.GetLength(0); x++)
                {
                    if (!nextRotationState.matrix[y].line[x]) continue;
                    
                    var cellPositionOnGameBoard = leftTopCellPositionOnMatrixOnGameBoard + new Vector2Int(x, y);
                    if (cellPositionOnGameBoard.x > _configuration.CellsGameBoardWidth - 1 ||
                        cellPositionOnGameBoard.x < 0 ||
                        cellPositionOnGameBoard.y > _configuration.CellsGameBoardHeight - 1)
                        return;

                    if (_runtimeData.CellsGameBoard[cellPositionOnGameBoard.y, cellPositionOnGameBoard.x].activeSelf)
                    {
                        if (x < 0 || x > cellsMatrix.GetLength(1) - 1 || y > cellsMatrix.GetLength(0) - 1)
                        {
                            return;
                        }
                        
                        var newCellRelatingToFigure = cellsMatrix[y, x];
                        if (newCellRelatingToFigure == default || !newCellRelatingToFigure.IsAlive()) return;    
                    }
                }
            
            DestroyEntities(cellsMatrix);

            for (var y = 0; y < nextRotationState.matrix.GetLength(0); y++)
                for (var x = 0; x < nextRotationState.matrix[0].line.GetLength(0); x++)
                {
                    if (!nextRotationState.matrix[y].line[x]) continue;

                    var cellPositionOnGameBoard = leftTopCellPositionOnMatrixOnGameBoard + new Vector2Int(x, y);
                    var newCellEntity = _world.NewEntity();
                    
                    ref var cellTag = ref newCellEntity.Get<CellTag>();
                    ref var positionOnGameBoardComponent = ref newCellEntity.Get<PositionOnGameBoardComponent>();
                    ref var colorComponent = ref newCellEntity.Get<ColorComponent>();

                    positionOnGameBoardComponent.Position = cellPositionOnGameBoard;
                    colorComponent.Color = figureComponent.Color;

                    cellsMatrix[y, x] = newCellEntity;
                }

            currentRotationStateIndex = nextRotationStateIndex;
        }

        private List<Vector2Int> GetCellsEntitiesPositions(EcsEntity[,] cellsMatrix)
        {
            var cellsEntitiesPositions = new List<Vector2Int>();
            
            for (var y = 0; y < cellsMatrix.GetLength(0); y++)
                for (var x = 0; x < cellsMatrix.GetLength(1); x++)
                {
                    ref var cellEntity = ref cellsMatrix[y, x];
                    if (cellEntity == default || !cellEntity.IsAlive()) continue;

                    cellsEntitiesPositions.Add(cellEntity.Get<PositionOnGameBoardComponent>().Position);
                }

            return cellsEntitiesPositions;
        }

        private Vector2Int GetLeftTopPositionOnGameBoard(EcsEntity[,] cellsMatrix)
        {
            for (var y = 0; y < cellsMatrix.GetLength(0); y++)
                for (var x = 0; x < cellsMatrix.GetLength(1); x++)
                {
                    ref var cellEntity = ref cellsMatrix[y, x];
                    if (cellEntity == default || !cellEntity.IsAlive()) continue;

                    var cellPositionOnGameBoard = cellEntity.Get<PositionOnGameBoardComponent>().Position;
                    var leftTopPositionOnGameBoard = cellPositionOnGameBoard - new Vector2Int(x, y);

                    return leftTopPositionOnGameBoard;
                }

            return default;
        }

        private void DestroyEntities(EcsEntity[,] entitiesMatrix)
        {
            for (var y = 0; y < entitiesMatrix.GetLength(0); y++)
            for (var x = 0; x < entitiesMatrix.GetLength(1); x++)
            {
                ref var cellEntity = ref entitiesMatrix[y, x];

                if (cellEntity == default || !cellEntity.IsAlive()) continue;

                cellEntity.Destroy();
            }
        }
    }
}