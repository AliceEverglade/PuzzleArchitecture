using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectionPoints;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private ConnectionSO connectionSO;

    [SerializeField] public Material selectMaterial;
    [SerializeField] public Material deselectMaterial;
    
    [SerializeField] private GameObject MainPiece;
    [SerializeField] private Selected selected;

    // Start is called before the first frame update
    void Start()
    {
        MainPiece = transform.parent.gameObject;
        selected = MainPiece.GetComponent<Selected>();
        connectionManager = FindObjectOfType<ConnectionManager>();

        foreach (Transform child in MainPiece.transform)
        {
            if (child.gameObject.CompareTag("ConnectionPoint"))
            {
                connectionPoints.Add(child.gameObject);
            }
        }

        deselectMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        SetMaterial();
        CheckMouseClick();
        Disconnect();
    }

    private void SetMaterial()
    {
        if (selected.isSelected)
        {
            gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = deselectMaterial;
        }
    }

    private void CheckMouseClick()
    {
        Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    Debug.Log(hit);
                    selected.isSelected = true;

                    connectionManager.CheckedConnectors.Clear();

                    for (int i = 0; i < connectionPoints.Count; i++)
                    {
                        connectionManager.ChangeMainPiece(gameObject.transform.parent.Find(connectionPoints[i].name).gameObject);
                    }
                }
                else { selected.isSelected = false; }
            }

            else { selected.isSelected = false; }
        }
    }

    private void Disconnect()
    {
        if (Input.GetKeyDown(KeyCode.G) && selected)
        {
            foreach (GameObject connector in connectionPoints)
            {
                GameObject secondConnector = connector.GetComponent<PieceConnect>().connectedConnector;
                
                connectionSO.Disconnect(connector, secondConnector);

                if (connector.GetComponent<PieceConnect>().connectedConnector != null)
                {
                    secondConnector.GetComponent<PieceConnect>().connectedConnector = null;
                    connector.GetComponent<PieceConnect>().connectedConnector = null;
                }
            }
        }
    }
}
