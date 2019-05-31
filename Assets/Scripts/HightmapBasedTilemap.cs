using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomTilemap
{
    public interface ITilemap
    {
        int Count { get; }
        int Height { get; }
        int Width { get; }
        ICell GetCell(Vector2Int position);
        Vector2[] GetClosedMesh();
    }

    public class HightmapBasedTilemap : ITilemap
    {
        public int Count => heights.Sum();
        public int Height => heights.Max();
        public int Width => heights.Length;

        private int[] heights;
        private ICell cell;

        public HightmapBasedTilemap(int width, ICell cell)
        {
            this.cell = cell;
            heights = new int[width];
        }

        public void SetHeight(int x, int value)
        {
            if (x < 0 && x >= heights.Length) throw new ArgumentOutOfRangeException("x");
            heights[x] = value;
        }

        ICell ITilemap.GetCell(Vector2Int position)
        {
            if (position.x < 0 && position.x >= heights.Length) throw new ArgumentOutOfRangeException("x");

            return position.y >= heights[position.x] ? null: cell;
        }

        public Vector2[] GetClosedMesh()
        {

            List<Vector2> points = new List<Vector2>();

            for (int x = 0; x < Width; x++)
            {
                points.Add(new Vector2(x - 0.5f, heights[x] - 0.5f));
                points.Add(new Vector2(x + 0.5f, heights[x] - 0.5f));
            }

            points.Add(new Vector2(Width - 0.5f, -0.5f));
            points.Add(new Vector2(-0.5f, -0.5f));

            return points.ToArray();
        }
    }
}