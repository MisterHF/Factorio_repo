using UnityEngine;

[CreateAssetMenu(fileName = "Ressour", menuName = "Scriptable Objects/Ressour")]
public class Resources : ItemData
{
   
    public float durability;
    public enum Type
    {
        ORE, GEM, OTHER
    }

    public Type type;
}
