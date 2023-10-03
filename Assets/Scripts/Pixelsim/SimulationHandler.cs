using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System.Linq;

public class SimulationHandler : MonoBehaviour
{
    public PixelData[,] Grid;
    public List<PixelData> GridList;
    public List<GameObject> Pixels;
    [SerializeField] private MaterialLibrary library;
    [SerializeField] private GameObject pixelPrefab;
    [SerializeField] private float pixelSize;
    private Vector2Int gridSize;

    [SerializeField] private GameObject[] boundary = new GameObject[2];


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
        return pixel.GetComponent<MeshRenderer>().bounds.size.x * pixelSize;
    }

    [Button]
    public void GenerateGrid()
    {
        float aspect = (float)Screen.width / Screen.height;

        float worldHeight = Camera.main.orthographicSize * 2;

        float worldWidth = worldHeight * aspect;

        Vector3 start = Camera.main.ScreenToWorldPoint( new Vector3(0,0, Camera.main.nearClipPlane));

        gridSize.x = (int)(worldWidth / (GetPixelSize(pixelPrefab) / 2));
        gridSize.y = (int)(worldHeight / (GetPixelSize(pixelPrefab) / 2));
        Debug.Log($"grid is of size x: {gridSize.x} by y: {gridSize.y} for a total of {gridSize.x * gridSize.y} cells.");
        Grid = new PixelData[gridSize.y,gridSize.x];

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i,j] = new PixelData();
                GridList.Add(Grid[i,j]);
                GameObject pixel =  Instantiate(pixelPrefab, 
                    new Vector3(
                        i * GetPixelSize(pixelPrefab) - GetPixelSize(pixelPrefab) + start.x, 
                        -j * GetPixelSize(pixelPrefab) + GetPixelSize(pixelPrefab) - start.y, 
                        this.transform.position.z),
                    Quaternion.identity,this.gameObject.transform);
                pixel.transform.localScale = new Vector3(pixelSize,pixelSize,pixelSize);
                Pixels.Add(pixel);
            }
        }
    }

    [Button]
    public void ClearGrid()
    {
        GridList.Clear();
        Pixels.Clear();
        Grid = null;
        List<Transform> children = GetComponentsInChildren<Transform>().ToList();
        children.Remove(transform);
        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }

    }
}

[Serializable]
public class PixelData
{
    public MaterialProperties properties;
    public Vector2 position;
    public Color color;

    [Range(0,1)]
    public float Hydration;
}
