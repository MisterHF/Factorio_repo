using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel; //UI
    [SerializeField] Transform content; // Ressource entrée

    [SerializeField] private List<CraftingRule> craft;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private CraftingRule SelectedCraft;

    private List<DefaultSlot> slots = new List<DefaultSlot>();

    [SerializeField] GameObject ParentSlot;
    [SerializeField] DefaultSlot resultCrafting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < craft.Count; i++) 
        {
            GameObject btn =  Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = craft[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite = craft[i].result.sprite;
            btn.GetComponent<SpawnRequireSlots>().RequireSlot1 = craft[i];
        }
    }

    public void Crafting()
    {
        
       DefaultSlot[] defaultSlot = ParentSlot.GetComponentsInChildren<DefaultSlot>();
        for (int i = 0; i < SelectedCraft.requires.Count; i++)
        {
            if(SelectedCraft.requires[i] != defaultSlot[i].Data) { return; }
        }
        resultCrafting.Data = SelectedCraft.result;
        resultCrafting.Count = 1; 
    }
}
