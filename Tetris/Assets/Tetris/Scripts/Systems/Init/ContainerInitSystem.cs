using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.References;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems.Init
{
    public class ContainerInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<InitializeContainerRequest> _initializeContainerEntityRequestsFilter = null;
        private readonly Configuration _configuration = null;

        public void Init()
        {
            foreach (var entityIndex in _initializeContainerEntityRequestsFilter)
            {
                ref var initializeContainerEntityRequest = ref _initializeContainerEntityRequestsFilter.Get1(entityIndex);
                ref var containerEntity = ref _initializeContainerEntityRequestsFilter.GetEntity(entityIndex);
                
                // Components
                ref var positionOnGameBoardComponent = ref containerEntity.Get<PositionOnGameBoardComponent>();
                ref var gameObjectReference = ref containerEntity.Get<GameObjectReference>();
                positionOnGameBoardComponent.Position = initializeContainerEntityRequest.ContainerPosition;

                var leftTopContainerPosition = _configuration.LeftTopContainerPosition;
                var containerPrefab = _configuration.ContainerPrefab;
                ref var containerEntityPosition = ref positionOnGameBoardComponent.Position;
                var indentBetweenContainers = _configuration.IndentBetweenContainers;
                var containerGameObjectPosition = leftTopContainerPosition +
                                                  new Vector2(containerEntityPosition.x, -containerEntityPosition.y) +
                                                  new Vector2(containerEntityPosition.x * indentBetweenContainers,
                                                      -containerEntityPosition.y * indentBetweenContainers);
                var containerGameObject =
                    Object.Instantiate(containerPrefab, containerGameObjectPosition, Quaternion.identity);

                gameObjectReference.GameObject = containerGameObject;
            }
        }
    }
}