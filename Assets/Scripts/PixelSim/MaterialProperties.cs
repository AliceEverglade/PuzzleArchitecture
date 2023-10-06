using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/PixelMaterial")]
public class MaterialProperties : ScriptableObject
{
    public string MaterialName;
    [SerializeField] private Texture2D texture;
    [Range(1f, 100f)]
    [SerializeField] private int textureSize;
    
    private enum textureStyle
    {
        Static,
        Random
    }
    [SerializeField] private textureStyle style;
    public enum MatterState
    {
        Solid,
        Liquid,
        Gas,
        Granular,
        Meta
    }

    public MatterState State;
    public List<Vector2Int> SpreadPattern;

    public float Density;

    public Color GetColor(Vector2Int pos)
    {
        int x;
        int y;
        if(texture != null)
        {
            switch (style)
            {
                case textureStyle.Static:
                    x = pos.x > textureSize ? pos.x % textureSize : pos.x;
                    y = pos.y > textureSize ? pos.y % textureSize : pos.y;
                    return texture.GetPixel(x, y);
                case textureStyle.Random:
                    x = UnityEngine.Random.Range(0, textureSize);
                    y = UnityEngine.Random.Range(0, textureSize);
                    return texture.GetPixel(x, y);
            }
        }
        return Color.magenta;
    }
}
