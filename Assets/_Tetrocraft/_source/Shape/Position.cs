using System;

namespace GameProject.Game
{
    public readonly struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Offset(int dx, int dy) => new(X + dx, Y + dy);

        public override bool Equals(object obj) => obj is Position p && p.X == X && p.Y == Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}