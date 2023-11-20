using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyButtons;
using UnityEngine.UI;

/// <summary>
/// the player controller that handles the brushes, tools and material selection.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SimulationHandler simHandler;
    [SerializeField] private MaterialLibrary library;

    [SerializeField] private GameObject mouseUI;
    [SerializeField] private Vector3 mouseUIOffset;
    private TMP_Text mouseUIText;

    [SerializeField] private Slider brushSizeSlider;

    [SerializeField] private BrushShapes brushShape;
    private float brushSize => (inputBrushSize) * (simHandler.GetPixelSize(simHandler.PixelPrefab) / 2);
    [SerializeField] private float inputBrushSize;
    public List<GameObject> brushHits;
    private Vector3 mousedObjectPos;


    public enum Tool
    {
        Place,
        Compact,
        Agitate
    }
    [Header("UI")]
    [SerializeField] private Tool selectedTool;
    [SerializeField] private MaterialData selectedData;
    [SerializeField] private PixelData eraserData;
    [SerializeField] private List<MaterialData> placeableMaterialList;
    [SerializeField] private GameObject materialUIElement;
    [SerializeField] private GameObject materialUI;
    [SerializeField] private List<GameObject> materialUIList;

    public enum BrushShapes
    {
        Circle,
        Square
    }
    // Start is called before the first frame update
    void Start()
    {
        mouseUIText = mouseUI.GetComponent<TMP_Text>();
        InstantiateUI();
    }

    [Button]
    private void InstantiateUI()
    {
        placeableMaterialList = library.GetPlacableMaterials();
        foreach (MaterialData data in placeableMaterialList)
        {
            GameObject element = Instantiate(materialUIElement, materialUI.transform);
            element.name = data.name + " UI Element";
            element.GetComponent<MaterialUI>().SetUIElements(this,data, data.Value.color, data.Value.properties.MaterialName);
            materialUIList.Add(element);
        }

        SetSelectedData(placeableMaterialList[0]);
    }

    [Button]
    private void RemoveUI()
    {
        foreach(GameObject element in materialUIList)
        {
            DestroyImmediate(element);
        }
        materialUIList.Clear();
    }

    public void SetSelectedData(MaterialData data)
    {
        selectedData = data;
        foreach(GameObject element in materialUIList)
        {
            element.GetComponent<MaterialUI>().CheckIfSelected(selectedData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Selection();
        inputBrushSize = brushSizeSlider.value;
        //selected tool
        if (Input.GetMouseButton(0))
        {
            foreach (GameObject pixel in brushHits)
            {
                UseToolOnPixel(pixel);
            }
        }
        //eraser tool
        if (Input.GetMouseButton(1))
        {
            foreach(GameObject pixel in brushHits)
            {
                if (pixel.GetComponent<PixelDataHolder>().data.properties.State != MaterialProperties.MatterState.Meta)
                {
                    //set pixeldata and grid stuff
                    SetPixel(pixel, eraserData);
                }
            }
        }
    }

    void UseToolOnPixel(GameObject pixel)
    {
        if (pixel.GetComponent<PixelDataHolder>().data.properties.State != MaterialProperties.MatterState.Meta)
        {
            switch (selectedTool)
            {
                case Tool.Place:
                    SetPixel(pixel, selectedData.Value);
                    break;
                case Tool.Compact:
                    Compact(pixel);
                    break;
                case Tool.Agitate:
                    Agitate(pixel);
                    break;
            }
        }
    }

    //set pixel to specified data
    void SetPixel(GameObject pixel, PixelData reference)
    {
        PixelData data = new PixelData(reference);
        
        Vector2Int pos = pixel.GetComponent<PixelDataHolder>().data.position;
        simHandler.Grid[pos.x, pos.y] = data;
        simHandler.Grid[pos.x, pos.y].position = pos;
        simHandler.Grid[pos.x, pos.y].color = simHandler.Grid[pos.x, pos.y].properties.GetColor(pos);
        simHandler.SetPixelData(pixel, pos.x, pos.y);
    }

    void Compact(GameObject pixel)
    {
        //check if pixel can be compressed and compress by setting the spread pattern to something more solid
    }

    void Agitate(GameObject pixel)
    {
        //check if pixel can be agitated and liquify by setting the spread pattern to something more liquid
    }

    void Selection()
    {
        mouseUI.transform.position = Input.mousePosition + mouseUIOffset;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Pixel"))
            {
                PixelData data = hit.transform.gameObject.GetComponent<PixelDataHolder>().data;
                mousedObjectPos = hit.transform.position;
                mouseUI.SetActive(true);
                mouseUIText.text =
                    $"<b>{data.properties.MaterialName}</b> ({data.position.x},{data.position.y})\n" +
                    $"Hydration: {data.Hydration * 100}% \n";
                mouseUIText.text += data.ReactionProgress > 0 ? $"Reaction Progress: {data.ReactionProgress * 100}%" : $"";
                if (inputBrushSize > 1)
                {
                    CheckBrushHits(mousedObjectPos);
                }
                else
                {
                    brushHits = new List<GameObject>();
                    brushHits.Add(hit.collider.gameObject);
                }
            }
        }
        else
        {
            mouseUI.SetActive(false);
            brushHits = new List<GameObject>();
        }
        foreach (GameObject pixel in simHandler.Pixels)
        {
            if (!brushHits.Contains(pixel))
            {
                pixel.GetComponent<MaterialInstance>().Selected = false;
            }
            else
            {
                pixel.GetComponent<MaterialInstance>().Selected = true;
            }
        }
    }

    PixelData GetData(GameObject pixel)
    {
        return pixel.GetComponent<PixelDataHolder>().data;
    }

    void CheckBrushHits(Vector3 pos)
    {
        brushHits = new List<GameObject>();
        Collider[] hits = null;
        switch (brushShape)
        {
            case BrushShapes.Circle:
                hits = Physics.OverlapSphere(pos, brushSize);
                break;
            case BrushShapes.Square:
                hits = Physics.OverlapBox(pos, new Vector3(brushSize, brushSize, brushSize));
                break;
        }
        foreach (Collider hit in hits)
        {
            brushHits.Add(hit.gameObject);
        }
    }

    public void SetTool(int i)
    {
        selectedTool = (Tool)i;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        switch (brushShape)
        {
            case BrushShapes.Circle:
                Gizmos.DrawWireSphere(mousedObjectPos, brushSize);
                break;
            case BrushShapes.Square:
                Gizmos.DrawWireCube(mousedObjectPos, new Vector3(brushSize, brushSize, brushSize));
                break;
        }
    }
}
