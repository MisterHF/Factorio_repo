using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRule", menuName = "Scriptable Objects/CraftingRule")]
public class CraftingRule : ScriptableObject
{
    public ItemData result;
    public List <ItemData> requires = new();
    public List <int> countPerRaquires = new();
}
