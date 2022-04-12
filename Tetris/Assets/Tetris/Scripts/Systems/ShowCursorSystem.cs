using Leopotam.Ecs;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class ShowCursorSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MouseMoveEvent> _mouseMoveEventsFilter = null;

        public void Run()
        {
            if (Cursor.visible) return;

            if (_mouseMoveEventsFilter.IsEmpty()) return;

            Cursor.visible = true;
        }
    }
}