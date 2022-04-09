using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FiguresSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnFigureRequest> _spawnFigureRequestsFilter = null;

        private readonly RuntimeData _runtimeData = null;
        private readonly Configuration _configuration = null;

        public void Run()
        {
            if (_spawnFigureRequestsFilter.IsEmpty()) return;

            ref var figuresSpawnerEntity = ref _runtimeData.FiguresSpawnerEntity;
            var existingFigures = _configuration.ExistingFigures;
            var spawnFigureIndex = Random.Range(0, existingFigures.Length);
            
            ref var initializeFigureRequest = ref figuresSpawnerEntity.Get<InitializeFigureRequest>();

            initializeFigureRequest.Figure = existingFigures[spawnFigureIndex];
        }
    }
}