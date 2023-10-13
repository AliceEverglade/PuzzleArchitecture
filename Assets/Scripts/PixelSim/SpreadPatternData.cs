using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/SpreadPattern")]
public class SpreadPatternData : ScriptableObject
{
    public List<Vector2Int> Pattern;
    public bool GoesUp;
}
