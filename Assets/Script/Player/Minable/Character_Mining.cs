using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mining : MonoBehaviour
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private int range;
    [SerializeField] private Inventory inventory;
    private bool isMining;
    private Coroutine mine;

    public void OnMiningPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mine = StartCoroutine(Mine());
        }
    }

    public void OnMiningCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StopCoroutine(mine);
        }
    }

    private IEnumerator Mine()
    {
        Collider2D _mouseCollision = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        if (_mouseCollision != null &&
            Vector3.Distance(_mouseCollision.gameObject.transform.position, transform.position) <= range && _mouseCollision.gameObject.CompareTag("Minable"))
        {
            if (_mouseCollision.TryGetComponent<Pickeable>(out Pickeable _p))
            {
                float _delay = _p.delay;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventory.AddItem(_p.ScriptableObject, 1);
                Debug.Log("Is Mined !");
                mine = StartCoroutine(Mine());
            }
        }
        else
        {
            yield return null;
        }
    }
}