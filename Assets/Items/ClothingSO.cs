using UnityEngine;

[CreateAssetMenu (menuName ="New Clothing")]
public class ClothingSO : ScriptableObject
{
    public enum ClothingType
    {
        Shirt,
        Pants,
        Shoes
    }

    [SerializeField] string displayName;
    [SerializeField] Sprite sprite;
    [SerializeField] ClothingType itemType;
    [SerializeField] int buyingPrice;
    [SerializeField] int sellingPrice;

    public string DisplayName { get { return displayName; } }
    public Sprite Sprite { get { return sprite; } }
    public ClothingType ItemType { get { return itemType; } }
    public int BuyingPrice { get { return buyingPrice; } }
    public int SellingPrice { get { return sellingPrice; } }
}
