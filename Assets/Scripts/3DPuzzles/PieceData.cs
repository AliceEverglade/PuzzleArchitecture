using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling Piece data.
/// </summary>

public class PieceData : MonoBehaviour
{
    public List<GameObject> SubElements;
    public List<GameObject> Connectors;
    public bool CanConnect => GetComponent<PieceHandler>().CanConnect;
    public bool Selected => GetComponent<PieceHandler>().Selected; // Gets Selected from PieceHandler, prevents needing to call PieceHandler and PieceData in other scripts.
    public int ID; // Keeps track of ID mainly for use in puzzle CREATION process.

    // Start is called before the first frame update
    void Start()
    {
        //Create list of SubElements and Connectors.
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
