using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetPanel : MonoBehaviour
{
    public static GetPanel Instance { get; private set; }

    private List<GameObject> panelList = new List<GameObject>();

    public List<GameObject> PanelList => panelList;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        for (int i = 0; i < transform.childCount; ++i)
        {
            panelList.Add(transform.GetChild(i).gameObject);
        }
    }
    
    public GameObject GetPanelByName(string name)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].name == name)
            {
                return panelList[i];
            }
        }
        return null;
    }
}
