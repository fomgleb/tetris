using System.Collections.Generic;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Components.Tags;
using Tetris.Scripts.MonoBehaviours;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class RemoveRawSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FigureStoppedFallingEvent> _figureStoppedFallingEventsFilter = null;
        private readonly EcsFilter<PositionOnGameBoardComponent, CellTag> _cellsFilter = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            if (_figureStoppedFallingEventsFilter.IsEmpty()) return;

            ref var figureMatrix =
                ref _figureStoppedFallingEventsFilter.GetEntity(0).Get<FigureComponent>().CellsMatrix;
            
            var removingRawIndexes = new List<uint>();
            for (var y = 0; y < figureMatrix.GetLength(0); y++)
                for (var x = 0; x < figureMatrix.GetLength(1); x++)
                {
                    ref var cellEntity = ref figureMatrix[y, x];
                    
                    if (cellEntity == default || !cellEntity.IsAlive()) continue;

                    var gameBoardY = cellEntity.Get<PositionOnGameBoardComponent>().Position.y;
                    
                    var lineStarted = false;
                    var solidCellsGameObjectsLine = new List<GameObject>();
                    for (int gameBoardX = 0; gameBoardX < _configuration.CellsGameBoardWidth; gameBoardX++)
                    {
                        if (_runtimeData.CellsGameBoard[gameBoardY, gameBoardX].activeSelf)
                        {
                            lineStarted = true;
                            solidCellsGameObjectsLine.Add(_runtimeData.CellsGameBoard[gameBoardY, gameBoardX]);
                        }
                        else if (lineStarted)
                            break;
                    }

                    Debug.Log(solidCellsGameObjectsLine.Count);
                    if (solidCellsGameObjectsLine.Count == _configuration.CellsGameBoardWidth)
                    {
                        removingRawIndexes.Add((uint)gameBoardY);
                        break;
                    }
                }

            if (removingRawIndexes.Count == 0) return;
            
            var requestEntity = _world.NewEntity();
            ref var updateGameBoardViewRequest = ref requestEntity.Get<UpdateGameBoardViewRequest>();
            ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
            cellPositionsToHide = new List<Vector2Int>();
            foreach (var removingRawIndex in removingRawIndexes)
            {
                for (var x = 0; x < _runtimeData.CellsGameBoard.GetLength(1); x++)
                {
                    var cellGameObject = _runtimeData.CellsGameBoard[(int)removingRawIndex, x];
                    ref var cellEntity = ref cellGameObject.GetComponent<EntityReference>().Entity;
                    
                    cellPositionsToHide.Add(cellEntity.Get<PositionOnGameBoardComponent>().Position);
                    
                    cellEntity.Destroy();
                }    
            }
            
        }
    }
}