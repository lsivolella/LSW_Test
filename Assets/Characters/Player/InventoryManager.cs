using System.Collections.Generic;
using UnityEngine;
using static ClothingSO;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySO inventory;
    [SerializeField] List<ClothingSO> temporaryShirts;
    [SerializeField] List<ClothingSO> temporaryPants;
    [SerializeField] List<ClothingSO> temporaryShoes;
    public InventorySO Inventory { get { return inventory; } }
    public List<ClothingSO> Shirts { get; private set; } = new List<ClothingSO>();
    public List<ClothingSO> Pants { get; private set; } = new List<ClothingSO>();
    public List<ClothingSO> Shoes { get; private set; } = new List<ClothingSO>();

    private void Start()
    {
        foreach (ClothingSO shirt in temporaryShirts)
        {
            AddItemToInventory(shirt);
        }

        foreach (ClothingSO pants in temporaryPants)
        {
            AddItemToInventory(pants);
        }

        foreach (ClothingSO shoes in temporaryShoes)
        {
            AddItemToInventory(shoes);
        }
    }


    public void AddItemToInventory(ClothingSO newClothing)
    {
        switch (newClothing.ItemType)
        {
            case ClothingType.Shirt:
                Shirts.Add(newClothing);
                break;
            case ClothingType.Pants:
                Pants.Add(newClothing);
                break;
            case ClothingType.Shoes:
                Shoes.Add(newClothing);
                break;
        }
    }

    // TODO: add safety verifications
    public void RemoveShirt(ClothingSO shirt)
    {
        Shirts.Remove(shirt);
    }

    public bool HasClothePiece(List<ClothingSO> inventory, ClothingSO shirt)
    {
        if (inventory.Find(x => x.DisplayName == shirt.DisplayName))
            return true;
        else
            return false;
    }
}
