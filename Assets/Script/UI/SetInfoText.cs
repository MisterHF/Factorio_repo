using System;
using TMPro;
using UnityEngine;

public class SetInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI TypeText;

    public static SetInfoText SInstance;
    
    private void Awake()
    {
        if (SInstance == null)
        {
            SInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Update()
    {
        // transform.position = Input.mousePosition;
    }

    public void SetTextName(string _name)
    {
        NameText.text = _name;
    }

    public void SetTextType(string _type)
    {
        TypeText.text = _type;
    }
}
