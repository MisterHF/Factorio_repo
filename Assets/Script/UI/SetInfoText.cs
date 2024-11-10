using TMPro;
using UnityEngine;

public class SetInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI TypeText;

    public void SetTextName(string _name)
    {
        NameText.text = _name;
    }

    public void SetTextType(string _type)
    {
        TypeText.text = _type;
    }
}
