using Leopotam.Ecs;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems
{
    public class ExitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExitInputEvent> _exitInputEventsFilter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            if (_exitInputEventsFilter.IsEmpty()) return;

            _world.NewEntity().Get<CloseApplicationRequest>();
            _world.NewEntity().Get<SaveProgressRequest>();
        }
    }
}