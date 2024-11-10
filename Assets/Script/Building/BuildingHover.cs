using UnityEngine;

public class BuildingHover : MonoBehaviour
{
    private SpriteRenderer haloRenderer;
    private GameObject infoPanel;

    private void Start()
    {
        infoPanel = GetPanel.Instance.InfoPanel1;
        haloRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        haloRenderer.enabled = false;
        infoPanel.GetComponent<SetInfoText>().SetTextName(string.Empty);
        infoPanel.GetComponent<SetInfoText>().SetTextType(string.Empty);
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            haloRenderer.enabled = true;
            infoPanel.SetActive(true);
            var pickeable = hitCollider.gameObject.GetComponent<Pickeable>();
            if (pickeable != null)
            {
                infoPanel.GetComponent<SetInfoText>().SetTextName(pickeable.ScriptableObject.nameItem);
                infoPanel.GetComponent<SetInfoText>().SetTextType(gameObject.tag);
            }
        }
        else
        {
            haloRenderer.enabled = false;
            infoPanel.GetComponent<SetInfoText>().SetTextName(string.Empty);
            infoPanel.GetComponent<SetInfoText>().SetTextType(string.Empty);
        }
    }
}