using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class SimulationHandler : MonoBehaviour
{
    public PixelData[,] Grid;
    public List<PixelData> GridList;
    [SerializeField] private GameObject pixelPrefab;
    [SerializeField] private Vector2Int gridSize;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GridPosToIndex(Vector2 pos)
    {
        return gridSize.x * pos.y + pos.x;
    }

    public Vector2 IndexToGridPos(float index)
    {
        Vector2 output = new Vector2(0,0);
        output.y = MathF.Floor(index / gridSize.x);
        output.x = index % gridSize.x;
        return output;
    }

    public float GetPixelSize(GameObject pixel)
    {
        return pixel.GetComponent<MeshRenderer>().bounds.size.x;
    }

    [Button]
    public void GenerateGrid()
    {

        float aspect = (float)Screen.width / Screen.height;

        float worldHeight = Camera.main.orthographicSize * 2;

        float worldWidth = worldHeight * aspect;

        gridSize.x = (int)(worldWidth / GetPixelSize(pixelPrefab));
        gridSize.y = (int)(worldHeight / GetPixelSize(pixelPrefab));

        Grid = new PixelData[gridSize.y,gridSize.x];

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i,j] = new PixelData();
            }
        }
    }
}

public class PixelData
{
    public MaterialProperties properties;
    public Vector2 position;
    public Color color;

    [Range(0,1)]
    public float Hydration;
}
