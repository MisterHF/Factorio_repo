using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DefaultSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public ItemData Data;
    public int Count;
    [SerializeField] private TextMeshProUGUI TextCountItem;
    [SerializeField] private Image Img;
    [SerializeField] private bool CanDropped = true;
    [SerializeField] private bool AcceptAll = true;
    [HideInInspector] public ItemData ItemAccepted;

    private void Start()
    {
        TextCountItem.text = Count.ToString();
    }

    private void Update()
    {
        TextCountItem.text = Count.ToString();
        if (Count <= 0)
        {
            ChangeColorAndSprite();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!CanDropped) return;
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if (!AcceptAll)
        {
            if (draggableItem.Datadrag == ItemAccepted)
                DropItem(draggableItem);
        }
        else
        {
            DropItem(draggableItem);
        }
    }

    private void DropItem(DraggableItem _draggableItem)
    {
        if (_draggableItem.Datadrag == null) return;

        if (Data == _draggableItem.Datadrag && transform != _draggableItem.Parent)
        {
            Count += _draggableItem.CountDrag;
            _draggableItem.Parent.GetComponent<DefaultSlot>().Data = null;
            _draggableItem.Parent.GetComponent<DefaultSlot>().Count = 0;
            _draggableItem.Parent.GetComponent<DefaultSlot>().ChangeColorAndSprite();
            ChangeColorAndSprite();
            TextCountItem.text = Count.ToString();
        }
        else if (transform == _draggableItem.Parent)
        {
            return;
        }
        else
        {
            GiveDataToOtherParent(_draggableItem);
            Data = _draggableItem.Datadrag;
            Count = _draggableItem.CountDrag;
            ChangeColorAndSprite();
            TextCountItem.text = Count.ToString();
        }
    }

    private void GiveDataToOtherParent(DraggableItem _dragged)
    {
        _dragged.Parent.GetComponent<DefaultSlot>().Data = Data;
        _dragged.Parent.GetComponent<DefaultSlot>().Count = Count;
    }

    public void ChangeColorAndSprite()
    {
        if (Data == null)
        {
            Img.sprite = null;
            Img.color = Color.clear;
        }
        else
        {
            Img.sprite = Data.sprite;
            Img.color = Color.white;
        }
    }

    public virtual void SetItem(ItemData d, int i)
    {
        Data = d;
        Count = i;
    }

    public virtual void UseItem()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Data != null && Data.Type == ObjectType.Building)
        {
            Building building = (Building)Data;
            EventMaster.TriggerBuildingPrefabSet(building);
        }
    }
}