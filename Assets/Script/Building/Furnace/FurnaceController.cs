using System;
using TMPro;
using UnityEngine;
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
                GetComponent<Build_Ui>().UpdateValueSlider(timer);
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
                GetComponent<Build_Ui>().UpdateValueSlider(0);
                timer = 0;
                if (IngredientSlot.Count <= 0)
                {
                    IngredientSlot.Data = null;
                    // IngredientSlot.Clear();
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