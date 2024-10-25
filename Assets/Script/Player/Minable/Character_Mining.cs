using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mining : MonoBehaviour
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private int range;

    public void Mining(InputAction.CallbackContext context)
    {
        Collider2D _mouseCollision = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        if (_mouseCollision != null &&
            Vector3.Distance(_mouseCollision.gameObject.transform.position, transform.position) <= range)
        {
            if (_mouseCollision.TryGetComponent<Pickeable>(out Pickeable p))
            {
                //invenotyPLayer.Add(p.ScriptableObject);
                Debug.Log("Is Mined !");
            }
        }
    }
}