using NUnit.Framework.Internal;
using System;
using UnityEngine;

namespace GameProject.Game
{
    public class ShapeEngine
    {
        public event Action<Shape> OnPieceLocked;
        public event Action<Shape> OnSpawnedTetramino;
        public event Action<int> OnLinesCleared;
        public event Action<string> OnGameOver;

        private readonly GameArea _gameArea;
        private Shape _currentShape;
        private float _gravityTimer;
        private DifficalHandler _difficalHandler;
        private readonly float _baseGravitySpeed;

        public Shape CurrentTetramino => _currentShape;
        public bool IsGameOver { get; private set; }
        public string GameOverReason { get; private set; }

        public ShapeEngine(GameArea gameArea, DifficalHandler difficalHandler)
        {
            _gameArea = gameArea;
            _difficalHandler = difficalHandler;

            _baseGravitySpeed = _difficalHandler.GetSpeedBlock();
            _gravityTimer = _baseGravitySpeed;
            IsGameOver = false;
            GameOverReason = string.Empty;
        }

        private bool IsValidPosition(Shape tetramino)
        {
            var positions = tetramino.GetWorldPositions();
            foreach (var pos in positions)
            {
                if (!_gameArea.IsInside(pos.X, pos.Y))
                    return false;
                if (_gameArea.IsOccupied(pos.X, pos.Y))
                    return false;
            }
            return true;
        }

  
        public bool SpawnNewShape(Shape shape)
        {
            if (IsGameOver) return false;

            if (!IsValidPosition(shape))
            {
                IsGameOver = true;
                GameOverReason = "Spawn blocked - field is full";
                OnGameOver?.Invoke(GameOverReason);
                return false;
            }

            _currentShape = shape;
            OnSpawnedTetramino?.Invoke(_currentShape);

            _gravityTimer = _baseGravitySpeed;
            return true;
        }

        public void UpdateGravity(float deltaTime)
        {
            if (IsGameOver || _currentShape == null) return;

            _gravityTimer -= deltaTime;
            if (_gravityTimer <= 0)
            {
                if (!TryMove(0, -1))
                {
                    LockPiece();
                }
                _gravityTimer = _baseGravitySpeed;
            }
        }

        public bool TryMove(int dx, int dy)
        {
            if (IsGameOver || _currentShape == null) return false;

            var test = _currentShape.Clone();
            test.Move(new Position(dx, dy));

            if (IsValidPosition(test))
            {
                _currentShape.Move(new Position(dx, dy));
                return true;
            }

            return false;
        }

        public bool TryRotate(bool clockwise)
        {
            if (IsGameOver || _currentShape == null) return false;
            if (!_currentShape.CanRotate) return false;

            var test = _currentShape.Clone();
            test.Rotate(clockwise);

            if (IsValidPosition(test))
            {
                _currentShape.Rotate(clockwise);
                return true;
            }

            return false;
        }

        public void HardDrop()
        {
            if (IsGameOver || _currentShape == null) return;

            while (TryMove(0, -1)) { }
            LockPiece();
        }

        private void LockPiece()
        {
            if (_currentShape == null) return;

            var positions = _currentShape.GetWorldPositions();
            var blocks = _currentShape.Blocks;

            for (int i = 0; i < positions.Length; i++)
            {
                var pos = positions[i];
                var block = blocks[i];
                _gameArea.SetBlock(pos.X, pos.Y, block);
            }

            OnPieceLocked?.Invoke(_currentShape);

            int cleared = _gameArea.ClearFullLines();
            if (cleared > 0)
            {
                OnLinesCleared?.Invoke(cleared);
            }
        }

        public void Reset()
        {
            _gameArea.ClearAll();
            _currentShape = null;
            IsGameOver = false;
            GameOverReason = string.Empty;
            _gravityTimer = _baseGravitySpeed;
        }
    }
}