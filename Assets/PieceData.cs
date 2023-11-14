using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour
{
    public List<GameObject> SubElements;
    public List<GameObject> Connectors;
    public bool Selected => GetComponent<PieceHandler>().Selected;
    public int PieceID;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.CompareTag("ConnectionPoint"))
            {
                Connectors.Add(child.gameObject);
            }
            else
            {
                SubElements.Add(child.gameObject);
            }
        }
    }
}
