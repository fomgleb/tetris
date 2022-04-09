using System;
using Leopotam.Ecs;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems
{
    public class FigureEntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<FigureEntityDestroyRequest> _figureEntityDestroyRequests = null;

        public void Run()
        {
            foreach (var entityIndex in _figureEntityDestroyRequests)
            {
                ref var figureEntity = ref _figureEntityDestroyRequests.GetEntity(entityIndex);
                
                figureEntity.Destroy();
            }
        }
    }
}