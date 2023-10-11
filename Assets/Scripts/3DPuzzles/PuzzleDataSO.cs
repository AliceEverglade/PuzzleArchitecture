using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/PuzzleDataSO")]

public class PuzzleDataSO : ScriptableObject
{
    public List<Piece> Pieces = new List<Piece>();
}

[Serializable]
public struct Piece
{
    public int ID;
    public GameObject Reference;

    public Piece(int _id, GameObject _reference)
    {
        ID = _id;
        Reference = _reference;
    }
}