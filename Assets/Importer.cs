using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using Unity.VisualScripting;
using System.Xml;
using System.ComponentModel;

public class Importer : MonoBehaviour
{
    [SerializeField] private GameObject modelToImport;
    [SerializeField] private PieceSpawner pieceSpawner;

    [SerializeField] private List<GameObject> PuzzlePieces;

    private List<List<GameObject>> ModelLayers = new List<List<GameObject>>();
    private List<string> names = new List<string>();
    private GameObject puzzleContainer;
    private GameObject importedModel;
    private GameObject currentLayer;
    private GameObject highestLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Imports 3D Model
    [Button]
    private void Import()
    {
        importedModel = Instantiate(modelToImport);
        puzzleContainer = GameObject.FindWithTag("PuzzleContainer");

        importedModel.transform.parent = puzzleContainer.transform;

        DeterminePieces(importedModel, puzzleContainer);
    }

    private void DeterminePieces(GameObject model, GameObject container)
    {
        ModelLayers.Clear();
        names.Clear();
        PuzzlePieces.Clear();

        if (container.transform.childCount == 1)
        {
            highestLayer = container.transform.GetChild(0).gameObject;
            FindPuzzlePieces(highestLayer);
            PutPuzzlePiecesInContainer();
        }

        for (int childIndex = 0; childIndex < container.transform.childCount; childIndex++)
        {
            ModelLayers.Add(new List<GameObject>());
            FindMeshes(container.transform.GetChild(childIndex).gameObject, childIndex);
        }

        Debug.Log(ModelLayers[0][0]);
        Debug.Log(ModelLayers[0][1]);

        GetNames();
        CreatePieces();
    }

    private void FindPuzzlePieces(GameObject layer)
    {
        if (layer.transform.childCount > 1)
        {
            foreach (Transform child in layer.transform)
            {
                PuzzlePieces.Add(child.gameObject);
            }
        }
        else
        {
            FindPuzzlePieces(layer.transform.GetChild(0).gameObject);
        }
    }

    private void PutPuzzlePiecesInContainer()
    {
        foreach (GameObject puzzlePiece in PuzzlePieces)
        {
            puzzlePiece.transform.parent = puzzleContainer.transform;
        }

        DestroyImmediate(highestLayer);
    }

    private void FindMeshes(GameObject layer, int pieceIndex)
    {
        currentLayer = GameObject.Find(layer.name).gameObject;

        if (layer.transform.childCount > 0)
        {
            foreach (Transform child in layer.transform)
            {
                FindMeshes(child.gameObject, pieceIndex);
            }
        }
        else
        {
            ModelLayers[pieceIndex].Add(currentLayer);
            Debug.Log("added " + layer.name);
        }
    }

    private void GetNames()
    {
        foreach (Transform child in puzzleContainer.transform)
        {
            names.Add(child.name);
        }
    }

    private void CreatePieces()
    {
        int index = 0;

        foreach (List<GameObject> subElements in ModelLayers)
        {
            pieceSpawner.CreatePiece(names[index], subElements);
            index++;
        }

        int totalChildren = puzzleContainer.transform.childCount;

        for (int i = 0; i < totalChildren; i++)
        {
            DestroyImmediate(puzzleContainer.transform.GetChild(0).gameObject);
        }

        totalChildren = GameObject.Find("tempContainer").transform.childCount;

        for (int i = 0; i < totalChildren; i++)
        {
            GameObject.Find("tempContainer").transform.GetChild(0).transform.parent = puzzleContainer.transform;
        }
    }
}

[System.Serializable]
public class ModelLayer
{
    public List<GameObject> subElements;
}
