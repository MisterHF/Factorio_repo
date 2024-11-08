using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BeltController : MonoBehaviour
{
    [SerializeField] private float BeltSpeed;
    [SerializeField] private ItemData TransportedItem;
    [SerializeField] private GameObject WaypointPrefab;
    private List<Vector3> waypons = new();
    private int index = 0;
    private Vector3 position0;


    private void Start()
    {
        position0 = transform.position;
        waypons.Add(position0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 newWaypointPosition = GetMousePositionInWorld2D();
            if (newWaypointPosition != Vector3.zero)
            {
                GameObject newWaypoint =
                    Instantiate(WaypointPrefab, newWaypointPosition, Quaternion.identity);
                newWaypoint.GetComponent<PassRender>().BeltController = this;
                waypons.Add(newWaypoint.transform.position);
            }
        }

        if (waypons.Count > 1)
        {
            Patrol();
        }
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
        transform.position =
            Vector3.MoveTowards(transform.position, waypons[index], BeltSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypons[index]) <= 0.1)
        {
            if (index >= waypons.Count - 1)
            {
                index = 0;
                waypons.Reverse();
            }
            else
            {
                Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, 2).Where(x => x.gameObject != transform.gameObject).ToArray();
                if (collision[0].GetComponent<BuildUi>())
                {
                    if (collision[0].TryGetComponent<BuildUi>(out BuildUi _p))
                    {
                        // _p.OpenPrefab.GetComponent<_p.>()
                    }
                }
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
        for (int i = 0; i < waypons.Count; i++)
        {
            if (waypons[i] == _pos)
            {
                if (i >= waypons.Count - 1)
                {
                    return waypons[i - 1];
                }
                else
                {
                    return waypons[i + 1];
                }
            }
        }

        return Vector3.zero;
    }
}