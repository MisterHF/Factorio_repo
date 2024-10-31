using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class UpdateForCrafting : MonoBehaviour
{
    private DefaultSlot slot;
    private GameObject child;
    
    private void Start()
    {
        slot = GetComponent<DefaultSlot>();
        child = slot.Child;
    }

    private void Update()
    {
        UpdateForCraft();
    }

    public void ClearSlot()
    {
        child.GetComponent<Image>().sprite = null;
        child.GetComponent<Image>().color = Color.clear;
    }

    private void UpdateForCraft()
    {
        child.GetComponent<Image>().sprite = slot.data.sprite;
        child.GetComponent<Image>().color = Color.white;
    }
}
