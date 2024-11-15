using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mouvement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSprite;

    public float speedX;

    public void RightLeft(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
        playerAnimator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.magnitude));

        if(rb.linearVelocity.magnitude > 0)
            playerSprite.flipX = rb.linearVelocity.x < 0;
        
        speedX = rb.linearVelocity.x;
    }

    public void UpAndDown(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * speed);
        playerAnimator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.magnitude));
        
    }



    public void ZoomInZoomOut(InputAction.CallbackContext context)
    {
        Camera camera = Camera.main;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + context.ReadValue<float>(), 15, 30);
    }

    public void OpenAndCloseInventory(InputAction.CallbackContext context) 
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}