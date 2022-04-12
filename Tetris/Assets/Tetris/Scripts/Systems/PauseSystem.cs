using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class PauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PauseInputEvent> _pauseInputEventsFilter = null;
        private readonly SceneData _sceneData = null;

        public void Run()
        {
            if (_pauseInputEventsFilter.IsEmpty()) return;

            if (_sceneData.PauseScreen.activeSelf)
            {
                Time.timeScale = 1;
                _sceneData.PauseScreen.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                _sceneData.PauseScreen.SetActive(true);
            }
                
            
            
        }
    }
}