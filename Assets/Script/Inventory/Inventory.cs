using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<DefaultSlot> items = new List<DefaultSlot>();
    [SerializeField] private GameObject inventoryPanel; // root inventory panel
    const int inventorySize = 9;
    [SerializeField] GameObject content;

    public Dictionary<string, string> itemSlot = new();

    [SerializeField] private List<ItemData> data = new();

    private SaveLoadJson saveLoadJson;
    private void Start()
    {
        saveLoadJson = SaveLoadJson.Instance;
        for (int i = 0; i < content.transform.childCount; i++)
        {
            items.Add(content.transform.GetChild(i).GetComponent<DefaultSlot>());
        }
        RefreshContent();
        LoadInventory();
    }

    public void AddItem(ItemData item, int count = 1)
    {
        DefaultSlot slot = items.FirstOrDefault(x => x.Data == item);

        if (slot != null)
        {
            slot.Data = item;
            slot.Count += count;
            RefreshContent();
        }
        else
        {
            DefaultSlot emptySlot = items.FirstOrDefault(x => x.Data == null);
            if (emptySlot != null)
            {
                emptySlot.Data = item;
                emptySlot.Count = count;
                RefreshContent();
            }
        }
        string idString = item.ID.ToString();
        if (!itemSlot.ContainsKey(idString))
        {
            itemSlot.Add(idString, count.ToString());
        }
        else
        {
            if (slot != null)
            {
                itemSlot[idString] = slot.Count.ToString();
            } else
            {
                itemSlot[idString] = "0"; // ?
            }
        }
        saveLoadJson.SaveInventory(itemSlot);
        saveLoadJson.SaveToJson();
        RefreshContent();
    }
    public void RemoveItem(ItemData item, int count)
    {
        var slot = items.Where(x => x.Data == item).ToList();

        if (slot.Any())
        {
            slot[0].Count -= count;

            if (slot[0].Count <= 0)
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
            if (items[i].Data == null) return;

            Image img = items[i].transform.GetChild(1).GetComponent<Image>();

            if (items[i].Count >= 1)
            {
                img.sprite = items[i].Data.sprite;
                img.color = Color.white;
            }
            else if (items[i].Count <= 0)
            {
                img.sprite = null;
                img.color = Color.clear;
                items[i].Data = null;
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

    public bool ContentItem(ItemData data, int count)
    {
        foreach (DefaultSlot item in items)
        {
            if (item.Data == data && item.Count >= count) return true;
        }
        return false;
    }

    private void LoadInventory()
    {
        if (saveLoadJson != null)
        {
            saveLoadJson.LoadGame();
            foreach (InventoryItem item in saveLoadJson._gameData.inventoryItems)
            {
                ItemData itemData = data.Where(data => data.ID == item.id).FirstOrDefault();
                Assert.IsNotNull(itemData);
                AddItem(itemData, item.count);
            }
            /*itemSlot = saveLoadJson.items;
            ItemData newData = null;
            foreach (var item in itemSlot)
            {
                print(item);
                if(item.Key != "") {
                    switch (item.Key)
                    {
                        case "1":
                            newData = data[0];
                            break;
                        case "2":
                            newData = data[1];
                            break;
                        case "3":
                            newData = data[2];
                            break;
                        case "4":
                            newData = data[3];
                            break;
                        default:
                            Debug.LogError("No data");
                            break;

                    }
                    Assert.IsNotNull(newData);
                    AddItem(newData, int.Parse(item.Value));
                }


        }*/

        }
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Add Item"))
        {
            AddItem(data[0]);
        }
    }
}
