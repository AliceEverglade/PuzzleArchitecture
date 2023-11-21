using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling interactions with the piece, such as selection and setting a piece as a connection's main piece.
/// </summary>

public class PieceHandler : MonoBehaviour
{
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private PieceData pieceData;
    [SerializeField] private GameObject audioObject;

    [SerializeField] private AudioClip selectSFX;
    [SerializeField] private AudioClip deselectSFX;

    public bool Selected = false;
    public bool CanConnect = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GetComponent<BoxCollider>()); // Destroy boxcollider during runtime to avoid interference during piecemovement.

        connectionManager = FindObjectOfType<ConnectionManager>();
        pieceData = GetComponent<PieceData>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseClick(); // Check if mouse is being clicked
    }

    private void CheckMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (pieceData.SubElements.Contains(hit.transform.gameObject)) //If clicked-on element is in the list of this piece's SubElements
                {
                    // Clear check-lists
                    connectionManager.CheckedConnectors.Clear();
                    connectionManager.PiecesToCheck.Clear();

                    //Set this piece as the main piece
                    connectionManager.ChangeMainPiece(gameObject);
                    
                    if (!Selected) //If not previously already selected:
                    {
                        SFXHandler.Instance().PlaySound(audioObject, selectSFX);
                    }

                    Selected = true;

                }
                else { Selected = false; }
            }

            else { 
                if (Selected) //If previously selected:
                {
                    SFXHandler.Instance().PlaySound(audioObject, deselectSFX);
                }

                Selected = false; 
            }
        }
    }
}
