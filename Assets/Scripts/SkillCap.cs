using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCap : MonoBehaviour
{

    public Texture2D Texture;

    private void Start()
    {
        StartCoroutine(Regenerate(0.5f));
    }

    void Generate()
    {
        Texture2D tex = new Texture2D(55, 55, TextureFormat.ARGB32, false);

        var heighMap = CreateCanvas(tex);
        Color lineColor = RandomColor();
        float startY = heighMap[0] / 2;
        for (int i = 0; i < tex.width; i += 2)
        {
            float startX = i;
            startY += RandomValue(-3, 3);
            Template1(new Vector2(startX, startY), tex, lineColor);
        }
        Crop(heighMap, tex);
        tex.Apply();
        Texture = tex;
        tex.mipMapBias = 0;
        tex.filterMode = FilterMode.Point;
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));        
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Crop(Dictionary<float, float> heightMap, Texture2D tex)
    {
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = tex.height; y > heightMap[x]; y--)
            {
                tex.SetPixel(x, y, Color.clear);
            }
        }
    }

    public IEnumerator Regenerate(float timeout)
    {
        while (true)
        {
            
            yield return new WaitForSeconds(timeout);
            Generate();
        }
    }

    private void Template1(Vector2 start, Texture2D skullcap, Color color)
    {
        skullcap.SetPixel((int)(start.x + 1), (int)(start.y), color);
        skullcap.SetPixel((int)(start.x - 1), (int)(start.y), color);
        skullcap.SetPixel((int)(start.x), (int)(start.y + 1), color);
        skullcap.SetPixel((int)(start.x), (int)(start.y - 1), color);
    }

    private Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private T RandomValue<T>(params T[] values)
    {
        return values[Random.Range(0, values.Length)];
    }

    private Dictionary<float, float> CreateCanvas(Texture2D empty)
    {
        Color[] fill = new Color[empty.width * empty.height];
        Dictionary<float, float> heightMap = new Dictionary<float, float>();

        for (int i = 0; i < fill.Length; i++)
            fill[i] = new Color(0, 0, 0, 0);

        empty.SetPixels(fill, 0);

        Color baseColor = RandomColor();
        Color shadyBaseColor = Color.Lerp(RandomColor(), Color.black, 0.5f);

        for (int x = 0; x < empty.width; x++)
        {
            float _x = (float)x / (empty.width / 3);
            float y = Mathf.Pow(Mathf.Sqrt(_x + 1 - Mathf.Pow(_x - 1, 2)), 0.2f);

            y += 0.5f;
            int _y = (int)(y * (empty.width / 3));
            heightMap.Add(x, _y);
            while (--_y > 0)
            {
                empty.SetPixel(x, _y, baseColor);
            }
        }

        return heightMap;
    }

    private Vector2 Rotate(Vector2 vector, float angel)
    {
        Vector2 rotated = Vector2.zero;
        angel *= Mathf.Deg2Rad;
        rotated.x = vector.x * Mathf.Cos(angel) - vector.y * Mathf.Sin(angel);
        rotated.y = vector.x * Mathf.Sin(angel) + vector.y * Mathf.Cos(angel);

        return rotated;
    }
}