using System.Collections;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Reflection;

using UnityEngine;
using EasyButtons;
using Unity.VisualScripting;

/// <summary>
/// Responsible for spawning pieces during puzzle CREATION process.
/// </summary>

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PiecePrefab;
    [SerializeField] private PuzzleDataSO pieceData;

    //Creates a piece with subelements
    public void CreatePiece(string name, List<GameObject> subElements)
    {
        GameObject newPiece = Instantiate(PiecePrefab);

        Vector3 position = new Vector3();
        int subPieceCount = 0;

        foreach (GameObject subPiece in subElements)
        {
            if (subPiece != null)
            {
                position += subPiece.transform.position;

                subPieceCount++;
            }
        }

        newPiece.transform.position = position / subPieceCount; //Wrapper element's position is centered on every subelement

        foreach (GameObject subPiece in subElements)
        {
            if (subPiece != null)
            {
                subPiece.transform.parent = newPiece.transform;
            }
        }

        bool unnamed = true;
        int currentPiece = 1;
        while (unnamed) // while the new piece is not named
        {
            if (GameObject.Find("Piece" + currentPiece) == null) // if a piece with name Piece[currentPiece] does not exist
            {
                //Name the piece Piece[currentPiece]
                newPiece.name = "Piece" + currentPiece;
                newPiece.GetComponent<PieceData>().ID = currentPiece;
                unnamed = false;
            }
            else { currentPiece++; }
        }

        GameObject tempContainer = GameObject.Find("tempContainer");

        if (tempContainer == null)
        {
            tempContainer = new GameObject();
        }
        
        tempContainer.name = "tempContainer";
        newPiece.transform.parent = tempContainer.transform; //Set tempcontainer as newPiece's parent.

        if (name != "" && name != null)
        {
            newPiece.name = name;
        }

        AddBoxColliders();
        SetBounds(newPiece);

        GameObject puzzleContainer = GameObject.Find("PuzzleContainer");       
    }

    //Sets the bounds of a Piece's boxcollider based on its child objects.
    void SetBounds(GameObject piece)
    {
        Bounds bounds = new Bounds();
        bounds.center = piece.transform.position; //center bounds on piece's position

        if (!piece.gameObject.CompareTag("PieceContainer")) return; //if the piece in question is NOT tagged as a pieceContainer

        foreach (Transform child in piece.transform)
        {
            //if (child.CompareTag("ConnectionPoint")) continue;

            if (!child.CompareTag("ConnectionPoint")) // if the child element in question is NOT a connection point
            {
                // Encapsulate bounds to contain the bounds of the child element
                if (child.GetComponent<BoxCollider>() == null)
                {
                    child.AddComponent<BoxCollider>();
                }
                bounds.Encapsulate(child.GetComponent<Collider>().bounds);
            }
        }

        //Encapsulate piece's BoxCollider's bounds to contain the previously established bounds, and set its size to the size of the bounds.
        piece.GetComponent<BoxCollider>().bounds.Encapsulate(bounds);
        piece.GetComponent<BoxCollider>().size = bounds.size;
    }

    //Adds box colliders to all puzzle piece sub-elements.
    private void AddBoxColliders()
    {
        foreach (Transform child in gameObject.transform)
        {
            foreach (Transform grandChild in child.transform)
            {
                grandChild.AddComponent<BoxCollider>();
            }
        }
    }
}