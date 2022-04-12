using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems
{
    public class ScoreAddingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FigureStoppedFallingEvent> _figureStoppedFallingEventsFilter = null;
        private readonly EcsFilter<RowsRemovedEvent> _rowsRemovedEvent = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly SceneData _sceneData = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            if (!_figureStoppedFallingEventsFilter.IsEmpty())
            {
                _runtimeData.Score += _configuration.ScoreForFigureFell;
                if (_runtimeData.Score > _runtimeData.HighScore)
                    _runtimeData.HighScore = _runtimeData.Score;
                _world.NewEntity().Get<UpdateScoreViewRequest>();
            }

            if (!_rowsRemovedEvent.IsEmpty())
            {
                _runtimeData.Score += _configuration.ScoreForRowRemoved *
                                      _rowsRemovedEvent.Get1(0).RemovedRowsIndexes.Count;
                if (_runtimeData.Score > _runtimeData.HighScore)
                    _runtimeData.HighScore = _runtimeData.Score;
                _world.NewEntity().Get<UpdateScoreViewRequest>();
            }
        }
    }
}