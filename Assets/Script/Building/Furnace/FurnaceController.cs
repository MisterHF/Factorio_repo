using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceController : MonoBehaviour
{
    [SerializeField] private float heatResistance;
    [SerializeField] private float furnaceSpeed;
    [SerializeField] private List<FurnaceRule> furnaceRule;
    [SerializeField] private float timer;
    [SerializeField] private FurnaceCraft selectedCraft;
    [SerializeField] private ScriptableObject ingredient;
    [SerializeField] private ScriptableObject result;
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
        selectedCraft = Dropdown.gameObject.GetComponent<GetValueFromDropDown>().FurnaceCraft;
    }

    private void FurnaceHeating()
    {
        timer -= furnaceSpeed * Time.deltaTime;
        if (timer <= 0)
        {
            foreach (FurnaceRule _furnaceRule in furnaceRule)
            {
                if (_furnaceRule.ingredient == ingredient && selectedCraft != null)
                {
                    result = _furnaceRule.result;
                }
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