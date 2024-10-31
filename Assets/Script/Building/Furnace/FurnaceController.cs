using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FurnaceController : MonoBehaviour
{
    //Float and Int
    [SerializeField] private float HeatResistance;
    [SerializeField] private float FurnaceSpeed;
    [SerializeField] private float EndTimer;

    [SerializeField] private float timer = 0;

    //Getter
    public float EndTimer1 => EndTimer;
    public float Timer1 => timer;

    //Script
    [SerializeField] private FurnaceCraft SelectedCraft;
    private ItemData Result;

    //Unity Component
    [SerializeField] private TMP_Dropdown Dropdown;
    [SerializeField] private DefaultSlot IngredientSlot;
    [SerializeField] private DefaultSlot ResultSlot;

    private void Start()
    {
        Dropdown.onValueChanged.AddListener(delegate { GetCraft(); });
        GetCraft(); // Initialize selectedCraft with the current dropdown value
    }

    private void Update()
    {
        FurnaceHeating();
    }

    public void GetCraft()
    {
        SelectedCraft = Dropdown.gameObject.GetComponent<GetValueFromDropDown>().ChangeCraftIntoFurnace();
    }
    
    private void FurnaceHeating()
    {
        if (SelectedCraft == null || IngredientSlot.count == 0) return;

        for (int i = 0; i < IngredientSlot.count; i++)
        {
            if (IngredientSlot.data == SelectedCraft.Item1)
            {
                if (timer <= EndTimer)
                {
                    timer += FurnaceSpeed * Time.deltaTime;
                    GetComponent<Build_Ui>().UpdateValueSlider(timer);
                }
                else
                {
                    Result = SelectedCraft.Item2;
                    ResultSlot.data = Result;
                    ResultSlot.count += 1;
                    IngredientSlot.count -= 1;
                    Debug.Log("Crafted");
                    GetComponent<Build_Ui>().UpdateValueSlider(0);
                    timer = 0;
                }

                break;
            }
        }
    }
}

[Serializable]
public struct FurnaceRule
{
    public ScriptableObject ingredient;
    public ScriptableObject result;
}