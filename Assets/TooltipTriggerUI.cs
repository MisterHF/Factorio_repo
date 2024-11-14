using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipTriggerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string Header;
    [SerializeField] private string Content;
    private DefaultSlot slot;
    private Coroutine showTooltipCoroutine;

    private void Start()
    {
        slot = GetComponent<DefaultSlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slot.Data != null)
        {
            Header = slot.Data.nameItem;
            Content = slot.Data.nameItem;
            showTooltipCoroutine = StartCoroutine(ShowTooltipWithDelay());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (showTooltipCoroutine != null)
        {
            StopCoroutine(showTooltipCoroutine);
        }
        TooltipSystem.Hide();
    }

    private IEnumerator ShowTooltipWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(Content, Header);
    }

    private void Update()
    {
        if (slot.Data == null)
        {
            Header = string.Empty;
            Content = string.Empty;
        }
    }
}