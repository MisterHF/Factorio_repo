using System;
using UnityEngine;

public class SetGemCrafterRarity : MonoBehaviour
{

    private GemController gemController;
    private Pickeable pickeable;
    
    private void Start()
    {
        gemController = GetComponent<BuildUi>().OpenPrefab.GetComponent<GemController>();
        pickeable = GetComponent<Pickeable>();
        Building building = (Building)pickeable.ScriptableObject;
        gemController.Rarity = building.Rarity;
    }
}
