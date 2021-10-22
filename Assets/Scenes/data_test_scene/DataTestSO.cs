using System;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom / DataTest")]
public class DataTestSO : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private float price;
    [SerializeField] private int buff;
    [SerializeField] private ItemConsumableType type;
    [SerializeField, TextArea(1, 3)] private string shortDescription;

    public string Name => name;
    public float Price => price;
    public int Buff => buff;
    public ItemConsumableType Type => type;
    public string ShortDescription => shortDescription;

    public void UpdateData(DataTestHandler dataHandler)
    {
        this.name = dataHandler.name;
        this.price = dataHandler.price;
        this.buff = dataHandler.buff;
        this.type = dataHandler.type;
        this.shortDescription = dataHandler.shortDescription;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

public class DataTestHandler
{
    public string name;
    public float price;
    public int buff;
    public ItemConsumableType type;
    public string shortDescription;

    public DataTestHandler(string name, string price, string buff, string type, string shortDescription)
    {
        float parsedPrice = float.Parse(price, CultureInfo.InvariantCulture);
        int parsedBuff = int.Parse(buff);
        ItemConsumableType parsedType = StringToEnum<ItemConsumableType>(type);

        this.name = name;
        this.price = parsedPrice;
        this.buff = parsedBuff;
        this.type = parsedType;
        this.shortDescription = shortDescription;
    }

    private T StringToEnum<T>(string str) where T : struct
    {
        try
        {
            T res = (T)Enum.Parse(typeof(T), str);

            if (!Enum.IsDefined(typeof(T), res)) return default(T);

            return res;
        }
        catch
        {
            return default(T);
        }
    }

}

public enum ItemConsumableType
{
    Food,
    Drink,
    Ingredient,
}
