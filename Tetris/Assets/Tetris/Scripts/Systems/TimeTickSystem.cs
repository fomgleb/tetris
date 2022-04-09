using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class TimeTickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Init()
        {
            ref var remainingTimeToNextTick = ref _runtimeData.RemainingTimeToNextTick;

            remainingTimeToNextTick = _configuration.OneTickDuration;
        }
        
        public void Run()
        {
            ref var remainingTimeToNextTick = ref _runtimeData.RemainingTimeToNextTick;
            if (remainingTimeToNextTick > 0)
            {
                remainingTimeToNextTick -= Time.deltaTime;
                return;
            }
            remainingTimeToNextTick = _configuration.OneTickDuration;

            _world.NewEntity().Get<TickEvent>();
        }
    }
}