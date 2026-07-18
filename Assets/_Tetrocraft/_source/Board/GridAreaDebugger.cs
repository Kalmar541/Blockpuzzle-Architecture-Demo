using UnityEngine;
using Zenject;

namespace Tetracraft.Game
{
    public class GridAreaDebugger : MonoBehaviour
    {
        private GameArea _gameArea;

        [Inject]
        public void Construct(GameArea  gameArea)
        {
            _gameArea = gameArea;
        }

        [NaughtyAttributes.Button]
        public void DebugGrid()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"=== GameArea ({_gameArea.Width}x{_gameArea.Height}) ===");

            var grid = _gameArea.GetGridSnapshot();
            for (int y = _gameArea.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < _gameArea.Width; x++)
                {
                    var block = grid[x, y];
                    sb.Append(block != null ? "[=]" : "  .  ");
                }
                sb.AppendLine($"  y={y}");
            }

            sb.Append("  ");
            for (int x = 0; x < _gameArea.Width; x++)
            {
                sb.Append($" {x}  ");
            }
            sb.AppendLine("  x");

            Debug.Log(sb.ToString());
        }
    }
}
