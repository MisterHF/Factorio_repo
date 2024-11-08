using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel; //UI
    [SerializeField] Transform content; // Ressource entr�e

    
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private CraftingRule SelectedCraft;

    public CraftingRule SelectedCraft1 { set { SelectedCraft = value; } }

    private List<DefaultSlot> slots = new List<DefaultSlot>();

    [SerializeField] private GameObject ParentSlot;
    [SerializeField] private DefaultSlot resultCrafting;
    [SerializeField] private List<GameObject> allButtonsInContent;

    private void Awake()
    {
        CrafterDataManager.instance.addCraftEvent += UpdateCraftPossibility;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCraftPossibility();
    }

    public void UpdateCraftPossibility()
    {
        DestroyAllButtons();
        for (int i = 0; i < CrafterDataManager.instance.crafts.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = CrafterDataManager.instance.crafts[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite = CrafterDataManager.instance.crafts[i].result.sprite;
            btn.GetComponent<SpawnRequireSlots>().RequireSlot1 = CrafterDataManager.instance.crafts[i];
            btn.GetComponent<SpawnRequireSlots>().CraftingController = this;

            allButtonsInContent.Add(btn);
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

    private void DestroyAllButtons()
    {
        for (int i = 0; i < allButtonsInContent.Count; i++) 
        {
            Destroy(allButtonsInContent[i]);
        }
    }

}
