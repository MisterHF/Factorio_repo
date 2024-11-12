using UnityEngine;

public class BuildingHover : MonoBehaviour
{
    private SpriteRenderer haloRenderer;
    private GameObject infoPanel;

    private void Start()
    {
        infoPanel = SetInfoText.SInstance.gameObject;
        haloRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        haloRenderer.enabled = false;
        SetInfoText.SInstance.SetTextName(string.Empty);
        SetInfoText.SInstance.SetTextType(string.Empty);
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePos);

        bool isHovering = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == gameObject)
            {
                haloRenderer.enabled = true;
                infoPanel.transform.GetChild(0).gameObject.SetActive(true);
                Pickeable pickeable = hitCollider.gameObject.GetComponent<Pickeable>();
                if (pickeable != null)
                {
                    SetInfoText.SInstance.SetTextName(pickeable.ScriptableObject.nameItem);
                    SetInfoText.SInstance.SetTextType(gameObject.tag);
                    BeltController.SelectedBelt = GetComponent<BeltController>();
                }
                isHovering = true;
            }
        }

        if (!isHovering)
        {
            haloRenderer.enabled = false;
            infoPanel.transform.GetChild(0).gameObject.SetActive(false);
            SetInfoText.SInstance.SetTextName(string.Empty);
            SetInfoText.SInstance.SetTextType(string.Empty);
        }
    }
}