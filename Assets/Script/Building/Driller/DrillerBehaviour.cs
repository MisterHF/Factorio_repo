using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DrillerBehaviour : Controller
{
    [SerializeField] private float miningSpeed;
    [SerializeField] private LayerMask LayerMinable;
    [SerializeField] private int range;
    [SerializeField] private BuildUi Ui;
    private DefaultSlot inventoryMiner;
    private Coroutine mine;
    private bool isStarted = false;
    private Collider2D[] results = new Collider2D[10];
    
    private void Start()
    {
        inventoryMiner = Ui.OpenPrefab.GetComponent<GetSlot>().Slot;
        mine = StartCoroutine(Mine());
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartCoroutine(Mine());
        }
    }

    public override ItemData GetItemData()
    {
        if (inventoryMiner.Count <= 0)
        {
            return null;
        }
        else
        {
            inventoryMiner.Count--;
            return inventoryMiner.Data;
        }
    }

    public override int GetItemCount()
    {
        int count = inventoryMiner.Count;
        inventoryMiner.Count = 0;
        return count;
    }

    private void OnDestroy()
    {
        StopCoroutine(Mine());
    }


    private IEnumerator Mine()
    {
        isStarted = true;
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 2, LayerMinable);
        if (collision != null &&
            Vector3.Distance(collision.gameObject.transform.position, transform.position) <= range)
        {
            Debug.Log("This is : " + collision.gameObject.name + ", " + "He has the tag : " + collision.gameObject.tag);
            if (collision.TryGetComponent<Pickeable>(out Pickeable _p))
            {
                float _delay = _p.delay;
                _delay = _delay * miningSpeed;
                yield return new WaitForSeconds(_delay);
                inventoryMiner.Count++;
                inventoryMiner.Data = _p.ScriptableObject;
                inventoryMiner.Img1.sprite = _p.ScriptableObject.sprite;
                inventoryMiner.Img1.color = Color.white;
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