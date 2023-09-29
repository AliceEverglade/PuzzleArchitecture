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
        Water,
        Sand,
        Brick
    }
}

[Serializable]
public class MaterialData
{
    public MaterialLibrary.MaterialNames key;
    public MaterialProperties Value;
}
