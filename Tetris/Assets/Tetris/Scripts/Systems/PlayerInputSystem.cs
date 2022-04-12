using Leopotam.Ecs;
using Tetris.Data;
using Tetris.Scripts.Components;
using Tetris.Scripts.Components.Events;
using UnityEngine;

namespace Tetris.Scripts.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private PlayerInput _playerInput;
        private EcsEntity _playerInputEntity;
        private readonly EcsWorld _world = null;
        private readonly RuntimeData _runtimeData = null;

        private bool _moveIsPressing = false;
        private bool _fallSpeedUpIsPressing = false;

        public void Init()
        {
            _playerInput = new PlayerInput();

            _playerInputEntity = _world.NewEntity();
            ref var playerInputComponent = ref _playerInputEntity.Get<PlayerInputComponent>();

            _playerInput.OnGameBoard.Moving.started += context => _moveIsPressing = true;
            _playerInput.OnGameBoard.Moving.performed += context => OnMoving(context.ReadValue<float>());
            _playerInput.OnGameBoard.Moving.canceled += context => _moveIsPressing = false;
            
            _playerInput.OnGameBoard.Rotation.performed += context => OnRotating();
            
            _playerInput.OnGameBoard.Fallspeedup.started += context => _fallSpeedUpIsPressing = true;
            _playerInput.OnGameBoard.Fallspeedup.performed += context => OnFallSpeedUp();
            _playerInput.OnGameBoard.Fallspeedup.canceled += context => _fallSpeedUpIsPressing = false;
            
            _playerInput.General.MouseDeltaInput.performed += context => OnMouseMove(context.ReadValue<Vector2>());

            _playerInput.General.Exit.performed += context => OnExit();

            _playerInput.General.Reload.performed += context => OnReload();

            _playerInput.General.Pause.performed += context => OnPause(); 

            _playerInput.Enable();

            _runtimeData.PlayerInputEntity = _playerInputEntity;
        }

        public void Run()
        {
            ref var playerMoveComponent = ref _playerInputEntity.Get<PlayerInputComponent>();

            playerMoveComponent.FallSpeedUpIsPressing = _fallSpeedUpIsPressing;

            if (_moveIsPressing)
                playerMoveComponent.MoveInput = _playerInput.OnGameBoard.Moving.ReadValue<float>();
            else
                playerMoveComponent.MoveInput = 0;
        }

        public void Destroy()
        {
            _playerInput.Disable();
        }

        private void OnMoving(float moveInput)
        {
            ref var moveInputEvent = ref _playerInputEntity.Get<MoveInputEvent>();
            moveInputEvent.MoveInput = moveInput;
        }

        private void OnRotating() => _playerInputEntity.Get<RotateInputEvent>();

        private void OnFallSpeedUp() => _playerInputEntity.Get<FigureFallingSpeedUpInputEvent>();

        private void OnMouseMove(Vector2 delta)
        {
            ref var mouseDeltaEvent = ref _playerInputEntity.Get<MouseMoveEvent>();
            mouseDeltaEvent.Delta = delta;
        }

        private void OnExit() => _playerInputEntity.Get<ExitInputEvent>();

        private void OnReload() => _playerInputEntity.Get<ReloadInputEvent>();

        private void OnPause() => _playerInputEntity.Get<PauseInputEvent>();
    }
}