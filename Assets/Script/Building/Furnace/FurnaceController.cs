using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceController : Controller
{
    //Float and Int
    [Header("Float and Int")] [SerializeField]
    private int HeatResistance;

    private float adjustedFurnaceSpeed;

    [SerializeField] private float FurnaceSpeed;
    [SerializeField] private float EndTimer;


    [SerializeField] private float timer = 0;

    //Getter
    public float EndTimer1 => EndTimer;

    //Script
    [Header("Script")] [SerializeField] private FurnaceCraft SelectedCraft;
    private ItemData Result;
    private BuildUi buildUi;

    //Unity Component
    [Header("Unity Component")] [SerializeField]
    private Slider TimerSlider;

    [SerializeField] private TMP_Dropdown Dropdown;
    [SerializeField] private DefaultSlot IngredientSlot;
    [SerializeField] private DefaultSlot ResultSlot;
    [SerializeField] private TextMeshProUGUI HeatResistanceText;
    [SerializeField] private TextMeshProUGUI HeatSpeedText;

    private void Start()
    {
        TimerSlider.maxValue = EndTimer1;
        HeatResistanceText.text = HeatResistance.ToString();
        adjustedFurnaceSpeed = FurnaceSpeed * (1 + VolcanoController.Instance.CurrentVolcanoHeat1 / 100f);
        HeatSpeedText.text = Mathf.Round(adjustedFurnaceSpeed).ToString();
    }

    private void Update()
    {
        FurnaceHeating();
        GetCraft();
    }

    public override ItemData GetItemData()
    {
        if (ResultSlot.Count <= 0)
        {
            ResultSlot.Data = null;
        }
        else
        {
            ResultSlot.Count--;
        }

        return ResultSlot.Data;
    }

    public override int GetItemCount()
    {
        int count = ResultSlot.Count;
        ResultSlot.Count = 0;
        return count;
    }

    public override void SetItemCountForMultiSlot(int _count, ItemData _data)
    {
        IngredientSlot.Data = _data;
        IngredientSlot.Count += _count;
    }

    private void GetCraft()
    {
        SelectedCraft = Dropdown.gameObject.GetComponent<GetValueFromDropDown>().FurnaceCraft;
        if (SelectedCraft != null)
        {
            IngredientSlot.ItemAccepted = SelectedCraft.Item1;
        }
    }
    
    public override bool HasCraftSelected()
    {
        if (SelectedCraft != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   private void FurnaceHeating()
{
    if (SelectedCraft == null || IngredientSlot.Count == 0) return;

    if (VolcanoController.Instance.CurrentVolcanoHeat1 < SelectedCraft.RequiresHeat) return;

    if (VolcanoController.Instance.CurrentVolcanoHeat1 > HeatResistance)
    {
        float failChance = UnityEngine.Random.Range(0f, 1f);
        if (failChance < 0.5f)
        {
            Debug.Log("Crafting failed due to high temperature.");
            return;
        }
    }

    if (IngredientSlot.Data == SelectedCraft.Item1)
    {
        if (timer <= EndTimer)
        {
            adjustedFurnaceSpeed = FurnaceSpeed * (1 + VolcanoController.Instance.CurrentVolcanoHeat1 / 100f);
            HeatSpeedText.text = Mathf.Round(adjustedFurnaceSpeed).ToString();
            timer += adjustedFurnaceSpeed * Time.deltaTime;
            TimerSlider.value = timer;
        }
        else
        {
            Result = SelectedCraft.Item2;
            ResultSlot.Data = Result;
            ResultSlot.Count += 1;
            IngredientSlot.Count -= 1;
            if (Result != null)
            {
                ResultSlot.transform.GetChild(1).GetComponent<Image>().sprite = Result.sprite;
                ResultSlot.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }

            Debug.Log("Crafted");
            timer = 0;
            TimerSlider.value = timer;
            if (IngredientSlot.Count <= 0)
            {
                IngredientSlot.Data = null;
            }
        }
    }
}
}