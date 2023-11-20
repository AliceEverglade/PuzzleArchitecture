using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

/// <summary>
/// Adds ConnectorSpawner scripts to pieces and their subelements (excl. connectors themselves)
/// </summary>

public class ConnectorSpawnerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    private void AddConnectorSpawners()
    {
        foreach (Transform piece in transform)
        {
            AddConnectorSpawner(piece.gameObject);

            foreach (Transform subPiece in piece.transform)
            {
                AddConnectorSpawner(subPiece.gameObject);
            }
        }
    }

    private void AddConnectorSpawner(GameObject piece)
    {
        if (piece.GetComponent<ConnectorSpawner>() == null && !piece.CompareTag("ConnectionPoint"))
        {
            piece.AddComponent<ConnectorSpawner>();
        }
    }
}
