using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectionPoints;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private ConnectionSO connectionSO;

    public List<GameObject> SubElements;
    public List<GameObject> Connectors;
    public bool Selected;
    public int PieceID;

    // Start is called before the first frame update
    void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();

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
