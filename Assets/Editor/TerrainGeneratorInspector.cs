using CustomTilemap;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator target = (TerrainGenerator)this.target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            target.GetComponent<TilemapRender>().Clear();
            target.GeneratAndRender();
        }

        if (GUILayout.Button("Clear"))
        {
            target.GetComponent<TilemapRender>().Clear();
        }
    }
}
