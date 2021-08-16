using UnityEngine;

[CreateAssetMenu (menuName ="Clothing/Shirt")]
public class ShirtSO : ScriptableObject
{
    [SerializeField] string displayName;
    [SerializeField] Sprite sprite;

    public string DisplayName { get { return displayName; } }
    public Sprite Sprite { get { return sprite; } }
}
