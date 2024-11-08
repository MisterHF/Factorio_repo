using UnityEngine;

public class PassRender : MonoBehaviour
{
    [HideInInspector] public BeltController BeltController;
    private Vector3 secondPosition;
    private LineRenderer lineRender;

    private void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        secondPosition = BeltController.SetSecondPosition(transform.position);
        SetLine();
    }

    private void SetLine()
    {
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, secondPosition);
    }
}