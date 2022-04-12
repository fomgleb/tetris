using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Requests;

namespace Tetris.Scripts.Systems
{
    public class ScoreViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpdateScoreViewRequest> _updateScoreViewRequests = null;
        private readonly SceneData _sceneData = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            foreach (var entityIndex in _updateScoreViewRequests)
            {
                _sceneData.ScoreText.text = _runtimeData.Score.ToString();
                _sceneData.HighScoreText.text = _runtimeData.HighScore.ToString();
            }
        }
    }
}