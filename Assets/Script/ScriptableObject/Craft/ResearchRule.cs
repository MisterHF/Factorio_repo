using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchRule", menuName = "Scriptable Objects/ResearchRule")]
public class ResearchRule : ScriptableObject
{
    public ItemData ObjectUnlock;
    public List<DictionaryElements<ItemData, int>> RequiresObjects;
}
