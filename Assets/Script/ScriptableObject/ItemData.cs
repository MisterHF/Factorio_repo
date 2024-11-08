using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string description;
    public string nameItem;
    public Sprite sprite;
    public ObjectType Type;
}

public enum ObjectType
{
    Building,
    Item
}