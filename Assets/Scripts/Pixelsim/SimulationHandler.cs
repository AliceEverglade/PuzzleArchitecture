using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System.Linq;
using Unity.VisualScripting;

public class SimulationHandler : MonoBehaviour
{
    public PixelData[,] Grid;
    private PixelData[,] nextFrame;
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
    [SerializeField] private bool done = true;
    [SerializeField] private bool RandomizeSpreadPattern;
    private bool readyForUpdate => done && !frameReady;

    public enum Axis
    {
        x = 0,
        y = 1
    }

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
        if (frameReady)
        {
            SendToScreen();
            
        }
        if (readyForUpdate)
        {
            StartCoroutine(UpdateGrid());
        }
    }

    private IEnumerator UpdateGrid()
    {
        done = false;
        nextFrame = Grid;
        //check physics for pixels from bottom to top
        for (int y = 0; y < Grid.GetLength((int)Axis.y); y++)
        {
            for (int x = 0; x < Grid.GetLength((int)Axis.x); x++)
            {
                if (Grid[x,y].properties.State != MaterialProperties.MatterState.Meta)
                {
                    //chem check
                    CalculatePixelPhysics(x, y);
                }
            }
        }
        done = true;
        frameReady = true;
        yield return 0;
    }

    private void SendToScreen()
    {
        Grid = nextFrame;
        for (int y = 0; y < Grid.GetLength((int)Axis.y); y++)
        {
            for (int x = 0; x < Grid.GetLength((int)Axis.x); x++)
            {
                SetPixelData(Pixels[GridPosToIndex(new Vector2Int(x, y))], x, y);
            }
        }

        frameReady = false;
    }
    #endregion

    #region grid functions
    public int GridPosToIndex(Vector2Int pos)
    {
        return Grid.GetLength((int)Axis.x) * pos.y + pos.x;
    }

    public float GetPixelSize(GameObject pixel)
    {
        return pixel.GetComponent<MeshRenderer>().bounds.size.x * pixelSize / resolution;
    }

    private void FlipPixels(Vector2Int pos1, Vector2Int pos2)
    {
        PixelData temp = Grid[pos1.x, pos1.y];
        nextFrame[pos1.x, pos1.y] = Grid[pos2.x, pos2.y];
        nextFrame[pos2.x,pos2.y] = temp;
    }

    private bool CalculatePixelPhysics(int x, int y)
    {
        Vector2Int origin = new Vector2Int(x, y);
        List<int> checkedIndexes = new List<int>();
        int currentIndex;
        for(int i = 0; i < Grid[x, y].properties.SpreadPattern.Pattern.Count; i++) //might want to randomize order of check
        {
            if (checkedIndexes.Count == Grid[x, y].properties.SpreadPattern.Pattern.Count) return false;
            do
            {
                if(i == 0)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex = RandomizeSpreadPattern ? UnityEngine.Random.Range(0, Grid[x, y].properties.SpreadPattern.Pattern.Count) : i;
                }
            }
            while (checkedIndexes.Contains(currentIndex));
            checkedIndexes.Add(currentIndex);
            Vector2Int targetPos = Grid[x, y].properties.SpreadPattern.Pattern[currentIndex];
            targetPos.y = -targetPos.y;
            //if not out of bounds
            if (!(
                targetPos.x + x < 0 || 
                targetPos.y + y < 0 || 
                targetPos.x + x >= Grid.GetLength((int)Axis.x) || 
                targetPos.y + y >= Grid.GetLength((int)Axis.y)
                ))
            {
                if (!Grid[x, y].properties.SpreadPattern.GoesUp)
                {
                    if (Grid[x + targetPos.x, y + targetPos.y].properties.State == MaterialProperties.MatterState.Liquid ||
                        Grid[x + targetPos.x, y + targetPos.y].properties.State == MaterialProperties.MatterState.Gas)
                    {
                        switch (Grid[x + targetPos.x, y + targetPos.y].properties.State)
                        {
                            //physics interactions
                            case MaterialProperties.MatterState.Granular:
                            case MaterialProperties.MatterState.Liquid:
                            case MaterialProperties.MatterState.Gas:
                                if (Grid[x + targetPos.x, y + targetPos.y].properties.Density < Grid[x, y].properties.Density)
                                {
                                    FlipPixels(origin, origin + targetPos);
                                    return true;
                                }
                                break;
                            case MaterialProperties.MatterState.Solid:
                                if (!(
                                    Grid[x + 1, y].properties == Grid[x,y].properties ||
                                    Grid[x -1, y].properties == Grid[x, y].properties ||
                                    Grid[x, y + 1].properties == Grid[x, y].properties ||
                                    Grid[x, y - 1].properties == Grid[x, y].properties))
                                {
                                    if (Grid[x + targetPos.x, y + targetPos.y].properties.Density < Grid[x, y].properties.Density)
                                    {
                                        FlipPixels(origin, origin + targetPos);
                                        return true;
                                    }
                                }
                                    break;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void CalculatePixelChemistry(int x, int y)
    {

    }

    private void SetPixelData(GameObject pixel,int x, int y)
    {
        pixel.GetComponent<MaterialInstance>().BaseColor = Grid[x, y].color;
        pixel.GetComponent<PixelDataHolder>().data = Grid[x, y];
    }
    #endregion

    #region Generation code
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

        gridSize.x = (int)(Mathf.Floor(width / (GetPixelSize(pixelPrefab))));
        gridSize.y = (int)(Mathf.Floor(height / (GetPixelSize(pixelPrefab))));
        Debug.Log($"grid is of size x: {gridSize.x} by y: {gridSize.y} for a total of {gridSize.x * gridSize.y} cells.");
        Grid = new PixelData[gridSize.x,gridSize.y];
        #endregion

        #region generation loop
        for (int y = 0; y < Grid.GetLength((int)Axis.y); y++)
        {
            for (int x = 0; x < Grid.GetLength((int)Axis.x); x++)
            {
                Grid[x, y] = GenerateData(x,y);
                GameObject pixel = Instantiate(pixelPrefab,
                    new Vector3(
                        x * GetPixelSize(pixelPrefab) + (GetPixelSize(pixelPrefab) / 2) + start.x,
                        -y * GetPixelSize(pixelPrefab) - (GetPixelSize(pixelPrefab) / 2) + start.y,
                        this.transform.position.z),
                    Quaternion.identity, this.gameObject.transform);
                pixel.transform.localScale = new Vector3(pixelSize / resolution, pixelSize / resolution, pixelSize / resolution);
                
                SetPixelData(pixel,x, y);
                Pixels.Add(pixel);
            }
        }

        #endregion
    }

    public PixelData GenerateData(int x, int y)
    {
        PixelData pxData = new PixelData();
        #region material selection
        if (x == 0 || y == 0 || x == gridSize.x - 1 || y == gridSize.y - 1)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.BoundaryRock);
        }
        else if (y > groundLevel)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Water);
        }
        else if (y == gridSize.y - groundLevel)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Sand);
        }
        else if (
            y >= gridSize.y / 2 - 10 && 
            y <= gridSize.y / 2 + 10 && 
            x >= gridSize.x / 2 - 10 && 
            x <= gridSize.x / 2 + 10)
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Sand);
        }
        else
        {
            pxData.properties = library.GetProperty(MaterialLibrary.MaterialNames.Air);
        }
        #endregion
        pxData.position = new Vector2Int(x, y);
        pxData.color = pxData.properties.GetColor(new Vector2Int(x,y));
        return pxData;
    }

    #endregion

    #region Buttons

    [Button]
    public void ClearGrid()
    {
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

    #endregion
}

[Serializable]
public class PixelData
{
    public MaterialProperties properties;
    public Vector2Int position;
    public Color color;

    [Range(0,1)]
    public float Hydration;

    [Range(0, 1)]
    public float ReactionProgress;
}
