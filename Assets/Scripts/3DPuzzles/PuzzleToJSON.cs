using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using EasyButtons;

public class PuzzleToJSON : MonoBehaviour
{
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private List<Connection> finalConnections;
    [SerializeField] private string directoryPath;

    // Start is called before the first frame update
    void Start()
    {
        directoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Puzzles" + Path.DirectorySeparatorChar;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void PuzzleToJson(GameObject model, string filename) {
        puzzle = new Puzzle();
        GameObject puzzleContainer = GameObject.Find("PuzzleContainer");

        foreach (Transform puzzlePiece in puzzleContainer.transform)
        {
            Piece piece = new Piece();

            piece = CreatePiece(puzzlePiece.gameObject);

            foreach (Transform child in puzzlePiece.transform)
            {
                if (child.gameObject.CompareTag("ConnectionPoint"))
                {
                    Debug.Log(child.gameObject.name);

                    piece.Connectors.Add(CreateConnector(child.gameObject));
                }
                else
                {
                    Debug.Log(child.gameObject.name);

                    piece.SubPieces.Add(CreateSubPiece(child.gameObject));
                }
            }

            puzzle.Pieces.Add(piece);
        }

        puzzle.Connections.AddRange(finalConnections);
        puzzle.PuzzleObject = model;

        string json = JsonUtility.ToJson(puzzle);

        SavePuzzle(json, filename);
    }

    public Connector CreateConnector(GameObject gameObject)
    {
        Connector connector = new Connector();

        connector.name = gameObject.name;
        connector.position = gameObject.transform.localPosition;
        connector.rotation = gameObject.transform.localRotation;
        connector.size = gameObject.transform.localScale;

        return connector;
    }

    public SubPiece CreateSubPiece(GameObject gameObject)
    {
        SubPiece subPiece = new SubPiece();

        subPiece.name = gameObject.name;
        subPiece.position = gameObject.transform.localPosition;
        subPiece.rotation = gameObject.transform.localRotation;
        subPiece.size = gameObject.transform.localScale;

        return subPiece;
    }

    public Piece CreatePiece(GameObject gameObject)
    {
        Piece piece = new Piece();

        piece.name = gameObject.name;
        piece.position = gameObject.transform.localPosition;
        piece.rotation = gameObject.transform.localRotation;
        piece.size = gameObject.transform.localScale;

        return piece;
    }

    public void SavePuzzle(string json, string fileName)
    {
        string fullPath = directoryPath + fileName + ".json";

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(json);

                    Debug.Log(fullPath);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    [Button]
    public void LoadPuzzle(string fileName)
    {
        Puzzle puzzle = new Puzzle();
        string filePath = Path.Combine(directoryPath + fileName);

        if (File.Exists (filePath))
        {
            try
            {
                string json = "";
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }

                puzzle = JsonUtility.FromJson<Puzzle>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to load data from: " + filePath + "\n" + e);
            }
        }

        OpenPuzzle(puzzle);
    }

    public void OpenPuzzle(Puzzle loadedPuzzle)
    {
        puzzle = loadedPuzzle;
    }

    [Serializable]
    public class Connector
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;
    }

    [Serializable]
    public class SubPiece
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;
    }

    [Serializable]
    public class Piece
    {
        public List<SubPiece> SubPieces = new List<SubPiece>();
        public List<Connector> Connectors = new List<Connector>();

        public string name;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;
    }

    [Serializable]
    public class Puzzle {
        public List<Piece> Pieces = new List<Piece>();
        public List<Connection> Connections = new List<Connection>();
        
        public GameObject PuzzleObject;
    }
}
