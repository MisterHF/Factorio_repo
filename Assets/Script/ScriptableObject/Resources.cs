using UnityEngine;

[CreateAssetMenu(fileName = "Ressour", menuName = "Scriptable Objects/Ressour")]
public class Resources : ItemData
{
   
    public float durability;

    public Type type;
}

public enum Type
{
    ORE,
    GEM,
    OTHER
}
