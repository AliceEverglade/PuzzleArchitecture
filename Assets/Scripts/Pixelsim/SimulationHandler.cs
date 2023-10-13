using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System.Linq;

public class SimulationHandler : MonoBehaviour
{
    public PixelData[,] Grid;
    private PixelData[,] nextFrame;
    public List<PixelData> GridList;
    public List<GameObject> Pixels;
    [SerializeField] private MaterialLibrary library;
    [SerializeField] private GameObject pixelPrefab;
    [SerializeField] private float pixelSize;
    [SerializeField] private float resolution;
    private Vector2Int gridSize;
    private float width;
    private float height;
    private float groundLevel => gridSize.y - (gridSize.y / 3);

    [SerializeField] private GameObject[] boundary = new GameObject[2];

    [Header("tick settings")]
    [SerializeField] private bool frameReady = false;
    [SerializeField] private bool done = false;
    private bool readyForUpdate => done && !frameReady;

    #region start and update
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region enable and disable
    private void OnEnable()
    {
        TickUpdateHandler.TickPing += UpdateHandler;
    }

    private void OnDisable()
    {
        TickUpdateHandler.TickPing -= UpdateHandler;
    }
    #endregion

    #region GridUpdate
    private void UpdateHandler()
    {
        //testing
        Debug.Log("update grid");
        return;

        //end of test
        if (frameReady)
        {
            SendToScreen();
            frameReady = false;
        }
        if (readyForUpdate)
        {
            done = false;
            StartCoroutine(UpdateGrid());
        }
    }

    private IEnumerator UpdateGrid()
    {
        nextFrame = Grid;
        //check physics for pixels from bottom to top
        for (int i = Grid.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                CalculatePixelPhysics();
            }
        }
        done = true;
        return null;
    }

    private void SendToScreen()
    {

    }
    #endregion

    #region grid functions
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
        return pixel.GetComponent<MeshRenderer>().bounds.size.x * pixelSize / resolution;
    }

    private void CalculatePixelPhysics()
    {

    }
    #endregion

    [Button]
    public void GenerateGrid()
    {
        #region field variables
        height = MathF.Abs(
            Camera.main.ScreenToWorldPoint(boundary[0].transform.position).y - 
            Camera.main.ScreenToWorldPoint(boundary[1].transform.position).y);

        width = MathF.Abs(
            Camera.main.ScreenToWorldPoint(boundary[0].transform.position).x - 
            Camera.main.ScreenToWorldPoint(boundary[1].transform.position).x);

        Vector3 start = Camera.main.ScreenToWorldPoint( 
            new Vector3(
                boundary[0].transform.position.x, 
                boundary[0].transform.position.y, 
                Camera.main.nearClipPlane));

        gridSize.x = (int)(width / (GetPixelSize(pixelPrefab)));
        gridSize.y = (int)(height / (GetPixelSize(pixelPrefab)));
        Debug.Log($"grid is of size x: {gridSize.x} by y: {gridSize.y} for a total of {gridSize.x * gridSize.y} cells.");
        Grid = new PixelData[gridSize.x,gridSize.y];
        #endregion

        #region generation loop
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i, j] = GenerateData(i,j);
                GridList.Add(Grid[i, j]);
                GameObject pixel = Instantiate(pixelPrefab,
                    new Vector3(
                        i * GetPixelSize(pixelPrefab) + (GetPixelSize(pixelPrefab) / 2) + start.x,
                        -j * GetPixelSize(pixelPrefab) - (GetPixelSize(pixelPrefab) / 2) + start.y,
                        this.transform.position.z),
                    Quaternion.identity, this.gameObject.transform);
                pixel.transform.localScale = new Vector3(pixelSize / resolution, pixelSize / resolution, pixelSize / resolution);
                pixel.GetComponent<MaterialInstance>().BaseColor = Grid[i, j].color;
                Pixels.Add(pixel);
            }
        }
        #endregion
    }

    public PixelData GenerateData(int i, int j)
    {
        PixelData pxData = new PixelData();
        #region material selection
        if (i == 0 || j == 0 || i == gridSize.x - 1 || j == gridSize.y - 1)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.BoundaryRock);
        }
        else if (j > groundLevel)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Water);
        }
        else
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Air);
        }
        #endregion
        pxData.position = new Vector2Int(i, j);
        pxData.color = pxData.properties.GetColor(new Vector2Int(i,j));
        return pxData;
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

    [Button]
    public void GetCellCount()
    {
        height = MathF.Abs(
            Camera.main.ScreenToWorldPoint(boundary[0].transform.position).y -
            Camera.main.ScreenToWorldPoint(boundary[1].transform.position).y);

        width = MathF.Abs(
            Camera.main.ScreenToWorldPoint(boundary[0].transform.position).x -
            Camera.main.ScreenToWorldPoint(boundary[1].transform.position).x);
        Debug.Log((int)(width / (GetPixelSize(pixelPrefab) / 2)) * (int)(height / (GetPixelSize(pixelPrefab) / 2)));
    }
}

[Serializable]
public class PixelData
{
    public MaterialProperties properties;
    public Vector2Int position;
    public Color color;

    [Range(0,1)]
    public float Hydration;
}
