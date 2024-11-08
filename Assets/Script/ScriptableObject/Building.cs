using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ItemData
{
    public float heatResistance;
    public GameObject prefab;
}

