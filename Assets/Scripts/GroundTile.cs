using UnityEngine;

namespace CustomTilemap
{
    public interface ICell
    {
        void Refresh(Vector2Int position, ITilemap tilemap, GameObject gameObject);
    }

    [CreateAssetMenu(menuName = "GroundTile")]
    public class GroundTile : ScriptableObject, ICell
    {
        public Sprite Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight, Other;

        public void Refresh(Vector2Int position, ITilemap tilemap, GameObject gameObject)
        {
            var render = gameObject.GetComponent<SpriteRenderer>();
            render.sprite = Other;

            if (Exist(position + Vector2Int.right, tilemap) &&
                Exist(position + Vector2Int.left, tilemap) &&
                !Exist(position + Vector2Int.up, tilemap)) render.sprite = Top;
            else if (Exist(position + Vector2Int.right, tilemap) &&
                     !Exist(position + Vector2Int.left, tilemap) &&
                     !Exist(position + Vector2Int.up, tilemap)) render.sprite = TopLeft;
            else if (!Exist(position + Vector2Int.right, tilemap) &&
                     Exist(position + Vector2Int.left, tilemap) &&
                     !Exist(position + Vector2Int.up, tilemap)) render.sprite = TopRight;

        }

        public bool Exist(Vector2Int position, ITilemap titleMap)
        {
            if (position.x < 0 || position.x >= titleMap.Width) return false;
            
            return titleMap.GetCell(position) != null;
        }
    }
}