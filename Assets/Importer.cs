using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class Importer : MonoBehaviour
{
    [SerializeField] private GameObject modelToImport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void Import()
    {
        GameObject importedModel = Instantiate(modelToImport);
        GameObject puzzleContainer = GameObject.FindWithTag("PuzzleContainer");

        foreach (Transform child in importedModel.transform)
        {
            child.transform.parent = puzzleContainer.transform;
        }

        DestroyImmediate(importedModel);
    }
}
