using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Character_Build : MonoBehaviour
{
    [SerializeField] private LayerMask PlacementLayer;
    [SerializeField] private Color ValidPlacementColor = new Color(0, 1, 0, 0.5f);
    [SerializeField] private Color InvalidPlacementColor = new Color(1, 0, 0, 0.5f);
    [SerializeField] private GameObject Canva;
    [SerializeField] private Inventory Inventory;

    private Building buildingPrefab;
    private GameObject previewObject;
    private Camera mainCamera;
    private bool canPlace;

    private void Awake()
    {
        mainCamera = Camera.main;
        EventMaster.OnBuildingPrefabSet += SetBuildingPrefab;
    }

    private void OnDestroy()
    {
        EventMaster.OnBuildingPrefabSet -= SetBuildingPrefab;
    }

    private void Update()
    {
        if (buildingPrefab != null)
        {
            HandlePreview();
            if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
            {
                PlaceObject();
            }
        }
    }

    private void SetBuildingPrefab(Building prefab)
    {
        buildingPrefab = prefab;
        PreparePreview();
    }

    private void HandlePreview()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (previewObject.activeSelf) previewObject.SetActive(false);
            return;
        }

        if (!previewObject.activeSelf) previewObject.SetActive(true);

        Vector3 mousePosition = GetMousePositionInWorld2D();
        previewObject.transform.position = mousePosition;

        canPlace = CheckPlacementValidity(mousePosition);
        ChangePreviewColor(canPlace);
    }

    private Vector3 GetMousePositionInWorld2D()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Round(mousePosition.x);
        mousePosition.y = Mathf.Round(mousePosition.y);
        mousePosition.z = 0;
        return mousePosition;
    }

    private bool CheckPlacementValidity(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f, PlacementLayer);
        return colliders.Length == 0;
    }

    private void ChangePreviewColor(bool isValid)
    {
        SpriteRenderer renderer = previewObject.GetComponent<SpriteRenderer>();
        renderer.color = isValid ? ValidPlacementColor : InvalidPlacementColor;
    }

    private void PlaceObject()
    {
        Inventory.RemoveItem(buildingPrefab, 1);
        Instantiate(buildingPrefab.prefab, previewObject.transform.position, Quaternion.identity);
        Destroy(previewObject);
        buildingPrefab = null;
        previewObject = null;
    }

    private void PreparePreview()
    {
        if (previewObject) Destroy(previewObject);

        previewObject = Instantiate(buildingPrefab.prefab);
        previewObject.SetActive(false);
        SpriteRenderer renderer = previewObject.GetComponent<SpriteRenderer>();
        renderer.color = InvalidPlacementColor;
    }
}