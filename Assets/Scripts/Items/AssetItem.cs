using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
class AssetItem : ScriptableObject, IItem
{
    public string Name { get; }
    public Texture2D UIIcon { get; }

    [SerializeField] private string name;
    [SerializeField] private Texture2D uiIcon;
}

