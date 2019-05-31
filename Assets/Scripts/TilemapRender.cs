using System.Linq;
using UnityEngine;

namespace CustomTilemap
{
    public class TilemapRender : MonoBehaviour
    {

        public void Render(ITilemap tileMap)
        {
            Clear();
            for (int x = 0; x < tileMap.Width; x++)
            for (int y = 0; y <= tileMap.Height; y++)
                tileMap.GetCell(new Vector2Int(x, y))?
                    .Refresh(new Vector2Int(x, y), tileMap, CreateEmpty(new Vector2Int(x, y)));
        }

        public void Clear()
        {
            foreach (Transform child in transform.OfType<Transform>().ToList())
            {
#if UNITY_EDITOR
                DestroyImmediate(child.gameObject);
#else
                Destroy(child.gameObject);
#endif
            }
        }

        public GameObject CreateEmpty(Vector2Int position)
        {
            var result = new GameObject(position.ToString());
            var transform = result.GetComponent<Transform>();
            transform.parent = GetComponent<Transform>();
            transform.localPosition = new Vector3(position.x, position.y, 0);
            result.AddComponent<SpriteRenderer>();

            return result;
        }
    }
}

