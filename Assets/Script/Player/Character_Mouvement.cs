using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mouvement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    [SerializeField] private GameObject inventory;

    public void RightLeft(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
    }

    public void UpAndDown(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * speed);
    }



    public void ZoomInZoomOut(InputAction.CallbackContext context)
    {
        Camera camera = Camera.main;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + context.ReadValue<float>(), 2, 15);
    }

    public void OpenAndCloseInventory(InputAction.CallbackContext context) 
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}