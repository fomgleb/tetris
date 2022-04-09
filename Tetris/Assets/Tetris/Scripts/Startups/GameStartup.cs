using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Systems;
using Tetris.Scripts.Systems.Initializations;
using UnityEngine;

namespace Tetris.Scripts.Startups
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private Configuration configuration;
        [SerializeField] private SceneData sceneData;
        private RuntimeData _runtimeData;
        
        private EcsWorld _world;
        private EcsSystems _updateSystems;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            
            _runtimeData = new RuntimeData();
            
            AddSystems();
            AddOneFrames();
            
            _updateSystems.
                Inject(configuration).
                Inject(sceneData).
                Inject(_runtimeData);
            
            _updateSystems.Init();
        }
        

        private void AddSystems()
        {
            _updateSystems.
                Add(new PlayerInputSystem()).
                Add(new ContainersGameBoardInitSystem()).
                Add(new ContainerInitSystem()).
                Add(new CellsGameBoardInitSystem()).
                Add(new FiguresSpawnerInitSystem()).

                Add(new FigureFallSpeedUpSystem()).

                Add(new TimeTickSystem()).
                Add(new FigureStopFallSystem()).
                Add(new FigureFallSystem()).
                
                Add(new FigureMovementSystem()).
                Add(new FigureRotationSystem()).
                
                Add(new FiguresSpawnSystem()).
                Add(new FigureInitSystem()).
                
                Add(new GameBoardViewUpdateSystem())
                ;
        }

        private void AddOneFrames()
        {
            _updateSystems.
                OneFrame<MoveInputEvent>().
                OneFrame<FigureFallingSpeedUpInputEvent>().
                OneFrame<RotateInputEvent>().
                OneFrame<TickEvent>().
                OneFrame<CheckFigureForStopRequest>().
                OneFrame<InitializeCellRequest>().
                OneFrame<InitializeContainerRequest>().
                OneFrame<InitializeFigureRequest>().
                OneFrame<SpawnFigureRequest>().
                OneFrame<UpdateGameBoardViewRequest>()
                ;
        }
        
        private void Update() => _updateSystems.Run();

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _world.Destroy();
        }
    }
}
