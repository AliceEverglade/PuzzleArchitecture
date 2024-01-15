using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using Unity.VisualScripting;

public class Importer : MonoBehaviour
{
    [SerializeField] private GameObject modelToImport;
    [SerializeField] private List<List<GameObject>> ModelLayers = new List<List<GameObject>>();
    [SerializeField] private GameObject importedModel;
    [SerializeField] private GameObject currentLayer;
    
    [SerializeField] private GameObject puzzleContainer;


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

        DeterminePieces(importedModel, puzzleContainer);
    }

    private void DeterminePieces(GameObject model, GameObject container)
    {
        ModelLayers.Clear();

        if(model.transform.childCount == 1)
        {
            foreach (Transform child in model.transform)
            {
                child.transform.parent = container.transform;
            }

            DestroyImmediate(model);

            importedModel = container.transform.GetChild(0).gameObject;
            DeterminePieces(importedModel, puzzleContainer);
        }

        else
        {
            foreach (Transform child in model.transform)
            {
                child.transform.parent = container.transform;
            }

            DestroyImmediate(model);

            for (int childIndex = 0; childIndex < container.transform.childCount; childIndex++)
            {
                ModelLayers.Add(new List<GameObject>());
                FindMeshes(container.transform.GetChild(childIndex).gameObject, childIndex);
            }
        }
    }

    private void FindMeshes(GameObject layer, int pieceIndex)
    {
        currentLayer = GameObject.Find(layer.name).gameObject;

        if (layer.transform.childCount > 0)
        {
            FindMeshes(layer.transform.GetChild(0).gameObject, pieceIndex);
        }
        else
        {
            Debug.Log(currentLayer);
            ModelLayers[pieceIndex].Add(currentLayer);
        }
    }
}

[System.Serializable]
public class ModelLayer
{
    public List<GameObject> subElements;
}
