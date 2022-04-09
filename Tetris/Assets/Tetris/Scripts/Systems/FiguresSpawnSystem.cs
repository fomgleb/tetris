using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FiguresSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FiguresSpawnerComponent, SpawnFigureRequest> _figuresSpawnersFilter = null;
        private readonly Configuration _configuration = null;

        public void Run()
        {
            foreach (var entityIndex in _figuresSpawnersFilter)
            {
                ref var figuresSpawnerEntity = ref _figuresSpawnersFilter.GetEntity(entityIndex);
                var existingFigures = _configuration.ExistingFigures;
                var spawnFigureIndex = Random.Range(0, existingFigures.Length);
                
                ref var initializeFigureRequest = ref figuresSpawnerEntity.Get<InitializeFigureRequest>();

                initializeFigureRequest.Figure = existingFigures[spawnFigureIndex];
            }
        }
    }
}