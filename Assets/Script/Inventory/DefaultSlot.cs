using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

[Serializable]
public class DefaultSlot : MonoBehaviour, IDropHandler
{
    public ItemData Data;
    public int Count;
    [SerializeField] private TextMeshProUGUI TextCountItem;
    [SerializeField] private Image Img ;

    public Image Img1 { get { return Img; } set { Img = value; } }

    [SerializeField] private bool CanDropped = true;
    public bool AcceptAll = true;
    [SerializeField] private bool IsHighlight = false;

    public bool IsHighlighted { get { return IsHighlight; } set { IsHighlight = value; } }
    [HideInInspector] public ItemData ItemAccepted;

    private void Start()
    {
        TextCountItem.text = Count.ToString();
    }

    private void Update()
    {
        TextCountItem.text = Count.ToString();
        if (Count <= 0 && !IsHighlight)
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
                DropItem();
        }
        else
        {
            DropItem();
        }


        void DropItem()
        {
            if (draggableItem.Datadrag == null) return;

            if (Data == draggableItem.Datadrag)
            {
                Count += draggableItem.CountDrag;
                draggableItem.Parent.GetComponent<DefaultSlot>().Data = null;
                draggableItem.Parent.GetComponent<DefaultSlot>().Count = 0;
                draggableItem.Parent.GetComponent<DefaultSlot>().ChangeColorAndSprite();
                ChangeColorAndSprite();
                TextCountItem.text = Count.ToString();
            }
            else
            {
                GiveDataToOtherParent(draggableItem);
                Data = draggableItem.Datadrag;
                Count = draggableItem.CountDrag;
                ChangeColorAndSprite();
                TextCountItem.text = Count.ToString();
            }
            
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
}