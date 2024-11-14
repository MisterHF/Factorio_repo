using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Sprites pour chaque type de rail")]
    [SerializeField] private Sprite fillVerticalSprite; // Sprite vertical pour le rail de type Fill
    [SerializeField] private Sprite fillHorizontalSprite; // Sprite horizontal pour le rail de type Fill
    [SerializeField] private Sprite fillCornerSprite; // Sprite de virage pour le rail de type Fill

    [SerializeField] private Sprite followVerticalSprite; // Sprite vertical pour le rail de type Follow
    [SerializeField] private Sprite followHorizontalSprite; // Sprite horizontal pour le rail de type Follow
    [SerializeField] private Sprite followCornerSprite; // Sprite de virage pour le rail de type Follow

    [SerializeField] private Sprite emptyVerticalSprite; // Sprite vertical pour le rail de type Empty
    [SerializeField] private Sprite emptyHorizontalSprite; // Sprite horizontal pour le rail de type Empty
    [SerializeField] private Sprite emptyCornerSprite; // Sprite de virage pour le rail de type Empty

    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer spriteRenderer; // Le SpriteRenderer du Waypoint

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
        // Si aucun sprite n'est assigné, on récupère celui du SpriteRenderer
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialiser le type de direction (horizontal par défaut)
        currentDirection = RailDirection.Horizontal;
        UpdateSprite();
    }

    public void SetPathType(BeltController.PathType type)
    {
        currentPathType = type;
        UpdateSprite();  // Met à jour le sprite lorsque le type de chemin est défini
    }

    private void UpdateSprite()
    {
        // Met à jour le sprite en fonction du type de rail et de la direction actuelle
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
                    _ => followHorizontalSprite
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
        // Changer la direction du rail de 90 degrés (clockwise)
        currentDirection = currentDirection switch
        {
            RailDirection.Horizontal => RailDirection.Vertical,
            RailDirection.Vertical => RailDirection.Horizontal,
            _ => RailDirection.Horizontal
        };

        // Mettre à jour le sprite après chaque rotation
        UpdateSprite();
    }

    public void SetOrientation(Vector3 previousPosition, Vector3 currentPosition, BeltController.PathType type)
    {
        // Si le type est "Follow" et que la direction change, on doit mettre un virage
        if (type == BeltController.PathType.Follow)
        {
            if (previousPosition.x != currentPosition.x && previousPosition.y != currentPosition.y)
            {
                currentDirection = RailDirection.Corner;
            }
            else
            {
                currentDirection = previousPosition.x == currentPosition.x ? RailDirection.Vertical : RailDirection.Horizontal;
            }
        }
        UpdateSprite();
    }
}
