using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FurnaceController : MonoBehaviour
{
    //Float and Int
    [SerializeField] private float HeatResistance;
    [SerializeField] private float FurnaceSpeed;
    [SerializeField] private float EndTimer;


    [SerializeField] private float timer = 0;

    //Getter
    public float EndTimer1 => EndTimer;

    //Script
    [SerializeField] private FurnaceCraft SelectedCraft;
    private ItemData Result;
    private BuildUi buildUi;

    //Unity Component
    [SerializeField] private Slider TimerSlider;
    [SerializeField] private TMP_Dropdown Dropdown;
    [SerializeField] private DefaultSlot IngredientSlot;
    [SerializeField] private DefaultSlot ResultSlot;

    private void Start()
    {
        TimerSlider.maxValue = EndTimer1;
    }

    private void Update()
    {
        FurnaceHeating();
        GetCraft();
    }

    private void GetCraft()
    {
        SelectedCraft = Dropdown.gameObject.GetComponent<GetValueFromDropDown>().FurnaceCraft;
        if (SelectedCraft != null)
        {
            IngredientSlot.ItemAccepted = SelectedCraft.Item1;
        }
    }

    private void FurnaceHeating()
    {
        if (SelectedCraft == null || IngredientSlot.Count == 0) return;

        if (IngredientSlot.Data == SelectedCraft.Item1)
        {
            if (timer <= EndTimer)
            {
                timer += FurnaceSpeed * Time.deltaTime;
                TimerSlider.value = timer;
            }
            else
            {
                Result = SelectedCraft.Item2;
                ResultSlot.Data = Result;
                ResultSlot.Count += 1;
                IngredientSlot.Count -= 1;
                ResultSlot.transform.GetChild(1).GetComponent<Image>().sprite = Result.sprite;
                ResultSlot.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                Debug.Log("Crafted");
                timer = 0;
                TimerSlider.value = timer;
                if (IngredientSlot.Count <= 0)
                {
                    IngredientSlot.Data = null;
                    // IngredientSlot.Clear();
                }
            }
        }
    }
}