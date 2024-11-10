using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    [SerializeField] private float BeltSpeed;
    [SerializeField] private ItemData TransportedItem;
    [SerializeField] private int CountItem;
    [SerializeField] private LayerMask Layer;
    [SerializeField] private GameObject WaypointPrefab;
    private List<Vector3> waypons = new();
    private int index = 0;
    private Vector3 position0;
    private Collider2D[] results = new Collider2D[10];

    private void Start()
    {
        position0 = transform.position;
        waypons.Add(position0);
        RetrieveDataFromNearbyMachine();
    }

    private void RetrieveDataFromNearbyMachine()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, 2, results);
        for (int i = 0; i < count; i++)
        {
            if (results[i].gameObject != gameObject)
            {
                Controller controller = results[i].GetComponent<Controller>();
                if (controller != null)
                {
                    TransportedItem = controller.GetItemData();
                    Debug.Log("Data retrieved from nearby machine: " + TransportedItem + " " + CountItem);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 newWaypointPosition = GetMousePositionInWorld2D();
            if (newWaypointPosition != Vector3.zero)
            {
                GameObject newWaypoint = Instantiate(WaypointPrefab, newWaypointPosition, Quaternion.identity);
                newWaypoint.GetComponent<PassRender>().BeltController = this;
                waypons.Add(newWaypoint.transform.position);
                InsertWaypointInOrder(newWaypoint.transform.position);
            }
        }

        if (waypons.Count > 1)
        {
            Patrol();
        }
    }

    private void InsertWaypointInOrder(Vector3 newWaypointPosition)
    {
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        for (int i = 0; i < waypons.Count; i++)
        {
            float distance = Vector3.Distance(newWaypointPosition, waypons[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        waypons.Insert(closestIndex + 1, newWaypointPosition);
    }

    Vector3 GetMousePositionInWorld2D()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Round(mousePosition.x);
        mousePosition.y = Mathf.Round(mousePosition.y);
        mousePosition.z = 0;
        return mousePosition;
    }

    private void Patrol()
{
    transform.position = Vector3.MoveTowards(transform.position, waypons[index], BeltSpeed * Time.deltaTime);
    if (Vector3.Distance(transform.position, waypons[index]) <= 0.1)
    {
        if (index == 0)
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, 2, results);
            for (int i = 0; i < count; i++)
            {
                if (results[i].gameObject != gameObject)
                {
                    Controller controller = results[i].GetComponent<Controller>();
                    if (controller != null)
                    {
                        Debug.Log(controller);
                        if (TransportedItem == null)
                        {
                            TransportedItem = controller.GetItemData();
                        }

                        CountItem += controller.GetItemCount();
                        Debug.Log(TransportedItem + " " + CountItem);
                    }
                }
            }
        }
        else if (index == waypons.Count - 1)
        {
            Collider2D[] count = Physics2D.OverlapCircleAll(transform.position, 2, Layer)
                .Where(collider => collider.gameObject != gameObject)
                .ToArray();
            if (count.Length > 0)
            {
                BuildUi controller = count[0].GetComponent<BuildUi>();
                if (controller != null)
                {
                    Debug.Log(controller);
                    if (TransportedItem != null)
                    {
                        controller.OpenPrefab.GetComponent<Controller>().SetItemData(TransportedItem);
                        controller.OpenPrefab.GetComponent<Controller>().SetItemCount(CountItem);
                        TransportedItem = null;
                        CountItem = 0;
                    }
                }
            }
        }

        if (index >= waypons.Count - 1)
        {
            index = 0;
            waypons.Reverse();
        }
        else
        {
            index++;
        }
    }
}

    void OnDrawGizmos()
    {
        if (waypons == null || waypons.Count == 0)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < waypons.Count - 1; i++)
        {
            Gizmos.DrawLine(waypons[i], waypons[i + 1]);
        }

        foreach (Vector3 waypoint in waypons)
        {
            Gizmos.DrawSphere(waypoint, 0.5f);
        }
    }

    public Vector3 SetSecondPosition(Vector3 _pos)
    {
        float closestDistance = float.MaxValue;
        Vector3 closestWaypoint = Vector3.zero;

        foreach (var waypoint in waypons)
        {
            float distance = Vector3.Distance(_pos, waypoint);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }
}