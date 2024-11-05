using System.Collections.Generic;
using UnityEngine;

public class SpawnRequireSlots : MonoBehaviour
{
    private GameObject CraftingSlots;
    private CraftingRule Craft;

    [SerializeField] private GameObject Slot;
    [HideInInspector] public List <DefaultSlot> slots = new List <DefaultSlot> ();

    private CraftingController craftingController;

    public CraftingController CraftingController { set { craftingController = value; } }
    public CraftingRule RequireSlot1 { get { return Craft; } set { Craft = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CraftingSlots = GetComponentInParent<SpawnSlotsCrafter>().SlotsUI;
    }

    public void SpawnSlot()
    {
        if (CraftingSlots.transform.childCount > 0) 
        {
            for (int i = 0; i < CraftingSlots.transform.childCount; i++)
            {
                slots.Clear ();
                Destroy(CraftingSlots.transform.GetChild(i).gameObject);
            }
        }

        craftingController.SelectedCraft1 = Craft;


        for (int i = 0; i < Craft.requires.Count; i++)
        {
            GameObject slot = Instantiate(Slot, CraftingSlots.transform);
            DefaultSlot defaultSlot = slot.GetComponent<DefaultSlot>();
            slots.Add (defaultSlot);
            defaultSlot.ItemAccepted = Craft.requires[i];
            defaultSlot.AcceptAll = false;
            defaultSlot.IsHighlighted = true;
            Color color = defaultSlot.Img1.color;
            color = Color.white;
            color.a = 0.25f;
            defaultSlot.Img1.color = color;
            defaultSlot.Img1.sprite = Craft.requires[i].sprite;
            
        }
    }
}