using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ItemData
{
    public enum Type
    {
        None,
    }
    public Type type;   
    public float heatResistance;
    public GameObject prefab;
}
