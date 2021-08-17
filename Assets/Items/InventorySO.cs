using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Inventory List")]
public class InventorySO : ScriptableObject
{
    [SerializeField] List<ClothingSO> container;

    public List<ClothingSO> Container { get { return container; } }

    public void AddItem(ClothingSO clothing)
    {
        Container.Add(clothing);
    }

    public void RemoveItem(ClothingSO clothing)
    {
        if (!HasItem(clothing)) return;

        Container.Remove(clothing);
    }

    public bool HasItem(ClothingSO clothing)
    {
        return (Container.Find(x => x == clothing));
    }
}
