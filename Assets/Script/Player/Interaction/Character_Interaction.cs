using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Interaction : MonoBehaviour
{
    [SerializeField] private float range;

    public void OpenBuild(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        Collider2D _mouseCollision = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);

        if (_mouseCollision != null &&
            Vector3.Distance(_mouseCollision.gameObject.transform.position, transform.position) <= range &&
            _mouseCollision.CompareTag("Build"))
        {
            if (_mouseCollision.gameObject.TryGetComponent<BuildUi>(out BuildUi _b))
            {
                _b.OpenUI();
            }
        }
    }

}