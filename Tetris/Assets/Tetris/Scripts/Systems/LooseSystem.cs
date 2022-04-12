using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using UnityEngine.SceneManagement;

namespace Tetris.Scripts.Systems
{
    public class LooseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnFigureRequest> _spawnFigureRequestsFilter = null;
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;

        public void Run()
        {
            foreach (var entityIndex in _spawnFigureRequestsFilter)
            {
                var figureMatrixHeight = _configuration.ExistingFigures[0].rotationStates[0].matrix.GetLength(0);
                var figureMatrixWidth = _configuration.ExistingFigures[0].rotationStates[0].matrix[0].line.Length;
                var figureSpawnPointOnGameBoard = _configuration.FigureSpawnPointOnGameBoard;
                
                for (var y = figureSpawnPointOnGameBoard.y; y < figureMatrixHeight + figureSpawnPointOnGameBoard.y; y++)
                    for (var x = figureSpawnPointOnGameBoard.x; x < figureMatrixWidth + figureSpawnPointOnGameBoard.x; x++)
                    {
                        if (!_runtimeData.CellsGameBoard[y, x].activeSelf) continue;

                        _world.NewEntity().Get<ReloadSceneRequest>();
                        _world.NewEntity().Get<SaveProgressRequest>();
                    }
            }
        }
    }
}