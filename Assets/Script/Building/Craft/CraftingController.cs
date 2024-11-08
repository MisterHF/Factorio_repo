using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] Transform content; // Ressource entrée

    [SerializeField] private List<CraftingRule> craft;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private List<GameObject> allButtonsInContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCraftPossibility();
    }

    public void UpdateCraftPossibility()
    {
        DestroyAllButtons();
        for (int i = 0; i < craft.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = craft[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite = craft[i].result.sprite;
            btn.GetComponent<SpawnRequireSlots>().RequireSlot1 = craft[i];

            allButtonsInContent.Add(btn);
        }
    }

    public void AddCraft(CraftingRule Newcraft)
    {
        if (craft.Contains(Newcraft)) {  return; }

        craft.Add(Newcraft);
        UpdateCraftPossibility();
    }

    private void DestroyAllButtons()
    {
        for (int i = 0; i < allButtonsInContent.Count; i++) 
        {
            Destroy(allButtonsInContent[i]);
        }
    }
}
