using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Character_Mining : MonoBehaviour
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private int range;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Slider Slider;
    private bool isMining;
    private Coroutine mine;

    private void Start()
    {
        Slider.gameObject.SetActive(false);
    }

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
            Slider.value = 0;
            Slider.gameObject.SetActive(false);
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
                Slider.gameObject.SetActive(true);
                float _delay = _p.delay;
                _delay = _delay * miningSpeed;
                Slider.maxValue = _delay;
                Slider.value = 0;
                float elapsedTime = 0;

                while (elapsedTime < _delay)
                {
                    elapsedTime += Time.deltaTime;
                    Slider.value = elapsedTime;
                    yield return null;
                }

                inventory.AddItem(_p.ScriptableObject, 1);
                mine = StartCoroutine(Mine());
            }
            else if (_mouseCollision.TryGetComponent<Pickeable>(out Pickeable _t) &&
                     _mouseCollision.transform.CompareTag("Build"))
            {
                float _delay = _t.delay;
                _delay = _delay * miningSpeed;
                Slider.maxValue = _delay;
                Slider.value = 0;
                float elapsedTime = 0;

                while (elapsedTime < _delay)
                {
                    elapsedTime += Time.deltaTime;
                    Slider.value = elapsedTime;
                    yield return null;
                }

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