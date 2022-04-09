using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Tetris.Data
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [Header("Containers game board")]
        [SerializeField] private uint containersGameBoardWidth;
        [SerializeField] private uint containersGameBoardHeight;
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private Vector2 leftTopContainerPosition;
        [SerializeField] private float indentBetweenContainers;
        
        [Header("Cells game board")]
        [SerializeField] private uint cellsGameBoardWidth;
        [SerializeField] private uint cellsGameBoardHeight;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2 leftTopCellPosition;
        [SerializeField] private float indentBetweenCells;
        
        [Header("System")]
        [SerializeField] private float oneTickDuration;
        [SerializeField] private Figure[] existingFigures;
        [SerializeField] private Vector2Int figureSpawnPointOnGameBoard;
        [SerializeField] private float timeBetweenFigureMoves;
        [SerializeField] private float timeBetweenFigureFallSpeedUps;


        public uint ContainersGameBoardWidth => containersGameBoardWidth;
        public uint ContainersGameBoardHeight => containersGameBoardHeight;
        public GameObject ContainerPrefab => containerPrefab;
        public Vector2 LeftTopContainerPosition => leftTopContainerPosition;
        public float IndentBetweenContainers => indentBetweenContainers;
        public float OneTickDuration => oneTickDuration;
        public Figure[] ExistingFigures => existingFigures;
        public Vector2Int FigureSpawnPointOnGameBoard => figureSpawnPointOnGameBoard;
        public uint CellsGameBoardWidth => cellsGameBoardWidth;
        public uint CellsGameBoardHeight => cellsGameBoardHeight;
        public Vector2 LeftTopCellPosition => leftTopCellPosition;
        public float IndentBetweenCells => indentBetweenCells;
        public GameObject CellPrefab => cellPrefab;
        public float TimeBetweenFigureMoves => timeBetweenFigureMoves;
        public float TimeBetweenFigureFallSpeedUps => timeBetweenFigureFallSpeedUps;
    }

    [Serializable]
    public struct Figure
    {
        public RotationState[] rotationStates;
        public uint matrixWidth;
        public Color color;
    }

    [Serializable]
    public struct RotationState
    {
        public Line[] matrix;
    }

    [Serializable]
    public struct Line
    {
        public bool[] line;
    }
}