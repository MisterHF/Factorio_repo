using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool CanAcceptItem;
    
    public virtual ItemData GetItemData()
    {
        return null;
    }

    public virtual int GetItemCount()
    {
        return 0;
    }
    
    public virtual void SetItemCountForMultiSlot(int _count, ItemData _data)
    {
        
    }

    public virtual bool HasCraftSelected()
    {
        return false;
    }
}