using System;
using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Components.Tags;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class GameBoardViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpdateGameBoardViewRequest> _updateRequestsFilter = null;
        private readonly EcsFilter<PositionOnGameBoardComponent, ColorComponent, CellTag> _cellsFilter = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            foreach (var requestEntityIndex in _updateRequestsFilter)
            {
                ref var updateGameBoardViewRequest = ref _updateRequestsFilter.Get1(requestEntityIndex);
                ref var cellPositionsToHide = ref updateGameBoardViewRequest.CellPositionsToHide;
                var cellsGameBoard = _runtimeData.CellsGameBoard;

                if (cellPositionsToHide != null)
                    foreach (var cellPosition in cellPositionsToHide)
                        cellsGameBoard[cellPosition.y, cellPosition.x].SetActive(false);

                foreach (var entityIndex in _cellsFilter)
                {
                    ref var positionOnGameBoardComponent = ref _cellsFilter.Get1(entityIndex);
                    ref var colorComponent = ref _cellsFilter.Get2(entityIndex);
                    ref var cellPositionToShow = ref positionOnGameBoardComponent.Position;
                    ref var color = ref colorComponent.Color;

                    var cellToUpdate = cellsGameBoard[cellPositionToShow.y, cellPositionToShow.x]; 
                    cellToUpdate.SetActive(true);
                    cellToUpdate.GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
    }
}