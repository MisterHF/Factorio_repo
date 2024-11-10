using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ItemData
{
    public float heatResistance;
    public GameObject prefab;
    public BuildingRarity Rarity;
}

public enum BuildingRarity
{
    Common = 10,
    Rare = 30,
    Legendary = 90
}