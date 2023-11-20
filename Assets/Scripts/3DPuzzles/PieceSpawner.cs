using System.Collections;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Reflection;

using UnityEngine;
using EasyButtons;

/// <summary>
/// Responsible for spawning pieces during puzzle CREATION process.
/// </summary>

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PiecePrefab;
    [SerializeField] private PuzzleDataSO pieceData;

    //Creates a piece with subelements
    [Button]
    private void CreatePiece(SubElementList subElementList)
    {
        GameObject newPiece = Instantiate(PiecePrefab);

        subElementList.MakeList(); //EasyButtons does not support list parameters

        Vector3 position = new Vector3();
        int subPieceCount = 0;

        foreach (GameObject subPiece in subElementList.SubElements)
        {
            if (subPiece != null)
            {
                position += subPiece.transform.position;
                
                subPieceCount++;
            }
        }

        newPiece.transform.position = position / subPieceCount; //Wrapper element's position is centered on every subelement

        foreach (GameObject subPiece in subElementList.SubElements)
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

        newPiece.transform.parent = GameObject.Find("PuzzleContainer").transform; //Set PuzzleContainer as newPiece's parent.

        SetBounds(newPiece);
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
                bounds.Encapsulate(child.GetComponent<Collider>().bounds);
            }
        }

        //Encapsulate piece's BoxCollider's bounds to contain the previously established bounds, and set its size to the size of the bounds.
        piece.GetComponent<BoxCollider>().bounds.Encapsulate(bounds);
        piece.GetComponent<BoxCollider>().size = bounds.size;
    }

    [System.Serializable]
    public class SubElementList
    {
        //Awkward workaround for list parameter.
        public GameObject SubElement1;
        public GameObject SubElement2;
        public GameObject SubElement3;
        public GameObject SubElement4;
        public GameObject SubElement5;
        public GameObject SubElement6;
        public GameObject SubElement7;
        public GameObject SubElement8;
        public GameObject SubElement9;
        public GameObject SubElement10;
        public GameObject SubElement11;
        public GameObject SubElement12;
        public GameObject SubElement13;
        public GameObject SubElement14;
        public GameObject SubElement15;
        public GameObject SubElement16;
        public GameObject SubElement17;
        public GameObject SubElement18;
        public GameObject SubElement19;
        public GameObject SubElement20;

        [HideInInspector]
        public List<GameObject> SubElements;

        public void MakeList()
        {
            SubElements.Add(SubElement1);
            SubElements.Add(SubElement2);
            SubElements.Add(SubElement3);
            SubElements.Add(SubElement4);
            SubElements.Add(SubElement5);
            SubElements.Add(SubElement6);
            SubElements.Add(SubElement7);
            SubElements.Add(SubElement8);
            SubElements.Add(SubElement9);
            SubElements.Add(SubElement10);
            SubElements.Add(SubElement11);
            SubElements.Add(SubElement12);
            SubElements.Add(SubElement13);
            SubElements.Add(SubElement14);
            SubElements.Add(SubElement15);
            SubElements.Add(SubElement16);
            SubElements.Add(SubElement17);
            SubElements.Add(SubElement18);
            SubElements.Add(SubElement19);
            SubElements.Add(SubElement20);
        }
    }
}
