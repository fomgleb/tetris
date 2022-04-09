using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

namespace Tetris.Scripts.MonoBehaviours
{
    // public class PlayerInput : MonoBehaviour
    // {   
    //     private FigureMoveInput _figureMoveInput;
    //
    //     private void Awake()
    //     {
    //         _figureMoveInput = new FigureMoveInput();
    //         
    //         _figureMoveInput.OnGameBoard.Rotation.performed += context => OnRotate();
    //         
    //     }
    //
    //     private void Update()
    //     {
    //         //Debug.Log(_figureMoveInput.OnGameBoard.Moving.ReadValue<float>());
    //     }
    //
    //     private void OnEnable()
    //     {
    //         _figureMoveInput.Enable();
    //     }
    //
    //     private void OnDisable()
    //     {
    //         _figureMoveInput.Disable();
    //     }
    //
    //     private void OnRotate()
    //     {
    //         Debug.Log("rotate");
    //     }
    // }
}