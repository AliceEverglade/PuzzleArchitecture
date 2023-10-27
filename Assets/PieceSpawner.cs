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

        subPieceList.MakeList();

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

        newPiece.transform.position = position / subPieceCount;

        foreach (GameObject subPiece in subPieceList.SubPieces)
        {
            if (subPiece != null)
            {
                
                subPiece.transform.parent = newPiece.transform;
                
            }
        }

        bool unnamed = true;
        int currentPiece = 1;
        while (unnamed)
        {
            if (GameObject.Find("Piece" + currentPiece) == null)
            {
                newPiece.name = "Piece" + currentPiece;
                newPiece.GetComponent<PieceID>().ID = currentPiece;
                unnamed = false;
            }
            else { currentPiece++; }
        }

        newPiece.transform.parent = GameObject.Find("PuzzleContainer").transform;

        SetBounds(newPiece);
    }

    void SetBounds(GameObject piece)
    {
        Bounds bounds = new Bounds();

        if (!piece.gameObject.CompareTag("PieceContainer")) return;

        foreach (Transform child in piece.transform)
        {
            if (child.CompareTag("ConnectionPoint")) continue;

            if (!child.CompareTag("ConnectionPoint"))
            {
                bounds.Encapsulate(child.GetComponent<Collider>().bounds);
                Debug.Log(bounds);
            }
        }

        piece.GetComponent<BoxCollider>().bounds.Encapsulate(bounds);
        Debug.Log(piece.GetComponent<Collider>().bounds);

        piece.GetComponent<BoxCollider>().size = bounds.size;

        Debug.Log(bounds);
    }

    [System.Serializable]
    public class SubPieceList
    {
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
