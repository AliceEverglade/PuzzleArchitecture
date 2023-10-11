using System.Collections;
using System.Collections.Generic;
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
    private void CreatePiece()
    {
        GameObject newPiece = Instantiate(PiecePrefab);

        bool unnamed = true;
        int currentPiece = 1;
        while (unnamed)
        {
            if (GameObject.Find("Piece" + currentPiece) == null)
            {
                newPiece.name = "Piece" + currentPiece;
                unnamed = false;
            }
            else { currentPiece++; }
        }

        newPiece.transform.parent = GameObject.Find("PuzzleContainer").transform;
    }
}
