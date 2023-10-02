using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private List<PieceGroup> pieceGroups;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class PieceGroup
{
    public List<GameObject> Pieces;
    
    public void Absorb(PieceGroup otherPieceGroup)
    {
        Pieces.AddRange(otherPieceGroup.Pieces);
    }
}