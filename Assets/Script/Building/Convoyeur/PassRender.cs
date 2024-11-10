using UnityEngine;

public class PassRender : MonoBehaviour
{
    [HideInInspector] public BeltController BeltController;
    private LineRenderer lineRender;

    private void Start()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    public void SetLine(Vector3 startPosition, Vector3 endPosition)
    {
        lineRender.SetPosition(0, startPosition);
        lineRender.SetPosition(1, endPosition);
    }
}