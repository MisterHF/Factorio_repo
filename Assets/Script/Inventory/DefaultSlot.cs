using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class DefaultSlot : MonoBehaviour, IDropHandler
{
    public ItemData data;
    public int count;
    [SerializeField] private TextMeshProUGUI textCountItem;

    private void Start()
    {
        textCountItem.text = count.ToString();  
    }

    public DefaultSlot(ItemData _data, int _cout)
    {
        data = _data;
        count = _cout;
    }

    public void OnDrop(PointerEventData eventData)
    {

        GameObject dropped = eventData.pointerDrag;

        dropped.GetComponent<DraggableItem>().ParentAfterDrag.GetComponent<DefaultSlot>().data = data;
        dropped.GetComponent<DraggableItem>().ParentAfterDrag.GetComponent<DefaultSlot>().count = count;
        transform.GetChild(0).transform.position = dropped.GetComponent<DraggableItem>().ParentAfterDrag.transform.position;
        transform.GetChild(0).transform.SetParent(dropped.GetComponent<DraggableItem>().ParentAfterDrag);
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        draggableItem.ParentAfterDrag = transform;

    }

    public virtual void SetItem(ItemData d)
    {
        data = d;
    }
    public virtual void UseItem() { }
}
