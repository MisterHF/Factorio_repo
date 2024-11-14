using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private CraftingRule resultResearch;
    [SerializeField] List<DictionaryElements<ItemData, int>> requires;

    public void Research()
    {
        for (int i = 0; i < requires.Count; i++) 
        {
            if (!inventory.ContentItem(requires[i].key, requires[i].value))
            {
                return;
            }
        }

        CrafterDataManager.instance.AddCraft(resultResearch);
        GetComponent<Button>().interactable = false;
        for (int i = 0; i < requires.Count; i++) 
        {
            inventory.RemoveItem(requires[i].key, requires[i].value);
        }

        GetComponent<Button>().interactable = false;
    }
}
