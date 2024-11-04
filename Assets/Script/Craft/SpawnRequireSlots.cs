using System.Collections.Generic;
using UnityEngine;

public class SpawnRequireSlots : MonoBehaviour
{
    private GameObject CraftingSlots;
    private CraftingRule RequireSlot;

    [SerializeField] private GameObject Slot;
    [HideInInspector] public List <DefaultSlot> slots = new List <DefaultSlot> ();
    public CraftingRule RequireSlot1 { get { return RequireSlot; } set { RequireSlot = value; } }

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
        for (int i = 0; i < RequireSlot.requires.Count; i++)
        {
            GameObject slot = Instantiate(Slot, CraftingSlots.transform);
            DefaultSlot defaultSlot = slot.GetComponent<DefaultSlot>();
            slots.Add (defaultSlot);
            defaultSlot.ItemAccepted = RequireSlot.requires[i];
            defaultSlot.AcceptAll = false;
            defaultSlot.IsHighlighted = true;
            Color color = defaultSlot.Img1.color;
            color = Color.white;
            color.a = 0.25f;
            defaultSlot.Img1.color = color;
            defaultSlot.Img1.sprite = RequireSlot.requires[i].sprite;
            
        }
    }
}