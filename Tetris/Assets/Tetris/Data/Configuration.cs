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

        [Header("Next figure board")]
        [SerializeField] private uint nextFigureBoardWidth;
        [SerializeField] private uint nextFigureBoardHeight;
        [SerializeField] private Vector2 leftTopElementPositionOnNextFigureBoard;
        
        [Header("System")]
        [SerializeField] private float oneTickDuration;
        [SerializeField] private Figure[] existingFigures;
        [SerializeField] private Vector2Int figureSpawnPointOnGameBoard;
        [SerializeField] private float timeBetweenFigureMoves;
        [SerializeField] private float timeAfterFirstMoveClick;
        [SerializeField] private float timeBetweenFigureFallSpeedUps;
        [SerializeField] private int scoreForFigureFell;
        [SerializeField] private int scoreForRowRemoved;
        [SerializeField] private string progressDataFileName;

        public int ScoreForFigureFell => scoreForFigureFell;
        public int ScoreForRowRemoved => scoreForRowRemoved;
        public float TimeAfterFirstMoveClick => timeAfterFirstMoveClick;
        public uint NextFigureBoardWidth => nextFigureBoardWidth;
        public uint NextFigureBoardHeight => nextFigureBoardHeight;
        public Vector2 LeftTopElementPositionOnNextFigureBoard => leftTopElementPositionOnNextFigureBoard;
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
        public string ProgressDataFileName => progressDataFileName;
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