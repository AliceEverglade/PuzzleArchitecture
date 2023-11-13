using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private PieceData pieceData;
    public bool Selected = false;

    // Start is called before the first frame update
    void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();
        pieceData = GetComponent<PieceData>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMaterial();
        CheckMouseClick();
        Disconnect();
    }

    private void SetMaterial()
    {
        if (Selected)
        {
            //gameObject.GetComponent<MeshRenderer>().material = selectMaterial;
        }
        else
        {
            //gameObject.GetComponent<MeshRenderer>().material = deselectMaterial;
        }
    }

    private void CheckMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject);

                if (pieceData.SubElements.Contains(hit.transform.gameObject))
                {
                    Selected = true;
                }
                else { Selected = false; }
            }

            else { Selected = false; }
        }
    }

    private void Disconnect()
    {
        if (Input.GetKeyDown(KeyCode.G) && Selected)
        {

            foreach (GameObject connector in pieceData.Connectors)
            {
                Debug.Log("lol");
                
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
