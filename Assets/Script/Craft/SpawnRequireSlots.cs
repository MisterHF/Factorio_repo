using System.Collections.Generic;
using UnityEngine;

public class SpawnRequireSlots : MonoBehaviour
{
    private GameObject CraftingSlots;
    private CraftingRule RequireSlot;

    [SerializeField] private GameObject Slot;

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
                Destroy(CraftingSlots.transform.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < RequireSlot.requires.Count; i++)
        {
            GameObject slot = Instantiate(Slot, CraftingSlots.transform);
        }
    }
}