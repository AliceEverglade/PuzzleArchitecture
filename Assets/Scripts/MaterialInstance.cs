using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInstance : MonoBehaviour
{
    GameObject go;
    public Color BaseColor;
    public bool Selected;
    public Color BorderColor;
    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
        go = this.gameObject;
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.color = BaseColor;
    }
}
