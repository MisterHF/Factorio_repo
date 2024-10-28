using UnityEngine;

public class Pickeable : MonoBehaviour
{
    [SerializeField] private Test scriptableObject;
    
    public Test ScriptableObject
    {
        get => scriptableObject;
        set => scriptableObject = value;
    }
}