using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems.Initializations
{
    public class FiguresSpawnerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly RuntimeData _runtimeData = null;
        
        public void Init()
        {
            var figuresSpawnerEntity = _world.NewEntity();

            ref var figuresSpawnerComponent = ref figuresSpawnerEntity.Get<FiguresSpawnerComponent>();
            ref var spawnFigureRequest = ref figuresSpawnerEntity.Get<SpawnFigureRequest>();

            _runtimeData.FiguresSpawnerEntity = figuresSpawnerEntity;
        }
    }
}