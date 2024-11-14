using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GetValueFromDropDownGemCrafter : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown DropDown;
    [SerializeField] private List<TMP_Dropdown.OptionData> DropDownOption = new List<TMP_Dropdown.OptionData>();

    [SerializeField] private List<FurnaceCraft> FurnaceCrafts = new List<FurnaceCraft>();

    private FurnaceCraft furnaceCraft;

    public FurnaceCraft FurnaceCraft => furnaceCraft;

    private int dropDownIndex;

    private void Start()
    {
        DropDown.onValueChanged.AddListener(ActionToCall);
    }


    public void GetDroopDownValue()
    {
        dropDownIndex = DropDown.value;
        string dropDownText = DropDown.options[dropDownIndex].text;
        Debug.Log(dropDownText);
    }

    [ContextMenu("Add New Craft")]
    private void AddNewCraft()
    {
        DropDown.options.Clear();
        DropDownOption.Clear();

        foreach (var craft in FurnaceCrafts)
        {
            DropDownOption.Add(new TMP_Dropdown.OptionData(craft.Name, craft?.Item2Sprite, Color.white));
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

    private void ActionToCall(int arg0)
    {
        dropDownIndex = DropDown.value;
        furnaceCraft = FurnaceCrafts[dropDownIndex];
        Debug.Log(furnaceCraft);
    }

    private void OnDestroy()
    {
        DropDown.onValueChanged.RemoveListener(ActionToCall);
    }
}