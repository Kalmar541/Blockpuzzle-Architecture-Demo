using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject.Game
{
    public class Shape 
    {
        public event Action<Position> OnChangeOrignPosition;

        public ShapeDefinition Definition { get; }
        public Position Origin { get; private set; }
        public ShapeRotation Rotation { get; private set; }
        public IReadOnlyList<Block> Blocks { get; }

        public ShapeType Type => Definition.ShapeType;
        public bool CanRotate => Definition.CanRotate;
        public int BlockCount => Blocks.Count;

        public Shape(ShapeDefinition definition, Position origin, Block[] blocks)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            if (blocks == null || blocks.Length == 0)
                throw new ArgumentException("Blocks cannot be null or empty");

            if (blocks.Length != definition.BlockCount)
                throw new ArgumentException($"Block count mismatch. Expected {definition.BlockCount}, got {blocks.Length}");

            Definition = definition;
            Origin = origin;
            Rotation = ShapeRotation.Deg_0;
            Blocks = blocks;
        }

        public Position[] GetWorldPositions()
        {
            var localPositions = Definition.GetPositions(Rotation);
            var worldPositions = new Position[localPositions.Length];

            for (int i = 0; i < localPositions.Length; i++)
            {
                var local = localPositions[i];
                worldPositions[i] = new Position(
                    Origin.X + local.X,
                    Origin.Y + local.Y
                );
            }

            return worldPositions;
        }

        public IReadOnlyList<Position> GetLocalPositions()
        {
            return Definition.GetPositions(Rotation);
        }

        public void Rotate(bool clockwise)
        {
            if (!CanRotate) return;

            Rotation = clockwise
                ? (ShapeRotation)(((int)Rotation + 1) % 4)
                : (ShapeRotation)(((int)Rotation + 3) % 4);

            OnChangeOrignPosition?.Invoke(Origin);
        }

        public void SetRotation(ShapeRotation rotation)
        {
            if (!CanRotate) return;
            Rotation = rotation;
        }

        public void Move(Position offset)
        {
            Origin = new Position(Origin.X + offset.X, Origin.Y + offset.Y);
            OnChangeOrignPosition?.Invoke(Origin);
        }

        public void SetOrigin(Position newOrigin)
        {
            Origin = newOrigin;
        }

        public Shape Clone()
        {
            var clonedBlocks = Blocks.Select(b => new Block(b.MaterialType, b.Id)).ToArray();
            var clone = new Shape(Definition, Origin, clonedBlocks);
            clone.SetRotation(Rotation);
            return clone;
        }

        public bool ContainsPosition(Position worldPos)
        {
            var localPositions = Definition.GetPositions(Rotation);
            foreach (var local in localPositions)
            {
                var worldX = Origin.X + local.X;
                var worldY = Origin.Y + local.Y;
                if (worldX == worldPos.X && worldY == worldPos.Y)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[Shape] Type: {Type}, Origin: ({Origin.X}, {Origin.Y}), " +
                   $"Rotation: {Rotation}, Blocks: {BlockCount}";
        }
    }
}