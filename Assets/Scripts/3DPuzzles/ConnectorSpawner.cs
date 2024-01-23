using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using static PieceSpawner;

/// <summary>
/// Responsible for spawning connectors during puzzle CREATION process.
/// </summary>

public class ConnectorSpawner : MonoBehaviour
{
    [SerializeField] private Bounds bounds = new Bounds();
    [SerializeField] private GameObject connectorPrefab;

    //Keeps track of the Face a connector needs to spawn on
    enum Face
    {
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back
    }

    //Keeps track of which way to offset the connectorpoint horizontally
    enum XPosition
    {
        Left,
        Center,
        Right
    }

    //Keeps track of which way to offset the connectorpoint vertically
    enum YPosition
    {
        Up,
        Center,
        Down
    }

    //Creates a connector
    [Button]
    private void SpawnConnector(Face face = Face.Front, XPosition xPosition = XPosition.Center, YPosition yPosition = YPosition.Center)
    {
        GameObject connector = Instantiate(connectorPrefab);

        int pieceID = 1;

        //Checks if connector is being spawned from Piece or SubElement
        if (transform.parent.CompareTag("PieceContainer"))
        {
            pieceID = transform.parent.GetComponent<PieceData>().ID;
            connector.transform.parent = gameObject.transform.parent.transform;
        }

        else if (transform.CompareTag("PieceContainer"))
        {
            pieceID = GetComponent<PieceData>().ID;
            connector.transform.parent = gameObject.transform;
        }

        connector.transform.position = gameObject.transform.position;

        SetConnectorFace(connector, face);
        SetConnectorPosition(connector, xPosition, yPosition);

        connector.name = "P" + pieceID + "C" + DetermineConnectorID(pieceID).ToString();
    }

    //Spawns connector at selected Face
    private void SetConnectorFace(GameObject connector, Face face)
    {
        connector.transform.position = gameObject.transform.position;
        bounds = gameObject.GetComponent<BoxCollider>().bounds;

        switch (face)
        {
            case Face.Top:
                connector.transform.position += transform.up * bounds.extents.y;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.up);
                break;
            case Face.Bottom:
                connector.transform.position += -transform.up * bounds.extents.y;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.down);
                break;
            case Face.Left:
                connector.transform.position += -transform.right * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.left);
                break;
            case Face.Right:
                connector.transform.position += transform.right * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.right);
                break;
            case Face.Front:
                connector.transform.position += transform.forward * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                break;
            case Face.Back:
                connector.transform.position += -transform.forward * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.back);
                break;
            default:
                break;
        }
    }

    //Sets connector to selected x and y positions.
    private void SetConnectorPosition(GameObject connector, XPosition xPosition, YPosition yPosition)
    {
        switch (xPosition)
        {
            case XPosition.Left:
                connector.transform.position += transform.right * -bounds.extents.x;
                break;
            case XPosition.Right:
                connector.transform.position += transform.right * bounds.extents.x;
                break;
            default:
                break;
        }

        switch (yPosition)
        {
            case YPosition.Up:
                connector.transform.position += transform.up * -bounds.extents.y;
                break;
            case YPosition.Down:
                connector.transform.position += transform.right * bounds.extents.y;
                break;
            default:
                break;
        }
    }

    //Calculates which ConnectorID the connector gets.
    private int DetermineConnectorID(int pieceID)
    {
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
    private void ResetBounds()
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        Vector3 position = new Vector3();
        int subPieceCount = 0;

        transform.DetachChildren();

        foreach (GameObject child in children)
        {
            if (child != null)
            {
                position += child.transform.position;

                subPieceCount++;
            }
        }

        transform.position = position / subPieceCount; //Wrapper element's position is centered on every subelement
        
        Debug.Log(transform.position);

        foreach (GameObject child in children)
        {
            if (child != null)
            {
                child.transform.parent = transform;
            }
        }

        bool unnamed = true;

        transform.parent = GameObject.Find("PuzzleContainer").transform; //Set PuzzleContainer as newPiece's parent.

        SetBounds();
    }

    private void SetBounds()
    {
        Bounds bounds = new Bounds();
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        bounds.center = gameObject.transform.position; //center bounds on piece's position

        if (!gameObject.gameObject.CompareTag("PieceContainer")) return; //if the piece in question is NOT tagged as a pieceContainer

        foreach (Transform child in gameObject.transform)
        {
            //if (child.CompareTag("ConnectionPoint")) continue;

            if (!child.CompareTag("ConnectionPoint")) // if the child element in question is NOT a connection point
            {
                // Encapsulate bounds to contain the bounds of the child element
                Debug.Log(child.GetComponent<BoxCollider>().bounds);
                bounds.Encapsulate(child.GetComponent<BoxCollider>().bounds);
            }
        }

        //Encapsulate piece's BoxCollider's bounds to contain the previously established bounds, and set its size to the size of the bounds.
        gameObject.GetComponent<BoxCollider>().bounds.Encapsulate(bounds);
        gameObject.GetComponent<BoxCollider>().size = bounds.size;
    }

    [Button]
    private void CopyConnector(GameObject pieceToCopyTo, GameObject connector)
    {
        int pieceID = pieceToCopyTo.transform.parent.gameObject.GetComponent<PieceData>().ID;

        GameObject connectorCopy = Instantiate(connector);
        connectorCopy.transform.parent = pieceToCopyTo.transform.parent.gameObject.transform;

        connectorCopy.name = "P" + pieceID + "C" + DetermineConnectorID(pieceID).ToString();
    }
}