using Leopotam.Ecs;
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

            _playerInput.Enable();
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

        private void OnRotating()
        {
            ref var rotateInputEvent = ref _playerInputEntity.Get<RotateInputEvent>();
        }

        private void OnFallSpeedUp()
        {
            ref var onFallSpeedUpInputEvent = ref _playerInputEntity.Get<FigureFallingSpeedUpInputEvent>();
        }
    }
}