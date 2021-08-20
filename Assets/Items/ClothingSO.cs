using UnityEngine;


/// <summary>
/// Clothing assets configuration scriptable object
/// </summary>
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
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite sideSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] ClothingType itemType;
    [SerializeField] int buyingPrice;
    [SerializeField] int sellingPrice;

    public string DisplayName { get { return displayName; } }
    public Sprite FrontSprite { get { return frontSprite; } }
    public Sprite BackSprite { get { return backSprite; } }
    public Sprite SideSprite { get { return sideSprite; } }
    public ClothingType ItemType { get { return itemType; } }
    public int BuyingPrice { get { return buyingPrice; } }
    public int SellingPrice { get { return sellingPrice; } }
}
