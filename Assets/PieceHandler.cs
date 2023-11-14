using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{
    [SerializeField] private ConnectionManager connectionManager;
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
                if (pieceData.SubElements.Contains(hit.transform.gameObject))
                {
                    connectionManager.CheckedConnectors.Clear();
                    connectionManager.PiecesToCheck.Clear();

                    connectionManager.ChangeMainPiece(this.gameObject);

                    Selected = true;
                }
                else { Selected = false; }
            }

            else { Selected = false; }
        }
    }
}
