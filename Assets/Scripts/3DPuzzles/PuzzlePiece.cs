using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectionPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("ConnectionPoint"))
            {
                connectionPoints.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
