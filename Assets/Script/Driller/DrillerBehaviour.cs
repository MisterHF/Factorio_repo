using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrillerBehaviour : MonoBehaviour
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private int range;
    [SerializeField] private Inventory inventoryMiner;
    private Coroutine mine;


    private void Awake()
    {
        mine = StartCoroutine(Mine());
    }


    private IEnumerator Mine()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 2);
        if (collision != null && collision.CompareTag("Minable"))
        {
            if (collision.TryGetComponent<Pickeable>(out Pickeable _p))
            {
                float _delay = _p.delay * miningSpeed;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventoryMiner.AddItem(_p.ScriptableObject);
                Debug.Log("Is Mined by Drill !");
            }
        }
        yield return new WaitForSeconds(1);
        mine = StartCoroutine(Mine());
    }
}
