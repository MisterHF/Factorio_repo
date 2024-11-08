using System.Collections;
using TMPro;
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
            RangeAndTag(_mouseCollision.transform))
        {
            if (_mouseCollision.TryGetComponent<Pickeable>(out Pickeable _p) &&
                _mouseCollision.transform.CompareTag("Minable"))
            {
                float _delay = _p.delay;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventory.AddItem(_p.ScriptableObject, 1);
                mine = StartCoroutine(Mine());
            }
            else if (_mouseCollision.TryGetComponent<Pickeable>(out Pickeable _t) &&
                     _mouseCollision.transform.CompareTag("Build"))
            {
                float _delay = _t.delay;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventory.AddItem(_t.ScriptableObject, 1);
                Destroy(_t.gameObject);
                mine = StartCoroutine(Mine());
            }
        }
        else
        {
            yield return null;
        }
    }

    private bool RangeAndTag(Transform _pos)
    {
        return Vector3.Distance(_pos.position, transform.position) <= range &&
               (_pos.CompareTag("Minable") || _pos.CompareTag("Build"));
    }
}