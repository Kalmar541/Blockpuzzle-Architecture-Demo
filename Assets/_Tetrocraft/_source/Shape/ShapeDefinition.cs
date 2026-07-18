using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GameProject.Game
{
    public class ShapeDefinition 
    {
        public ShapeType ShapeType { get; }
        public bool CanRotate { get; }
        public int BlockCount { get; }

        private Dictionary<ShapeRotation, Position[]> _rotateVariants;

        public ShapeDefinition(Position[] positionsBlock, ShapeType shapeType, bool canRotate = true)
        {
            ShapeType = shapeType;
            CanRotate = canRotate;
            _rotateVariants = new Dictionary<ShapeRotation, Position[]>();
            BlockCount = positionsBlock.Length;
            GenerateRotating(positionsBlock);
        }

        public Position[] GetPositions(ShapeRotation rotation)
        {
            return _rotateVariants.TryGetValue(rotation, out var positions)
                ? positions
                : _rotateVariants[ShapeRotation.Deg_0];
        }

        private void GenerateRotating(Position[] basePositionsBlock)
        {
            _rotateVariants[ShapeRotation.Deg_0] = basePositionsBlock;

            if (!CanRotate)
            {
                for (int i = 1; i < 4; i++)
                {
                    _rotateVariants[(ShapeRotation)i] = basePositionsBlock;
                }
                return;
            }

            var current = basePositionsBlock;
            for (int i = 1; i < 4; i++)
            {
                current = RotateCW(current);
                _rotateVariants[(ShapeRotation)i] = current;
            }
        }

        private Position[] RotateCW(Position[] positions)
        {
            return positions
                .Select(p => new Position(p.Y, -p.X))
                .ToArray();
        }
    }
}