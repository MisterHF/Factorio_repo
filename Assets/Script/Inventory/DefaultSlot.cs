using System;
using System.Net.NetworkInformation;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.Properties;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

[Serializable]
public class DefaultSlot : MonoBehaviour, IDropHandler
{
    public ItemData data;
    public int count;
    [SerializeField] private TextMeshProUGUI textCountItem;
    private GameObject child;

    public GameObject Child => child;


    private void Start()
    {
        textCountItem.text = count.ToString();
    }

    private void Update()
    {
        textCountItem.text = count.ToString();
    }

    public void Clear()
    {
        transform.GetChild(1).GetComponent<Image>().color = Color.clear;
        transform.GetChild(1).GetComponent<Image>().sprite = null;
    }
    
    public DefaultSlot(ItemData _data, int _cout)
    {
        data = _data;
        count = _cout;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        GiveDataToOtherParent(draggableItem);

        if (transform.childCount == 2)
        {
            SetNewTransform(draggableItem);
        }

        child = draggableItem.gameObject;
        draggableItem.ParentAfterDrag = transform;
        textCountItem.text = count.ToString();
    }

    private void GiveDataToOtherParent(DraggableItem _dragged)
    {
        _dragged.ParentAfterDrag.GetComponent<DefaultSlot>().data = data;
        _dragged.ParentAfterDrag.GetComponent<DefaultSlot>().count = count;
    }

    private void SetNewTransform(DraggableItem _dragged)
    {
        transform.GetChild(1).transform.position = _dragged.ParentAfterDrag.transform.position;
        transform.GetChild(1).SetParent(_dragged.ParentAfterDrag.transform);
    }


    public virtual void SetItem(ItemData d, int i)
    {
        data = d;
        count = i;
    }

    public virtual void UseItem()
    {
    }
}