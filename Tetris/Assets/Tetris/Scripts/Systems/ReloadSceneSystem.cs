using Leopotam.Ecs;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using UnityEngine.SceneManagement;

namespace Tetris.Scripts.Systems
{
    public class ReloadSceneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ReloadSceneRequest> _reloadSceneRequestsFilter = null;
        private readonly EcsFilter<ReloadInputEvent> _reloadInputEventsFilter = null;

        public void Run()
        {
            if (_reloadSceneRequestsFilter.IsEmpty() && _reloadInputEventsFilter.IsEmpty()) return;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}