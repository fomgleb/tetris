using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Systems;
using Tetris.Scripts.Systems.Init;
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
                Add(new SetCameraPositionAndSizeSystem()).
                Add(new PlayerInputSystem()).
                Add(new HideCursorSystem()).
                Add(new ContainersGameBoardInitSystem()).
                Add(new ContainerInitSystem()).
                Add(new CellsGameBoardInitSystem()).
                Add(new FiguresSpawnerInitSystem()).
                Add(new NextFigureBoardInitSystem()).
                
                Add(new LoadProgressSystem()).

                Add(new PauseSystem()).
                
                Add(new FigureFallSpeedUpSystem()).

                Add(new TimeTickSystem()).
                Add(new FigureStopFallSystem()).
                Add(new RemoveRawSystem()).
                Add(new ScoreAddingSystem()).
                Add(new ScoreViewUpdateSystem()).
                Add(new LoweringRowsSystem()).
                Add(new FigureEntityDestroySystem()).
                Add(new FigureFallSystem()).
                
                Add(new FigureMovementSystem()).
                Add(new FigureRotationSystem()).
                
                Add(new LooseSystem()).
                Add(new FiguresSpawnSystem()).
                Add(new FigureInitSystem()).
                
                Add(new UpdateNextFigureSystem()).
                
                Add(new GameBoardViewUpdateSystem()).
                Add(new NextFigureBoardViewUpdateSystem()).
                
                Add(new ShowCursorSystem()).
                Add(new ExitSystem()).
                
                Add(new SaveProgressSystem()).
                Add(new ReloadSceneSystem()).
                Add(new CloseApplicationSystem())
                ;
        }

        private void AddOneFrames()
        {
            _updateSystems.
                OneFrame<FigureFallingSpeedUpInputEvent>().
                OneFrame<MoveInputEvent>().
                OneFrame<RotateInputEvent>().
                OneFrame<TickEvent>().
                OneFrame<CheckFigureForStopRequest>().
                OneFrame<FigureStoppedFallingEvent>().
                OneFrame<InitializeCellRequest>().
                OneFrame<InitializeContainerRequest>().
                OneFrame<InitializeFigureRequest>().
                OneFrame<SpawnFigureRequest>().
                OneFrame<UpdateGameBoardViewRequest>().
                OneFrame<FigureEntityDestroyRequest>().
                OneFrame<RowsRemovedEvent>().
                OneFrame<FigureSpawnedEvent>().
                OneFrame<MouseMoveEvent>().
                OneFrame<ExitInputEvent>().
                OneFrame<SaveProgressRequest>().
                OneFrame<UpdateScoreViewRequest>().
                OneFrame<ReloadSceneRequest>().
                OneFrame<CloseApplicationRequest>().
                OneFrame<PauseInputEvent>()
                ;
        }
        
        private void Update() => _updateSystems.Run();

        private void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}
