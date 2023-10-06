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
        BoundaryRock
    }

    public MaterialProperties GetProperty(MaterialNames key)
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
}

[Serializable]
public class MaterialData
{
    public MaterialLibrary.MaterialNames key;
    public MaterialProperties Value;
}
