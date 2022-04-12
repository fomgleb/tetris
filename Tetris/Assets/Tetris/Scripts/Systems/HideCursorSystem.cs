using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class HideCursorSystem : IEcsRunSystem
    {
        private readonly RuntimeData _runtimeData = null;
        
        public void Run()
        {
            if (!Cursor.visible) return;
            
            ref var playerInputEntity = ref _runtimeData.PlayerInputEntity;

            if (!playerInputEntity.Has<MoveInputEvent>() && !playerInputEntity.Has<RotateInputEvent>() &&
                !playerInputEntity.Has<FigureFallingSpeedUpInputEvent>()) return;

            Cursor.visible = false;
        }
    }
}