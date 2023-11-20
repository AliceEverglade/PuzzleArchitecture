using System.Collections;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Reflection;

using UnityEngine;
using EasyButtons;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PiecePrefab;
    [SerializeField] private PuzzleDataSO pieceData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    private void CreatePiece(SubPieceList subPieceList)
    {
        GameObject newPiece = Instantiate(PiecePrefab);

        subPieceList.MakeList(); //EasyButtons does not support list parameters

        Vector3 position = new Vector3();
        int subPieceCount = 0;

        foreach (GameObject subPiece in subPieceList.SubPieces)
        {
            if (subPiece != null)
            {
                position += subPiece.transform.position;
                
                subPieceCount++;
            }
        }

        newPiece.transform.position = position / subPieceCount; //Wrapper element's position is centered on every subelement

        foreach (GameObject subPiece in subPieceList.SubPieces)
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
    public class SubPieceList
    {
        //Awkward workaround for list parameter.
        public GameObject SubPiece1;
        public GameObject SubPiece2;
        public GameObject SubPiece3;
        public GameObject SubPiece4;
        public GameObject SubPiece5;
        public GameObject SubPiece6;
        public GameObject SubPiece7;
        public GameObject SubPiece8;
        public GameObject SubPiece9;
        public GameObject SubPiece10;
        public GameObject SubPiece11;
        public GameObject SubPiece12;
        public GameObject SubPiece13;
        public GameObject SubPiece14;
        public GameObject SubPiece15;
        public GameObject SubPiece16;
        public GameObject SubPiece17;
        public GameObject SubPiece18;
        public GameObject SubPiece19;
        public GameObject SubPiece20;

        [HideInInspector]
        public List<GameObject> SubPieces;

        public void MakeList()
        {
            SubPieces.Add(SubPiece1);
            SubPieces.Add(SubPiece2);
            SubPieces.Add(SubPiece3);
            SubPieces.Add(SubPiece4);
            SubPieces.Add(SubPiece5);
            SubPieces.Add(SubPiece6);
            SubPieces.Add(SubPiece7);
            SubPieces.Add(SubPiece8);
            SubPieces.Add(SubPiece9);
            SubPieces.Add(SubPiece10);
            SubPieces.Add(SubPiece11);
            SubPieces.Add(SubPiece12);
            SubPieces.Add(SubPiece13);
            SubPieces.Add(SubPiece14);
            SubPieces.Add(SubPiece15);
            SubPieces.Add(SubPiece16);
            SubPieces.Add(SubPiece17);
            SubPieces.Add(SubPiece18);
            SubPieces.Add(SubPiece19);
            SubPieces.Add(SubPiece20);
        }
    }
}
