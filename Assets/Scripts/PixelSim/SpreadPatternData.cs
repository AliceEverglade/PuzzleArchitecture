using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// has a list of positions relative to the current position of a pixel to check for it to spread to.
/// </summary>
[CreateAssetMenu(menuName ="Data/SpreadPattern")]
public class SpreadPatternData : ScriptableObject
{
    public List<Vector2Int> Pattern;
    public bool GoesUp;
}
