using System;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    [SerializeField] private float beltSpeed;
    [SerializeField] private GameObject waypointPrefab; // Un seul prefab pour tous les waypoints
    [SerializeField] private LayerMask layerMask;

    private List<PathNode> pathNodes = new();
    private HashSet<Vector3> occupiedPositions = new();
    private int currentNodeIndex = 0;
    private bool pathValidated = false;
    private bool isDrawingPath = false;

    public static BeltController SelectedBelt { get; set; }

    public enum PathType
    {
        Fill,
        Empty,
        Follow
    }

    private PathType selectedPathType = PathType.Follow;

    private void Start()
    {
        CreateInitialNode();
    }

    private void CreateInitialNode()
    {
        Vector3 startPosition = transform.position;
        AddNode(startPosition, PathType.Fill);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedPathType = PathType.Fill;
            Debug.Log(selectedPathType);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedPathType = PathType.Empty;
            Debug.Log(selectedPathType);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedPathType = PathType.Follow;
            Debug.Log(selectedPathType);
        }

        if (Input.GetMouseButtonDown(0) && !pathValidated && SelectedBelt == this)
        {
            isDrawingPath = true;
        }

        if (Input.GetMouseButtonUp(0) && !pathValidated)
        {
            isDrawingPath = false;
        }

        if (isDrawingPath && !pathValidated)
        {
            Vector3 newNodePosition = GetMousePositionRounded();
            if (newNodePosition != Vector3.zero && CanAddNode(newNodePosition))
            {
                AddNode(newNodePosition, selectedPathType);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPathClosed())
            {
                pathValidated = true;
                Debug.Log("Chemin validé !");
            }
            else
            {
                Debug.LogWarning("Le chemin doit être une boucle fermée pour être validé !");
            }
        }

        if (Input.GetMouseButtonDown(1) && !pathValidated)
        {
            RemoveNodeAtPosition(GetMousePositionRounded());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateNodeAtPosition(GetMousePositionRounded());
        }

        if (pathValidated && pathNodes.Count > 1)
        {
            MoveAlongPath();
        }
    }

    private Vector3 GetMousePositionRounded()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(
            Mathf.Round(mousePosition.x),
            Mathf.Round(mousePosition.y),
            0
        );
        if (mousePosition.x % 2 != 0)
        {
            mousePosition.x++;
        }
        if (mousePosition.y % 2 != 0)
        {
            mousePosition.y++;
        }

        return mousePosition;
    }

    private void RotateNodeAtPosition(Vector3 position)
    {
        foreach (PathNode node in pathNodes)
        {
            if (node.Position == position)
            {
                node.NodeObject.GetComponent<Waypoint>().RotateRail();
                break;
            }
        }
    }

    private void AddNode(Vector3 position, PathType type)
    {
        if (IsOccupied(position))
        {
            Debug.Log("Cette case ou zone est déjà occupée par un autre waypoint !");
            return;
        }

        GameObject newWaypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        MarkOccupiedArea(position);


        pathNodes.Add(new PathNode(position, type, newWaypoint));
    }

    private bool IsOccupied(Vector3 position)
    {
        for (int x = -1; x <= 0; x += 1)
        {
            for (int y = -1; y <= 0; y += 1)
            {
                Vector3 checkPosition = position + new Vector3(x, y, 0);
                if (occupiedPositions.Contains(checkPosition))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void MarkOccupiedArea(Vector3 position)
    {
        for (int x = -1; x <= 0; x += 1)
        {
            for (int y = -1; y <= 0; y += 1)
            {
                Vector3 checkPosition = position + new Vector3(x, y, 0);
                occupiedPositions.Add(checkPosition);
            }
        }
    }

    private bool CanAddNode(Vector3 position)
    {
        return !IsOccupied(position);
    }

    private void RemoveNodeAtPosition(Vector3 position)
    {
        foreach (var node in pathNodes)
        {
            if (node.Position == position)
            {
                UnmarkOccupiedArea(position);
                Destroy(node.NodeObject);
                pathNodes.Remove(node);
                break;
            }
        }
    }

    private void UnmarkOccupiedArea(Vector3 position)
    {
        for (int x = -1; x <= 0; x += 1)
        {
            for (int y = -1; y <= 0; y += 1)
            {
                Vector3 checkPosition = position + new Vector3(x * 1, y * 1, 0);
                occupiedPositions.Remove(checkPosition);
            }
        }
    }

    private void MoveAlongPath()
    {
        if (pathNodes.Count == 0) return;

        PathNode currentNode = pathNodes[currentNodeIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentNode.Position, beltSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentNode.Position) <= 0.1f)
        {
            if (currentNodeIndex >= pathNodes.Count - 1)
            {
                currentNodeIndex = 0;
            }
            else
            {
                currentNodeIndex++;
            }
        }
    }


    private bool IsPathClosed()
    {
        if (pathNodes.Count < 3) return false;

        Vector3 firstNode = pathNodes[0].Position;
        Vector3 lastNode = pathNodes[pathNodes.Count - 1].Position;

        return Vector3.Distance(firstNode, lastNode) <= 2f;
    }

    public class PathNode
    {
        public Vector3 Position { get; }
        public PathType Type { get; set; }
        public GameObject NodeObject { get; }

        public PathNode(Vector3 position, PathType type, GameObject nodeObject)
        {
            Position = position;
            Type = type;
            NodeObject = nodeObject;
        }
    }

    public int GetNodeIndex(Vector3 position)
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].Position == position)
            {
                return i;
            }
        }

        return -1;
    }

    public PathType GetNodeType(Vector3 position)
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].Position == position)
            {
                return pathNodes[i].Type;
            }
        }

        return PathType.Follow;
    }

    public Vector3 GetNodePosition(int index)
    {
        if (index >= 0 && index < pathNodes.Count)
        {
            return pathNodes[index].Position;
        }

        return Vector3.zero;
    }

    public int GetNodeCount()
    {
        return pathNodes.Count;
    }

    private void OnDestroy()
    {
        foreach (var node in pathNodes)
        {
            Destroy(node.NodeObject);
        }
    }
}