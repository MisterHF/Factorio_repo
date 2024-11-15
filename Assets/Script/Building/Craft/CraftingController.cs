using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : Controller
{
    [SerializeField] private GameObject craftPanel; //UI
    [SerializeField] Transform content; // Ressource entrï¿½e

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private CraftingRule SelectedCraft;

    public CraftingRule SelectedCraft1
    {
        set { SelectedCraft = value; }
    }

    private List<DefaultSlot> slots = new List<DefaultSlot>();

    [SerializeField] private GameObject ParentSlot;
    [SerializeField] private DefaultSlot resultCrafting;
    [SerializeField] private List<GameObject> allButtonsInContent;

    private void Awake()
    {
        CrafterDataManager.instance.addCraftEvent += UpdateCraftPossibility;
    }
    void Start()
    {
        UpdateCraftPossibility();
    }

    public override void SetItemCountForMultiSlot(int _count, ItemData _data)
    {
        for (int i = 0; i < ParentSlot.transform.childCount; i++)
        {
            if (ParentSlot.transform.GetChild(i).GetComponent<DefaultSlot>().ItemAccepted == _data)
            {
                ParentSlot.transform.GetChild(i).GetComponent<DefaultSlot>().Data = _data;
                ParentSlot.transform.GetChild(i).GetComponent<DefaultSlot>().Count += _count;
            }
            else
            {
                return;
            }
        }
    }

    public override bool HasCraftSelected()
    {
        if (SelectedCraft != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void UpdateCraftPossibility()
    {
        DestroyAllButtons();
        for (int i = 0; i < CrafterDataManager.instance.crafts.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                CrafterDataManager.instance.crafts[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite =
                CrafterDataManager.instance.crafts[i].result.sprite;
            btn.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
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
            if (SelectedCraft.countPerRaquires[i] > defaultSlot[i].Count)
            {
                return;
            }
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