using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<DefaultSlot> items = new List<DefaultSlot>();
    [SerializeField] private GameObject inventoryPanel; // root inventory panel
    const int inventorySize = 9;
    [SerializeField] GameObject content;

    private void Start()
    {
        for (int i = 0; i < content.transform.childCount; i++) 
        {
            items.Add(content.transform.GetChild(i).GetComponent<DefaultSlot>());
        }
        RefreshContent();
    }

    public void AddItem(ItemData item, int count = 1)
{
    var slot = items.Where(x => x.data == item).ToList();

    if (slot.Any())
    {
        slot[0].data = item;
        slot[0].count += count;
        RefreshContent();
    }
    else
    {
        var emptySlot = items.FirstOrDefault(x => x.data == null);
        if (emptySlot != null)
        {
            emptySlot.data = item;
            emptySlot.count = count;
            RefreshContent();
        }
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
                slot[0].SetItem(null, 0);
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
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].data == null) return;

            Image img = items[i].transform.GetChild(0).GetComponent<Image>();

            if (items[i].count >= 1)
            {
                img.sprite = items[i].data.sprite;
                img.color = Color.white;
            }
            else if (items[i].count <= 0)
            {
                img.sprite = null;
                img.color = Color.clear;
                items[i].data = null;
            }
        }
    }

    public bool IsFull()
    {
        return inventorySize == items.Count;
    }
    public void ShowInventory()
    {
        gameObject.SetActive(true);
    }

    public void HideInventory()
    {
        gameObject.SetActive(false);
    }
}
