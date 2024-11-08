using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnacePanelInfo : MonoBehaviour
{
    [SerializeField] private DefaultSlot EntrySlot;
    [SerializeField] private DefaultSlot ResultSlot;
    [SerializeField] private Slider Slider;
    [SerializeField] private TMP_Dropdown DropDown;

    public TMP_Dropdown DropDown1 => DropDown;

    public DefaultSlot EntrySlot1 => EntrySlot;

    public DefaultSlot ResultSlot1 => ResultSlot;

    public Slider Slider1 => Slider;
}
