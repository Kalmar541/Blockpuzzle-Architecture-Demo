using System;
using GameProject.Game;
using UnityEngine.InputSystem;

namespace GameProject.Input
{
    public class InputHandler : IDisposable
    {
        private readonly ShapeEngine _engine;
        private readonly GameInput _input;

        public InputHandler(ShapeEngine engine, GameInput input)
        {
            _engine = engine;
            _input = input;

            _input.TetraminoControl.Horizontal.performed += OnHorizontal;
            _input.TetraminoControl.Vertical.performed += OnVertical;
            _input.TetraminoControl.Rotate.performed += OnRotate;
            _input.TetraminoControl.FallFast.performed += OnFallFast;

            _input.TetraminoControl.Enable();
        }

        private void OnHorizontal(InputAction.CallbackContext context)
        {
            if (_engine.IsGameOver) return;
            if (_engine.CurrentTetramino == null) return;

            float value = context.ReadValue<float>();
            int direction = value > 0 ? 1 : -1;

            _engine.TryMove(direction, 0);
        }

        private void OnVertical(InputAction.CallbackContext context)
        {
            if (_engine.IsGameOver) return;
            if (_engine.CurrentTetramino == null) return;

            float value = context.ReadValue<float>();
            int direction = value > 0 ? -1 : 1;

            if (value < 0)
            {
                _engine.TryMove(0, -1);
            }
            else
            {
                _engine.TryMove(0, -1);
            }
        }

        private void OnRotate(InputAction.CallbackContext context)
        {
            if (_engine.IsGameOver) return;
            if (_engine.CurrentTetramino == null) return;

            float value = context.ReadValue<float>();
            bool clockwise = value < 0;

            _engine.TryRotate(clockwise);
        }

        private void OnFallFast(InputAction.CallbackContext context)
        {
            if (_engine.IsGameOver) return;
            if (_engine.CurrentTetramino == null) return;

            _engine.HardDrop();
        }

        public void Dispose()
        {
            _input.TetraminoControl.Horizontal.performed -= OnHorizontal;
            _input.TetraminoControl.Vertical.performed -= OnVertical;
            _input.TetraminoControl.Rotate.performed -= OnRotate;
            _input.TetraminoControl.FallFast.performed -= OnFallFast;

            _input.TetraminoControl.Disable();
        }
    }
}