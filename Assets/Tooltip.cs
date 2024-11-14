using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HeaderField;
    [SerializeField] private TextMeshProUGUI ContentField;
    [SerializeField] private LayoutElement LayoutElement;
    [SerializeField] private int CharacterWrapLimit;
    [SerializeField] private RectTransform RTransform;


    public void SetText(string _content, string _header = "")
    {
        if (string.IsNullOrEmpty(_header))
        {
            HeaderField.gameObject.SetActive(false);
        }
        else
        {
            HeaderField.gameObject.SetActive(true);
            HeaderField.text = _header;
        }

        ContentField.text = _content;

        int headerLength = HeaderField.text.Length;
        int contentLength = ContentField.text.Length;

        LayoutElement.enabled =
            (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
    }


    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = HeaderField.text.Length;
            int contentLength = ContentField.text.Length;

            LayoutElement.enabled =
                (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        if (pivotX > 0.8f) pivotX = 1;
        else if (pivotX < 0.2f) pivotX = 0;

        if (pivotY > 0.8f) pivotY = 1;
        else if (pivotY < 0.2f) pivotY = 0;

        RTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}