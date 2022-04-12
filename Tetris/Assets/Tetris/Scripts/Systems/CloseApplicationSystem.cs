using Leopotam.Ecs;
using Tetris.Scripts.Components.Requests;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class CloseApplicationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CloseApplicationRequest> _closeApplicationRequestsFilter = null;

        public void Run()
        {
            foreach (var entityIndex in _closeApplicationRequestsFilter)
            {
                Application.Quit();
            }
        }
    }
}