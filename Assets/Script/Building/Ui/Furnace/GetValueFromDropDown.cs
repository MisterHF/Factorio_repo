using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetValueFromDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private List<TMP_Dropdown.OptionData> dropDownOption = new List<TMP_Dropdown.OptionData>();


    public void GetDroopDownValue()
    {
        int _dropDownIndex = dropDown.value;
        string _dropDownText = dropDown.options[_dropDownIndex].text;
        Debug.Log(_dropDownText);
    }

    [ContextMenu("Add New Craft")]
    private void AddNewCraft()
    {
        dropDown.options.Add(new TMP_Dropdown.OptionData("Iron", null, Color.black));

        dropDown.AddOptions(dropDownOption);

        dropDown.RefreshShownValue();
    }

    [ContextMenu("Remove At")]
    private void RemoveCraftAt()
    {
        int index = 0;
        if (dropDown.value == index)
        {
            dropDown.value = 0;
        }

        dropDown.options.RemoveAt(index);
        dropDown.RefreshShownValue();
    }

    [ContextMenu("Remove By Name")]
    private void RemoveCraftName(string _locationName)
    {
        for (int i = 0; i < dropDown.options.Count; i++)
        {
            if (dropDown.options[i].text == _locationName)
            {
                if (dropDown.value == i)
                {
                    dropDown.value = 0;
                }

                dropDown.options.RemoveAt(i);
                break;
            }
        }
    }

    [ContextMenu("AddListener To Craft")]
    private void ActionCraft()
    {
        dropDown.onValueChanged.RemoveListener(ActionToCall);
        dropDown.onValueChanged.AddListener(ActionToCall);
    }

    private void ActionToCall(int arg0)
    {
        Debug.Log(arg0);
    }

    private void OnDestroy()
    {
        dropDown.onValueChanged.RemoveListener(ActionToCall);
    }
}