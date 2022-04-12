using Leopotam.Ecs;
using Tetris.Data;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class SetCameraPositionAndSizeSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly SceneData _sceneData = null;
        
        public void Init()
        {
            var mainCamera =  _sceneData.MainCamera;
            var gameBoardHeight = _configuration.ContainersGameBoardHeight;
            var gameBoardWidth = _configuration.ContainersGameBoardWidth;
            var indentBetweenCells = _configuration.IndentBetweenContainers;
            var positionOfLeftTopCell = _configuration.LeftTopContainerPosition;
            
            mainCamera.orthographicSize = (gameBoardHeight + (gameBoardHeight - 1) * indentBetweenCells) / 2f;
            mainCamera.transform.position = new Vector3(positionOfLeftTopCell.x + gameBoardWidth / 2f + ((gameBoardHeight - 1) * indentBetweenCells) /2f - 0.5f,
                positionOfLeftTopCell.y - gameBoardHeight / 2f - ((gameBoardHeight - 1) * indentBetweenCells) /2f + 0.5f, mainCamera.transform.position.z);
        }
    }
}