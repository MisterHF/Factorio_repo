using UnityEngine;

[DefaultExecutionOrder(-10)]
public class BuildUi : MonoBehaviour
{
    [SerializeField] private GameObject PanelPrefab;
    private GameObject openPrefab;

    public GameObject PanelPrefab1 => PanelPrefab;
    public GameObject OpenPrefab => openPrefab;

    private void Start()
    {
        openPrefab = Instantiate(PanelPrefab, GetPanel.Instance.transform.position, Quaternion.identity,
            GetPanel.Instance.transform);
        GetPanel.Instance.AddPanelToOpen(openPrefab);
    }

    public void OpenUI()
    {
        openPrefab.transform.GetChild(0).gameObject.SetActive(!openPrefab.transform.GetChild(0).gameObject.activeSelf);
    }

    private void OnDestroy()
    {
        Destroy(openPrefab);
    }
}