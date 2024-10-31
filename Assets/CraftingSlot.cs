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
        dropped.GetComponent<DraggableItem>().ParentAfterDrag.GetComponent<DefaultSlot>().data = data;
        dropped.GetComponent<DraggableItem>().ParentAfterDrag.GetComponent<DefaultSlot>().count = count;
        transform.GetChild(0).transform.position =
            dropped.GetComponent<DraggableItem>().ParentAfterDrag.transform.position;
        transform.GetChild(0).transform.SetParent(dropped.GetComponent<DraggableItem>().ParentAfterDrag);
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.ParentAfterDrag = transform;
    }
}