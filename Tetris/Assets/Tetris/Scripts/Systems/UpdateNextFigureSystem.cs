using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class UpdateNextFigureSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FigureSpawnedEvent> _figureSpawnedEventFilter = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Run()
        {
            if (_figureSpawnedEventFilter.IsEmpty()) return;

            _runtimeData.NextFigureIndex = (uint)Random.Range(0, _configuration.ExistingFigures.Length);
        }
    }
}