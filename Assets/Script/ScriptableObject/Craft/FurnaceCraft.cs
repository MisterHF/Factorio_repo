using UnityEngine;

[CreateAssetMenu(fileName = "FurnaceCraft", menuName = "Scriptable Objects/FurnaceCraft")]
public class FurnaceCraft : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public ItemData Item1;
    [SerializeField] public ItemData Item2;
    
    public Sprite Item2Sprite => Item2 != null ? Item2.sprite : null;
}
