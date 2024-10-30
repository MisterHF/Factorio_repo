using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GetValueFromDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown DropDown;
    [SerializeField] private List<TMP_Dropdown.OptionData> DropDownOption = new List<TMP_Dropdown.OptionData>();

    [SerializeField] private List<FurnaceCraft> FurnaceCrafts = new List<FurnaceCraft>();

    private FurnaceCraft furnaceCraft;

    public FurnaceCraft FurnaceCraft => furnaceCraft;

    public void GetDroopDownValue()
    {
        int _dropDownIndex = DropDown.value;
        string _dropDownText = DropDown.options[_dropDownIndex].text;
        Debug.Log(_dropDownText);
    }

    [ContextMenu("Add New Craft")]
    private void AddNewCraft()
    {
        DropDown.options.Clear();
        DropDownOption.Clear();

        foreach (var craft in FurnaceCrafts)
        {
            DropDownOption.Add(new TMP_Dropdown.OptionData(craft.Name, craft.Item2Sprite, Color.white));
        }

        DropDown.AddOptions(DropDownOption);
        DropDown.RefreshShownValue();
    }

    [ContextMenu("Remove At")]
    private void RemoveCraftAt()
    {
        int index = 0;
        if (DropDown.value == index)
        {
            DropDown.value = 0;
        }

        DropDown.options.RemoveAt(index);
        DropDown.RefreshShownValue();
    }

    [ContextMenu("AddListener To Craft")]
    private void ActionCraft()
    {
        DropDown.onValueChanged.RemoveListener(ActionToCall);
        DropDown.onValueChanged.AddListener(ActionToCall);
    }

    private void ActionToCall(int arg0)
    {
        for (int i = 0; i < FurnaceCrafts.Count; i++)
        {
            if (FurnaceCrafts[i].name == FurnaceCrafts[arg0].name)
            {
                furnaceCraft = FurnaceCrafts[i];
            }
        }
    }

    private void OnDestroy()
    {
        DropDown.onValueChanged.RemoveListener(ActionToCall);
    }
}