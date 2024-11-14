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
        AddNode(startPosition, PathType.Follow);
    }

    private void Update()
    {
        // Changer le type de node avec les touches 1, 2, ou 3
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedPathType = PathType.Fill;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectedPathType = PathType.Empty;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectedPathType = PathType.Follow;

        // Commencer à dessiner le chemin
        if (Input.GetMouseButtonDown(0) && !pathValidated && SelectedBelt == this)
        {
            isDrawingPath = true;
        }

        // Terminer le dessin du chemin
        if (Input.GetMouseButtonUp(0) && !pathValidated)
        {
            isDrawingPath = false;
        }

        // Ajouter un waypoint lors du clic gauche
        if (isDrawingPath && !pathValidated)
        {
            Vector3 newNodePosition = GetMousePositionRounded();
            if (newNodePosition != Vector3.zero && CanAddNode(newNodePosition))
            {
                AddNode(newNodePosition, selectedPathType);
            }
        }

        // Validation du chemin
        if (Input.GetKeyDown(KeyCode.E) && pathNodes.Count > 1 && !pathValidated)
        {
            pathValidated = true;
            Debug.Log("Chemin validé !");
        }

        // Déplacement le long du chemin
        if (pathValidated && pathNodes.Count > 1)
        {
            MoveAlongPath();
        }

        // Suppression d'un nœud avec clic droit
        if (Input.GetMouseButtonDown(1))
        {
            RemoveNodeAtPosition(GetMousePositionRounded());
        }

        // Rotation du rail avec R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateNodeAtPosition(GetMousePositionRounded());
        }
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
        // Vérifier si la position et la zone 2x2 sont déjà occupées
        if (IsOccupied(position))
        {
            Debug.Log("Cette case ou zone est déjà occupée par un autre waypoint !");
            return;
        }

        // Instancier un seul prefab de waypoint
        GameObject newWaypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
        
        // Ajouter la position à la liste des positions occupées
        MarkOccupiedArea(position);

        Waypoint waypointScript = newWaypoint.GetComponent<Waypoint>();
        waypointScript.SetPathType(type);
        waypointScript.SetOrientation(Vector3.zero, position, type);

        pathNodes.Add(new PathNode(position, type, newWaypoint));
    }

    // Vérifier si la zone de 2x2 est occupée
    private bool IsOccupied(Vector3 position)
    {
        for (int x = -1; x <= 0; x += 1)
        {
            for (int y = -1; y <= 0; y += 1)
            {
                Vector3 checkPosition = position + new Vector3(x * 1, y * 1, 0);
                if (occupiedPositions.Contains(checkPosition))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Marquer la zone 2x2 comme occupée
    private void MarkOccupiedArea(Vector3 position)
    {
        for (int x = -1; x <= 0; x += 1)
        {
            for (int y = -1; y <= 0; y += 1)
            {
                Vector3 checkPosition = position + new Vector3(x * 1, y * 1, 0);
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
                // Marquer la zone 2x2 comme libérée
                UnmarkOccupiedArea(position);

                occupiedPositions.Remove(position);
                Destroy(node.NodeObject);
                pathNodes.Remove(node);
                break;
            }
        }
    }

    // Marquer la zone 2x2 comme libérée
    private void UnmarkOccupiedArea(Vector3 position)
    {
        for (int x = -1; x <= 1; x += 2)
        {
            for (int y = -1; y <= 1; y += 2)
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

    private Vector3 GetMousePositionRounded()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Assurer que la position est en 2D
        return new Vector3(
            Mathf.Round(mousePosition.x),
            Mathf.Round(mousePosition.y),
            0
        );
    }


    private class PathNode
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

    private void OnDestroy()
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            Destroy(pathNodes[i].NodeObject);
        }
    }
}
