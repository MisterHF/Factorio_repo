using System;
using UnityEngine;

public class Pickeable : MonoBehaviour
{
    [SerializeField] private ItemData scriptableObject;
    [SerializeField] public float delay;

    public ItemData ScriptableObject => scriptableObject;
}