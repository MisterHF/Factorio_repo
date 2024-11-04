using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    private ItemData data;
    private int count;

    public ItemData Data
    {
        get => data;
        set => data = value;
    }

    public int Count
    {
        get => count;
        set => count = value;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        dropped.GetComponent<DraggableItem>().Parent.GetComponent<DefaultSlot>().Data = data;
        dropped.GetComponent<DraggableItem>().Parent.GetComponent<DefaultSlot>().Count = count;
        transform.GetChild(0).transform.position =
            dropped.GetComponent<DraggableItem>().Parent.transform.position;
        transform.GetChild(0).transform.SetParent(dropped.GetComponent<DraggableItem>().Parent);
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.Parent = transform;
    }
}