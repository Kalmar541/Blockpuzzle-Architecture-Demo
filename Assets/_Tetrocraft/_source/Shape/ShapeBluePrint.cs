using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "ShapeBluePrint", menuName = "Scriptable Objects/Shape BluePrint")]
    public class ShapeBluePrint : ScriptableObject
    {
        [Header("=== SHAPE DATA ===")]
        public Vector2Int[] Positions;
        [field: SerializeField] public ShapeType ShapeType {get; private set;}   
        [field: SerializeField] public bool IsCanRotate {get; private set;} = true;

        [Header("=== PREVIEW ===")]
        [SerializeField, TextArea(3, 6)] private string _debug;

#if UNITY_EDITOR
        private void OnValidate()
        {
            GenerateDebugVisualization();
        }
#endif

        private void GenerateDebugVisualization()
        {
            if (Positions == null || Positions.Length == 0)
            {
                _debug = "⚠️ No positions defined!";
                return;
            }

            int minX = Positions.Min(p => p.x);
            int maxX = Positions.Max(p => p.x);
            int minY = Positions.Min(p => p.y);
            int maxY = Positions.Max(p => p.y);

            var positionSet = new HashSet<(int x, int y)>();
            foreach (var pos in Positions)
            {
                positionSet.Add((pos.x, pos.y));
            }

            var sb = new StringBuilder();

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    bool hasBlock = positionSet.Contains((x, y));
                    bool isCenter = (x == 0 && y == 0);

                    if (isCenter && hasBlock)
                        sb.Append("[X]");
                    else if (isCenter)
                        sb.Append("[X]");
                    else if (hasBlock)
                        sb.Append("[=]");
                    else
                        sb.Append("  .  ");
                }
                sb.AppendLine();
            }

            _debug = sb.ToString();
        }
    }
}