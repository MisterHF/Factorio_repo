using UnityEngine;

public class DefaultSlot
{
    public ItemData data;
    public int count;

    public DefaultSlot(ItemData _data, int _cout)
    {
        data = _data;
        count = _cout;
    }

    public virtual void SetItem(ItemData d) {
        data = d;
    }
    public virtual void UseItem() { }
}
