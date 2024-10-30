using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform parentAfterDrag;

    [SerializeField] private Image image;

    private int countDrag;
    private ItemData dataDrag;

    public int CountDrag {  set { countDrag = value; }}
    public ItemData Datadrag {  set { dataDrag = value; } }
    public Transform ParentAfterDrag {  get { return parentAfterDrag; } set { parentAfterDrag = value; } }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("start drag");
        parentAfterDrag = transform.parent;
        dataDrag = parentAfterDrag.GetComponent<DefaultSlot>().data;
        countDrag = parentAfterDrag.GetComponent<DefaultSlot>().count;
        parentAfterDrag.GetComponent<DefaultSlot>().count = 0;
        parentAfterDrag.GetComponent<DefaultSlot>().data = null;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        transform.SetParent(parentAfterDrag);  
        transform.position = parentAfterDrag.position;
        parentAfterDrag.GetComponent<DefaultSlot>().data = dataDrag;
        parentAfterDrag.GetComponent<DefaultSlot>().count = countDrag;
        image.raycastTarget = true;

    }
}
