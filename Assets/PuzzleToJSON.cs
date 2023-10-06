using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EasyButtons;

public class PuzzleToJSON : MonoBehaviour
{
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private List<Connection> finalConnections;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void GeneratePuzzle(GameObject model) {
        puzzle = new Puzzle();
        GameObject puzzleContainer = GameObject.Find("PuzzleContainer");

        foreach (Transform puzzlePiece in puzzleContainer.transform)
        {
            Piece piece = new Piece();

            foreach (Transform child in puzzlePiece.transform)
            {
                if (child.gameObject.CompareTag("ConnectionPoint"))
                {
                    Debug.Log(child.gameObject.name);
                    piece.Connectors.Add(child.gameObject);
                }
                else
                {
                    Debug.Log(child.gameObject.name);
                    piece.SubPieces.Add(child.gameObject);
                }
            }

            puzzle.Pieces.Add(piece);
        }

        puzzle.Connections.AddRange(finalConnections);
        puzzle.OriginalModel = model;
    }

    [Serializable]
    public class Piece
    {
        public List<GameObject> SubPieces = new List<GameObject>();
        public List<GameObject> Connectors = new List<GameObject>();
    }

    [Serializable]
    public class Puzzle {
        public List<Piece> Pieces = new List<Piece>();
        public List<Connection> Connections = new List<Connection>();
        public GameObject OriginalModel;
    }
}
