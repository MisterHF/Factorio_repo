using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<DefaultSlot> items = new();
    [SerializeField] private GameObject inventoryPanel; // root inventory panel
    [SerializeField] private GameObject inventorySlots; // root inventory slot
    const int inventorySize = 9;

    private void Start()
    {
        RefreshContent();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

    }
    public void AddItem(ItemData item, int count = 1)
    {
        var slot = items.Where(x => x.data == item).ToList();

        if (slot.Any())
        {
            slot[0].count += count;
        }
        else
        {
            if (items.Count < inventorySize)
                items.Add(new(item, count));
        }

        RefreshContent();
    }
    public void RemoveItem(ItemData item, int count)
    {
        var slot = items.Where(x => x.data == item).ToList();

        if (slot.Any())
        {
            slot[0].count -= count;

            if (slot[0].count <= 0)
            {
                items.Remove(slot[0]);
                slot[0].SetItem(null);
            }
        }

        RefreshContent();
    }

    //Fonction optionnelle
    //Utilisation de cette fonction pour forcer la fermeture de l inventaire si besoin suivant une action.
    public void CloseInventory()
    {
      inventoryPanel?.SetActive(false);

    }

    public void RefreshContent()
    {
        for (int i = 0; i < inventorySlots.transform.childCount; i++)
        {
            Image img = inventorySlots.transform.GetChild(i).GetComponent<Image>();
            
            if (items.Count >= i)
            {
                img.sprite = items[i].data.sprite;
            }
        }
    }

    public bool IsFull()
    {
        return inventorySize == items.Count;
    }
}
