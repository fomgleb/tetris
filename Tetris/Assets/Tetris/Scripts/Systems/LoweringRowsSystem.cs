using System.Collections.Generic;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.MonoBehaviours;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class LoweringRowsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RowsRemovedEvent> _rowsRemovedEventsFilter = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var entityIndex in _rowsRemovedEventsFilter)
            {
                ref var removedRowsIndexes = ref _rowsRemovedEventsFilter.Get1(entityIndex).RemovedRowsIndexes;
                
                var updateGameBoardViewEntity = _world.NewEntity();
                ref var updateGameBoardViewRequest = ref updateGameBoardViewEntity.Get<UpdateGameBoardViewRequest>();
                ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
                cellPositionsToHide = new List<Vector2Int>();

                foreach (var removedRowIndex in removedRowsIndexes)
                {
                    for (var y = 0; y < removedRowIndex; y++)
                    for (var x = 0; x < _configuration.CellsGameBoardWidth; x++)
                    {
                        var cellGameObject = _runtimeData.CellsGameBoard[y, x];
                        if (!cellGameObject.activeSelf) continue;
                        ref var cellEntity = ref cellGameObject.GetComponent<EntityReference>().Entity;
                        if (cellEntity == default || !cellEntity.IsAlive()) continue;

                        ref var cellPositionOnGameBoard = ref cellEntity.Get<PositionOnGameBoardComponent>().Position;
                        var newCellPositionOneGameBoard =  cellPositionOnGameBoard + new Vector2Int(0, 1);

                        cellPositionsToHide.Add(cellPositionOnGameBoard);
                        cellPositionOnGameBoard = newCellPositionOneGameBoard;
                    }    
                }
            }
        }
    }
}