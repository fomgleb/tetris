using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using Tetris.Scripts.Components.Requests;
using Tetris.Scripts.Components.Tags;
using UnityEngine;

namespace Tetris.Scripts.Systems.Init
{
    public class FigureInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeFigureRequest> _initializeFigureEntityRequests = null;
        private readonly Configuration _configuration = null;
        private readonly RuntimeData _runtimeData = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var entityIndex in _initializeFigureEntityRequests)
            {
                var figure = _initializeFigureEntityRequests.Get1(entityIndex).Figure;
                var figureRotationStates = figure.rotationStates;
                var figureColor = figure.color;
                var figureSpawnPoint = _configuration.FigureSpawnPointOnGameBoard;
                var figureEntity = _world.NewEntity();

                ref var figureTag = ref figureEntity.Get<FigureTag>();
                ref var figureComponent = ref figureEntity.Get<FigureComponent>();
                ref var movableFigureComponent = ref figureEntity.Get<MovableFigureComponent>();

                figureComponent.RotationStates = figureRotationStates;
                figureComponent.Color = figureColor;
                figureComponent.CurrentRotationStateIndex = 0;
                ref var cellsMatrix = ref figureComponent.CellsMatrix;
                var figureRotationStatesHeight = figureRotationStates[0].matrix.GetLength(0);
                var figureRotationStatesWidth = figureRotationStates[0].matrix[0].line.GetLength(0);
                cellsMatrix = new EcsEntity[figureRotationStatesHeight, figureRotationStatesWidth];
                
                for (var y = 0; y < figureRotationStatesHeight; y++)
                    for (var x = 0; x < figureRotationStatesWidth; x++)
                    {
                        if (!figureRotationStates[0].matrix[y].line[x]) continue;
                        var cellPositionOnGameBoard = new Vector2Int(x + figureSpawnPoint.x, y + figureSpawnPoint.y);

                        var newCellEntity = _world.NewEntity();

                        ref var cellTag = ref newCellEntity.Get<CellTag>();
                        ref var positionOnGameBoardComponent = ref newCellEntity.Get<PositionOnGameBoardComponent>();
                        ref var colorComponent = ref newCellEntity.Get<ColorComponent>();
                        
                        positionOnGameBoardComponent.Position =
                            new Vector2Int(cellPositionOnGameBoard.x, cellPositionOnGameBoard.y);
                        colorComponent.Color = figureColor;

                        cellsMatrix[y, x] = newCellEntity;
                    }

                _runtimeData.CurrentFigure = figureEntity;
                figureEntity.Get<FigureSpawnedEvent>();
                _world.NewEntity().Get<UpdateGameBoardViewRequest>();
            }
        }
    }
}