using System;
using Leopotam.Ecs;
using Tetris.Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Tetris.Scripts.Systems.Init
{
    public class NextFigureBoardInitSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Init()
        {
            ref var nextFigureBoard = ref _runtimeData.NextFigureBoard;
            var nextFigureBoardHeight = _configuration.NextFigureBoardHeight;
            var nextFigureBoardWidth = _configuration.NextFigureBoardWidth;
            var containerPrefab = _configuration.ContainerPrefab;
            var cellPrefab = _configuration.CellPrefab;
            var leftTopElementPositionOnNextFigureBoard = _configuration.LeftTopElementPositionOnNextFigureBoard;
            var indentBetweenContainers = _configuration.IndentBetweenContainers;

            nextFigureBoard = new GameObject[nextFigureBoardHeight, nextFigureBoardWidth];

            for (var y = 0; y < nextFigureBoardHeight; y++)
                for (var x = 0; x < nextFigureBoardWidth; x++)
                {
                    var gameObjectPosition = leftTopElementPositionOnNextFigureBoard + new Vector2(x, -y) +
                        new Vector2(x * indentBetweenContainers, -y * indentBetweenContainers);
                    
                    Object.Instantiate(containerPrefab, gameObjectPosition, Quaternion.identity);

                    var cellGameObject =  Object.Instantiate(cellPrefab, gameObjectPosition, Quaternion.identity);

                    nextFigureBoard[y, x] = cellGameObject;
                }
            
            _runtimeData.NextFigureIndex = (uint)Random.Range(0, _configuration.ExistingFigures.Length);
        }
    }
}