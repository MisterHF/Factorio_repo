using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[DefaultExecutionOrder(-100)]
public class CrafterDataManager : MonoBehaviour
{
    public static CrafterDataManager instance;

    public List<CraftingRule> crafts = new();
    public Action addCraftEvent;

    private void Awake()
    {
        instance = this;
    }
    public void AddCraft(CraftingRule NewCraft)
    {
        if (crafts.Contains(NewCraft)) { return; }

        crafts.Add(NewCraft);
        addCraftEvent?.Invoke();
    }
}
