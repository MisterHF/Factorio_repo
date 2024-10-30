using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    //List 
    [SerializeField] private List<ItemData> Ingredients = new List<ItemData>();
    [SerializeField] private List<ItemData> Outputs = new List<ItemData>();

    //Script
    [SerializeField] private FurnaceCraft SelectedCraft;
    [SerializeField] private ItemData Ingredient;
    [SerializeField] private ItemData Result;

    //Unity Component
    [SerializeField] private TMP_Dropdown Dropdown;

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
        if (SelectedCraft == null || Ingredients.Count == 0) return;

        for (int i = 0; i < Ingredients.Count; i++)
        {
            if (Ingredients[i] == SelectedCraft.Item1)
            {
                if (timer <= EndTimer)
                {
                    timer += FurnaceSpeed * Time.deltaTime;
                    GetComponent<Build_Ui>().UpdateValueSlider(timer);
                }
                else
                {
                    Result = SelectedCraft.Item2;
                    Outputs.Add(Result);
                    Ingredients.RemoveAt(i);
                    Debug.Log("Crafted");
                    GetComponent<Build_Ui>().UpdateValueSlider(0);
                    timer = 0;
                }

                break; // Exit the loop after processing the first matching ingredient
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