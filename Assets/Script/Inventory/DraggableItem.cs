using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform parent;

    [SerializeField] private Image Image;

    private int countDrag;
    private ItemData dataDrag;


    public int CountDrag
    {
        get { return countDrag; }
    }

    public ItemData Datadrag
    {
        get { return dataDrag; }
    }

    public Transform Parent
    {
        get { return parent; }
        set { parent = value; }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        if (parent.GetComponent<DefaultSlot>().Data == null) return;
        Debug.Log("start drag");

        dataDrag = parent.GetComponent<DefaultSlot>().Data;
        countDrag = parent.GetComponent<DefaultSlot>().Count;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (parent.GetComponent<DefaultSlot>().Data == null) return;
        Debug.Log("drag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");

        transform.SetParent(parent);
        transform.position = parent.position;

        dataDrag = null;
        countDrag = 0;

        Image.raycastTarget = true;
    }
}