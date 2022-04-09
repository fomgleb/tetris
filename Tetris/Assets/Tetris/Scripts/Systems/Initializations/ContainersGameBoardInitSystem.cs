using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems.Initializations
{
    public class ContainersGameBoardInitSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            var containersGameBoardWidth = _configuration.ContainersGameBoardWidth;
            var containersGameBoardHeight = _configuration.ContainersGameBoardHeight;
            ref var containersGameBoard = ref _runtimeData.ContainersGameBoard;

            containersGameBoard = new EcsEntity[containersGameBoardHeight, containersGameBoardWidth];

            for (var y = 0; y < containersGameBoardHeight; y++)
            {
                for (var x = 0; x < containersGameBoardWidth; x++)
                {
                    var containerEntity = _world.NewEntity();

                    ref var initializeContainerRequest = ref containerEntity.Get<InitializeContainerRequest>();

                    initializeContainerRequest.ContainerPosition = new Vector2Int(x, y);
                    containersGameBoard[y, x] = containerEntity;
                }
            }
        }
    }
}