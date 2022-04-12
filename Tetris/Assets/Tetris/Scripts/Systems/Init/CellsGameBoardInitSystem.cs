using Leopotam.Ecs;
using Tetris.Data;
using UnityEngine;

namespace Tetris.Scripts.Systems.Init
{
    public class CellsGameBoardInitSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Init()
        {
            var cellsGameBoardWidth = _configuration.CellsGameBoardWidth;
            var cellsGameBoardHeight = _configuration.CellsGameBoardHeight;
            ref var cellsGameBoard = ref _runtimeData.CellsGameBoard;

            cellsGameBoard = new GameObject[cellsGameBoardHeight, cellsGameBoardWidth];

            for (var y = 0; y < cellsGameBoardHeight; y++)
            {
                for (var x = 0; x < cellsGameBoardWidth; x++)
                {
                    var leftTopCellPosition = _configuration.LeftTopCellPosition;
                    var indentBetweenContainers = _configuration.IndentBetweenCells;
                    var cellPrefab = _configuration.CellPrefab;
                    var cellGameObjectPosition = leftTopCellPosition + new Vector2(x, -y) +
                                                 new Vector2(x * indentBetweenContainers, -y * indentBetweenContainers);
                    var cellGameObject = Object.Instantiate(cellPrefab, cellGameObjectPosition, Quaternion.identity);
                    cellGameObject.SetActive(false);

                    cellsGameBoard[y, x] = cellGameObject;
                }
            }
        }
    }
}