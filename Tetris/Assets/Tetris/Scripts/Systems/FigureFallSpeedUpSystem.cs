using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class FigureFallSpeedUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent> _playerInputFilter = null;
        private readonly EcsFilter<FigureFallingSpeedUpInputEvent> _figureFallingSpeedUpInputEventFilter = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            ref var remainingTimeToNextFallSpeedUp = ref _runtimeData.RemainingTimeToNextFallSpeedUp;
            ref var fallSpeedUpIsPressing = ref _playerInputFilter.Get1(0).FallSpeedUpIsPressing;
            if (!_figureFallingSpeedUpInputEventFilter.IsEmpty())
                remainingTimeToNextFallSpeedUp = _configuration.TimeBetweenFigureFallSpeedUps;
            else if (fallSpeedUpIsPressing)
            {
                if (remainingTimeToNextFallSpeedUp > 0)
                {
                    remainingTimeToNextFallSpeedUp -= Time.deltaTime;
                    return;
                }

                remainingTimeToNextFallSpeedUp = _configuration.TimeBetweenFigureFallSpeedUps;
            }
            else return;

            _runtimeData.RemainingTimeToNextTick = 0;
        }
    }
}