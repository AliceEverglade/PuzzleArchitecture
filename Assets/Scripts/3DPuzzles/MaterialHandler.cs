using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler : MonoBehaviour
{
    [SerializeField] private Material selectMaterial;
    private Material deselectMaterial;
    public bool Selected => transform.parent.GetComponent<PieceData>().Selected;

    // Start is called before the first frame update
    void Start()
    {
        deselectMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        SetMaterial();
    }

    private void SetMaterial()
    {
        if (Selected)
        {
            gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = deselectMaterial;
        }
    }
}