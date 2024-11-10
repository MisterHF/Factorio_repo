using UnityEngine;

public class OverSelected : MonoBehaviour
{
    private GameObject currentBuilding; 

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

        if (hitCollider != null && hitCollider.CompareTag("Build") && hitCollider.CompareTag("Minable"))
        {
            if (currentBuilding != hitCollider.gameObject)
            {
                ClearCurrentBuilding();
                currentBuilding = hitCollider.gameObject;
                currentBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            ClearCurrentBuilding();
        }
    }

    private void ClearCurrentBuilding()
    {
        if (currentBuilding != null)
        {
            currentBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            currentBuilding = null;
        }
    }
}
