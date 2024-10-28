using System;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : MonoBehaviour
{
    [SerializeField] private float heatResistance;
    [SerializeField] private float furnaceSpeed;
    [SerializeField] private List<FurnaceRule> furnaceRule;
    [SerializeField] private float timer;
    [SerializeField] private FurnaceCraft selectedCraft;
    [SerializeField] private ScriptableObject ingredient;
    [SerializeField] private ScriptableObject result;

    private void Update()
    {
        FurnaceHeating();
    }

    private void FurnaceHeating()
    {
        timer -= furnaceSpeed * Time.deltaTime;
        if (timer <= 0)
        {
            foreach (FurnaceRule _furnaceRule in furnaceRule)
            {
                if (_furnaceRule.ingredient == ingredient)
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