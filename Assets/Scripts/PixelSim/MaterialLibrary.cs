using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// contains a list of all materials and links them to an enum value to function as a dictionary
/// </summary>
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

    //function to update names for the list
    [Button]
    private void SetNames()
    {
        foreach(MaterialData data in Materials)
        {
            data.name = data.key.ToString();
        }
    }

    public List<MaterialData> GetPlacableMaterials()
    {
        List<MaterialData> placableMaterials = new List<MaterialData>();

        foreach (MaterialData data in Materials)
        {
            if (data.placable)
            {
                placableMaterials.Add(data);
            }
        }

        return placableMaterials;
    }
}

[Serializable]
public class MaterialData
{
    [HideInInspector] public string name;
    public bool placable;
    public MaterialLibrary.MaterialNames key;
    public PixelData Value;
}
