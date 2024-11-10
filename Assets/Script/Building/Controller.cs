using UnityEngine;

public class Controller : MonoBehaviour
{
    public virtual ItemData GetItemData()
    {
        return null;
    }

    public virtual int GetItemCount()
    {
        return 0;
    }

    public virtual void SetItemData(ItemData _data)
    {
        
    }
    
    public virtual void SetItemCount(int _count)
    {
        
    }
}