using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<ShirtSO> temporaryShirts;
    public List<ShirtSO> Shirts { get; private set; } = new List<ShirtSO>();

    private void Start()
    {
        foreach (ShirtSO shirt in temporaryShirts)
        {
            AddShirt(shirt);
        }
    }

    public void AddShirt(ShirtSO newShirt)
    {
        Shirts.Add(newShirt);
    }

    public void RemoveShirt(ShirtSO shirt)
    {
        Shirts.Remove(shirt);
    }
}
