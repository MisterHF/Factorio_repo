using System;
using UnityEngine;

public class BuildingHover : MonoBehaviour
{
    private SpriteRenderer haloRenderer;
    private GameObject infoPanel;
    private Collider2D hitColliders;

    private void Start()
    {
        haloRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        haloRenderer.enabled = false;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hitColliders = Physics2D.OverlapPoint(mousePos);

        bool isHovering = false;
        if(hitColliders != null)
        {
            if (hitColliders.gameObject == gameObject)
            {
                haloRenderer.enabled = true;
                Pickeable pickeable = hitColliders.gameObject.GetComponent<Pickeable>();
                if (pickeable != null)
                {
                    BeltController.SelectedBelt = GetComponent<BeltController>();
                    hitColliders = null;
                }

                isHovering = true;
            }
        }


        if (!isHovering)
        {
            haloRenderer.enabled = false;
        }
    }
}