using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectionPoints;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private ConnectionSO connectionSO;

    public bool selected = false;
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material deselectMaterial;

    // Start is called before the first frame update
    void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("ConnectionPoint"))
            {
                connectionPoints.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        //SetMaterial();
        CheckMouseClick();
        Disconnect();
    }

    private void SetMaterial()
    {
        if (selected)
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
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, transform.forward, Camera.main.farClipPlane);

        if (Input.GetMouseButtonDown(0))
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    Debug.Log("hit");
                    selected = true;

                    connectionManager.CheckedConnectors.Clear();

                    for (int i = 0; i < connectionPoints.Count; i++)
                    {
                        connectionManager.ChangeMainPiece(gameObject.transform.GetChild(i).gameObject);
                    }
                }
                else { selected = false; }
            }
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
