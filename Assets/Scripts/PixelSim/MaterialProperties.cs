using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="data/PixelMaterial")]
public class MaterialProperties : ScriptableObject
{
    public List<PixelColor> Texture;

    public enum MatterState
    {
        Solid,
        Liquid,
        Gas
    }
    public MatterState State;
    public float[,] SpreadPattern = new float[5,3];

    public float Density;
}

[Serializable]
public class PixelColor
{
    public Color color;
    public float weight;
}
