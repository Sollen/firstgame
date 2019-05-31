using CustomTilemap;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int Height;
    public int Width;
    public GroundTile groundTile;

    [SerializeField] private bool isGenerateOnStart;
    public void Start()
    {
        if(isGenerateOnStart) GeneratAndRender();
    }

    public void GeneratAndRender()
    {
        var tileMap = Generator();
        GetComponent<TilemapRender>().Render(tileMap);
        GetComponent<PolygonCollider2D>().points = tileMap.GetClosedMesh();
    }

    public ITilemap Generator()
    {
        int groundHeight = 5;
        var tileMap = new HightmapBasedTilemap(Width, groundTile);
        for (int x = 0; x < Width; x++)
        {
            if(x % 2 == 0)
                groundHeight += Random.Range(-1, 2);

            tileMap.SetHeight(x, groundHeight);


            //for (int y = groundHeight; y > 0; y--)
            //{
            //    var cell = Instantiate(Cell, Zero);
            //    cell.transform.localPosition = new Vector3(x, y, 0);
            //}
        }

        return tileMap;
    }
}
