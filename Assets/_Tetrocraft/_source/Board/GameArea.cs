using GameProject.Game;
using System;
using System.Collections.Generic;
using Unity.Android.Gradle;

public class GameArea
{
    public struct ChangeBlockInfo
    {
        public int X;
        public int Y;
        public Block Block;

        public ChangeBlockInfo(int x, int y, Block block)
        {
            X = x;
            Y = y;
            Block = block;
        }
    }

    public event Action<int> OnLinesDelete;
    public event Action<ChangeBlockInfo> OnChangeGrid;


    public int Width { get; private set; }
    public int Height { get; private set; }
  
    private Block[,] _grid;

    public GameArea(int width, int heigth)
    {
        if (width < 4 || heigth < 4)
            throw new ArgumentException("Size Area no valid");

        Width = width;
        Height = heigth;

        _grid = new Block[Width, Height];
    }

    public void SetBlock(int x, int y, Block block)
    {
        if (IsInside(x, y))
        {
            _grid[x, y] = block;
            OnChangeGrid?.Invoke(new(x, y, block));
        }
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public bool IsOccupied(int x, int y)
    {
        if (!IsInside(x, y)) return true;
        return _grid[x, y] != null;
    }

    public bool IsLineFull(int y)
    {
        if (y < 0 || y >= Height) return false;

        for (int x = 0; x < Width; x++)
        {
            if (_grid[x, y] == null)
                return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_grid[x, y] != null)
                    return false;
            }
        }
        return true;
    }

    public Block GetBlock(int x, int y)
    {
        if (!IsInside(x, y)) return null;
        return _grid[x, y];
    }

    public Block[,] GetGridSnapshot()
    {
        return (Block[,])_grid.Clone();
    }
    public IEnumerable<Position> GetOccupiedPositions()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_grid[x, y] != null)
                {
                    yield return new Position(x, y);
                }
            }
        }
    }

    public int OccupiedCount
    {
        get
        {
            int count = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (_grid[x, y] != null)
                        count++;
                }
            }
            return count;
        }
    }

    public void DeleteLine(int y)
    {
        if (y < 0 || y >= Height) return;

        // 1. Удаляем строку y
        for (int x = 0; x < Width; x++)
        {
            _grid[x, y] = null;
            OnChangeGrid?.Invoke(new (x, y, null)); // ← блок удалён
        }

        // 2. Сдвигаем все строки выше на 1 вниз
        for (int row = y + 1; row < Height; row++)
        {
            for (int x = 0; x < Width; x++)
            {
                // Блок из row переезжает в row-1
                var block = _grid[x, row];
                _grid[x, row - 1] = block;
                _grid[x, row] = null;

                // Событие: блок появился в row-1
                OnChangeGrid?.Invoke(new (x, row - 1, block));

                // Событие: блок исчез из row
                OnChangeGrid?.Invoke(new (x, row, null));
            }
        }
    }

    public List<int> FindFullLines()
    {
        var lines = new List<int>();
        for (int y = 0; y < Height; y++)
        {
            if (IsLineFull(y))
            {
                lines.Add(y);
            }
        }
        return lines;
    }

    public int ClearFullLines()
    {
        var lines = FindFullLines();
        if (lines.Count == 0) return 0;

        for (int i = lines.Count - 1; i >= 0; i--)
        {
            DeleteLine(lines[i]);
        }

        return lines.Count;
    }

    public void ClearAll()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _grid[x, y] = null;
                OnChangeGrid?.Invoke(new(x, y, null));
            }
        }
    }
}