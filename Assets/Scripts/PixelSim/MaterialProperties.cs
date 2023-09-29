using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/PixelMaterial")]
public class MaterialProperties : ScriptableObject
{
    public List<PixelColor> Texture;

    public enum MatterState
    {
        Solid,
        Liquid,
        Gas,
        Meta
    }

    public MatterState State;
    public GridCell[] SpreadPattern = new GridCell[15];

    public float Density;
}

[Serializable]
public class PixelColor
{
    public Color color;
    public float weight;
}

[Serializable]
public class GridCell
{
    public Vector2 pos;
    public float value;
}
