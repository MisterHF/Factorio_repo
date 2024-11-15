using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Sprites pour chaque type de rail")] [SerializeField]
    private Sprite fillVerticalSprite; // Sprite vertical pour le rail de type Fill

    [SerializeField] private Sprite fillHorizontalSprite; // Sprite horizontal pour le rail de type Fill
    [SerializeField] private Sprite fillCornerSprite; // Sprite de virage pour le rail de type Fill

    [SerializeField] private Sprite followVerticalSprite; // Sprite vertical pour le rail de type Follow
    [SerializeField] private Sprite followHorizontalSprite; // Sprite horizontal pour le rail de type Follow
    [SerializeField] private Sprite followCornerSprite; // Sprite de virage pour le rail de type Follow

    [SerializeField] private Sprite emptyVerticalSprite; // Sprite vertical pour le rail de type Empty
    [SerializeField] private Sprite emptyHorizontalSprite; // Sprite horizontal pour le rail de type Empty
    [SerializeField] private Sprite emptyCornerSprite; // Sprite de virage pour le rail de type Empty

    [Header("SpriteRenderer")] [SerializeField]
    private SpriteRenderer spriteRenderer; // Le SpriteRenderer du Waypoint

    public Vector3 NextPosition;
    public Vector3 PreviousPosition;
    public Vector3 NextPositionNormalized;
    public Vector3 PreviousPositionNormalized;

    private BeltController.PathType currentPathType;

    private enum RailDirection
    {
        Vertical,
        Horizontal,
        Corner
    }

    private RailDirection currentDirection;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateOrientation();
        Debug.Log(currentDirection);
    }

    private void UpdateOrientation()
    {
        BeltController beltController = FindObjectOfType<BeltController>();
        if (beltController == null) return;

        int currentIndex = beltController.GetNodeIndex(transform.position);
        currentPathType = beltController.GetNodeType(transform.position);
        if (currentIndex == -1) return;

        PreviousPosition =
            currentIndex > 0 ? beltController.GetNodePosition(currentIndex - 1) : transform.position;
        NextPosition = currentIndex < beltController.GetNodeCount() - 1
            ? beltController.GetNodePosition(currentIndex + 1)
            : transform.position;

        SetOrientationAngle(PreviousPosition, NextPosition, currentPathType);
    }

    private void UpdateSprite()
    {
        switch (currentPathType)
        {
            case BeltController.PathType.Fill:
                spriteRenderer.sprite = currentDirection switch
                {
                    RailDirection.Vertical => fillVerticalSprite,
                    RailDirection.Horizontal => fillHorizontalSprite,
                    RailDirection.Corner => fillCornerSprite,
                    _ => fillHorizontalSprite
                };
                break;

            case BeltController.PathType.Follow:
                spriteRenderer.sprite = currentDirection switch
                {
                    RailDirection.Vertical => followVerticalSprite,
                    RailDirection.Horizontal => followHorizontalSprite,
                    RailDirection.Corner => followCornerSprite,
                };
                break;

            case BeltController.PathType.Empty:
                spriteRenderer.sprite = currentDirection switch
                {
                    RailDirection.Vertical => emptyVerticalSprite,
                    RailDirection.Horizontal => emptyHorizontalSprite,
                    RailDirection.Corner => emptyCornerSprite,
                    _ => emptyHorizontalSprite
                };
                break;
        }
    }

    public void RotateRail()
    {
        currentDirection = currentDirection switch
        {
            RailDirection.Horizontal => RailDirection.Vertical,
            RailDirection.Vertical => RailDirection.Horizontal,
            _ => RailDirection.Horizontal
        };

        UpdateSprite();
    }

    public void SetOrientationAngle(Vector3 previousPosition, Vector3 nextPosition,
        BeltController.PathType type)
    {
        PreviousPositionNormalized = (transform.position - previousPosition).normalized;
        NextPositionNormalized = (nextPosition - transform.position).normalized;

        if (NextPositionNormalized.y == 0 && PreviousPositionNormalized.y == 0)
        {
            currentDirection = RailDirection.Horizontal;
        }
        else if (NextPositionNormalized.x == 0 && PreviousPositionNormalized.x == 0)
        {
            currentDirection = RailDirection.Vertical;
        }
        else
        {
            currentDirection = RailDirection.Corner;
            if (NextPositionNormalized.x == 1 && PreviousPositionNormalized.x == 0 && NextPositionNormalized.y == 0 &&
                PreviousPositionNormalized.y == 1)
            {
                transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else if (NextPositionNormalized.x == 0 && PreviousPositionNormalized.x == 1 &&
                     NextPositionNormalized.y == -1 &&
                     PreviousPositionNormalized.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (NextPositionNormalized.x == -1 && PreviousPositionNormalized.x == 0 &&
                     NextPositionNormalized.y == 0 &&
                     PreviousPositionNormalized.y == -1)
            {
                transform.eulerAngles = new Vector3(0,0,90);
            }
        }

        UpdateSprite();
    }
}