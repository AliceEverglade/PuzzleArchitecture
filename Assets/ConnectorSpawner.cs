using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class ConnectorSpawner : MonoBehaviour
{
    private Bounds bounds = new Bounds();
    [SerializeField] private GameObject connectorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void SpawnConnector()
    {
        GameObject connector = Instantiate(connectorPrefab);

        int pieceID = 1;

        if (transform.parent.CompareTag("PieceContainer"))
        {
            pieceID = transform.parent.GetComponent<PieceID>().ID;
            connector.transform.parent = gameObject.transform;
        }
        else if (transform.CompareTag("PieceContainer"))
        {
            pieceID = GetComponent<PieceID>().ID;
            connector.transform.parent = gameObject.transform;
        }

        connector.name = "P" + pieceID + "C" + DetermineConnectorID(pieceID).ToString();
    }

    int DetermineConnectorID(int pieceID) {
        bool unnamed = true;
        int currentConnector = 1;
        while (unnamed)
        {
            if (GameObject.Find("P" + pieceID + "C" + currentConnector) == null)
            {
                unnamed = false;
            }
            else { currentConnector++; }
        }

        return currentConnector;
    }

    //[Button]
    /*void SetBounds()
    {
        bounds = new Bounds();

        foreach (Transform child in transform)
        {
            bounds.size += transform.gameObject.transform.localScale;
        }

        GetComponent<Collider>().bounds.Equals() = ;
    }*/
}
