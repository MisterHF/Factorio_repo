using System.Collections.Generic;
using System.Linq;
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

    public CraftingRule SelectedCraft1 { set { SelectedCraft = value; } }

    private List<DefaultSlot> slots = new List<DefaultSlot>();

    [SerializeField] private GameObject ParentSlot;
    [SerializeField] private DefaultSlot resultCrafting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < craft.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = craft[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite = craft[i].result.sprite;
            btn.GetComponent<SpawnRequireSlots>().RequireSlot1 = craft[i];
            btn.GetComponent<SpawnRequireSlots>().CraftingController = this;
        }
    }

    public void Crafting()
    {

        DefaultSlot[] defaultSlot = ParentSlot.GetComponentsInChildren<DefaultSlot>();
        for (int i = 0; i < SelectedCraft.requires.Count; i++)
        {
            if (SelectedCraft.countPerRaquires[i] > defaultSlot[i].Count) { return; }
        }
        resultCrafting.Img1.sprite = SelectedCraft.result.sprite;
        resultCrafting.Data = SelectedCraft.result;
        resultCrafting.Count++;
        resultCrafting.Img1.color = Color.white;
        for (int i = 0; i < defaultSlot.Length; i++)
        {
            if (defaultSlot[i].ItemAccepted == SelectedCraft.requires[i])
            {
                if (defaultSlot[i].Count > 0)
                    defaultSlot[i].Count -= SelectedCraft.countPerRaquires[i];
            }
        }
    }
}
