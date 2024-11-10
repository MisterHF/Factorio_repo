using System;
using System.Linq;
using UnityEngine;

public class VolcanoController : MonoBehaviour
{
    public static VolcanoController Instance { get; private set; }

    [SerializeField] private int VolcanoHeat;
    [SerializeField] private int CurrentVolcanoHeat;
    [SerializeField] private float HeatIncreaseRate = 1f; // Rate at which heat increases

    public int CurrentVolcanoHeat1 => CurrentVolcanoHeat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetInitialVolcanoHeat();
        }
        else
        {
            Destroy(this);
        }
    }

    private void SetInitialVolcanoHeat()
    {
        Pickeable[] buildings = FindObjectsOfType<Pickeable>().Where(x => x.CompareTag("Build")).ToArray();
        foreach (Pickeable building in buildings)
        {
            Building buildingObject = (Building)building.ScriptableObject;
            IncreaseVolcanoHeat(buildingObject.Rarity);
        }
    }

    public void IncreaseVolcanoHeat(BuildingRarity _rarity)
    {
        switch (_rarity)
        {
            case BuildingRarity.Common:
                CurrentVolcanoHeat += Mathf.RoundToInt((int)BuildingRarity.Common);
                break;
            case BuildingRarity.Rare:
                CurrentVolcanoHeat += Mathf.RoundToInt((int)BuildingRarity.Rare);
                break;
            case BuildingRarity.Legendary:
                CurrentVolcanoHeat += Mathf.RoundToInt((int)BuildingRarity.Legendary);
                break;
        }
    }
}