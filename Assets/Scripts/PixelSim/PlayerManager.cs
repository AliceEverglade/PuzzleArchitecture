using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using static UnityEditor.PlayerSettings;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SimulationHandler simHandler;

    [SerializeField] private GameObject mouseUI;
    [SerializeField] private Vector3 mouseUIOffset;
    private TMP_Text mouseUIText;

    [SerializeField] private BrushShapes brushShape;
    [SerializeField] private float brushSize;
    public List<GameObject> brushHits;
    private Vector3 mousedObjectPos;

    [Header("UI")]
    [SerializeField] private List<PixelData> presets;
    [SerializeField] private PixelData selectedData;

    public enum BrushShapes
    {
        Circle,
        Square
    }
    // Start is called before the first frame update
    void Start()
    {
        mouseUIText = mouseUI.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
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
                CheckBrushHits(mousedObjectPos);
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
