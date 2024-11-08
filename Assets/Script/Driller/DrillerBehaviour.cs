using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrillerBehaviour : MonoBehaviour
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private int range;
    [SerializeField] private DefaultSlot inventoryMiner;
    private Coroutine mine;
    private bool isStarted = false;


    private void Start()
    {
        mine = StartCoroutine(Mine());
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(Mine());
        }
    }


    private IEnumerator Mine()
    {
        isStarted = true;
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, 2).Where(x => x.gameObject != transform.gameObject).ToArray();
        if (collision != null &&
            // Vector3.Distance(collision.gameObject.transform.position, transform.position) <= range &&
            collision[0].gameObject.CompareTag("Minable"))
        {
            Debug.Log("This is : " + collision[0].gameObject.name + ", " + "He has the tag : " + collision[0].gameObject.tag);
            if (collision[0].TryGetComponent<Pickeable>(out Pickeable _p))
            {
                float _delay = _p.delay;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventoryMiner.Count++;
                inventoryMiner.Data = _p.ScriptableObject;
                Debug.Log("Is Mined by Drill !");
                mine = StartCoroutine(Mine());
            }
        }
        else
        {
            Debug.Log("Nothing To Mine !");
            isStarted = false;
            yield return null;
        }
    }
}