using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetPanel : MonoBehaviour
{
    public static GetPanel Instance { get; private set; }

    private List<GameObject> panelList = new List<GameObject>();
    [SerializeField] private GameObject InfoPanel;

    public GameObject InfoPanel1 => InfoPanel;

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

    public void AddPanelToOpen(GameObject _panel)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (_panel == panelList[i])
            {
                return;
            }
        }
        panelList.Add(_panel);
    }

    public GameObject GetPanelByObject(GameObject _g)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i] == _g)
            {
                return panelList[i];
            }
        }

        return null;
    }
}