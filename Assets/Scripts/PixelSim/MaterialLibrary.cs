using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MaterialLibrary")]
public class MaterialLibrary : ScriptableObject
{
    public List<MaterialData> Materials;

    public enum MaterialNames
    {
        Air,
        Water,
        Sand,
        Brick,
        Dirt,
        Concrete,
        BoundaryRock
    }

    public PixelData GetProperty(MaterialNames key)
    {
        foreach (MaterialData data in Materials)
        {
            if(data.key == key)
            {
                return data.Value;
            }
        }
        return null;
    }

    [Button]
    private void SetNames()
    {
        foreach(MaterialData data in Materials)
        {
            data.name = data.key.ToString();
        }
    }
}

[Serializable]
public class MaterialData
{
    [HideInInspector] public string name;
    public MaterialLibrary.MaterialNames key;
    public PixelData Value;
}
