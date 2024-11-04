using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel; //UI
    [SerializeField] Transform content; // Ressource entrée

    [SerializeField] private List<CraftingRule> craft;
    [SerializeField] private GameObject buttonPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < craft.Count; i++) 
        {
            GameObject btn =  Instantiate(buttonPrefab, content);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = craft[i].result.name;
            btn.transform.GetChild(1).GetComponent<Image>().sprite = craft[i].result.sprite;
            btn.GetComponent<SpawnRequireSlots>().RequireSlot1 = craft[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
