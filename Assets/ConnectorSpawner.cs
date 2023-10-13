using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class ConnectorSpawner : MonoBehaviour
{
    private Bounds bounds = new Bounds();
    [SerializeField] private GameObject connectorPrefab;

    [Button]
    void SpawnConnector()
    {
        GameObject connector = Instantiate(connectorPrefab);

        int pieceID = 1;

        if (transform.parent.CompareTag("PieceContainer"))
        {
            pieceID = transform.parent.GetComponent<PieceID>().ID;
            connector.transform.parent = gameObject.transform.parent.transform;
            connector.transform.position = gameObject.transform.position;
        }
        else if (transform.CompareTag("PieceContainer"))
        {
            pieceID = GetComponent<PieceID>().ID;
            connector.transform.parent = gameObject.transform;
            connector.transform.position = bounds.center;
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

    [Button]
    void SetBounds()
    {
        foreach (Transform child in transform)
        {
            if (!child.CompareTag("ConnectionPoint"))
            {
                bounds.Encapsulate(child.GetComponent<Collider>().bounds);
            }
        }

        GetComponent<BoxCollider>().bounds.Encapsulate(bounds);
        Debug.Log(GetComponent<Collider>().bounds);

        GetComponent<BoxCollider>().size = bounds.size;
        GetComponent<BoxCollider>().center = bounds.center;

        Debug.Log(bounds);
    }
}
