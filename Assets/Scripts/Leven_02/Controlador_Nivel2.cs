using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PiecesList
{
    public List<GameObject> ListPieces = new List<GameObject>();
}

public class Controlador_Nivel2 : MonoBehaviour
{
    [Header("Variables")]
    public int MaxPistas = 10;
    public int PistasGeneradas = 0;
    public int pieceIndex;
    public GameObject CurrentPiece;

    [Header("Pistas")]
    public GameObject MetaFinal;
    [SerializeField] List<PiecesList> Pieces = new List<PiecesList>();

    [Header("Hazards")]
    public List<GameObject> Hazards = new List<GameObject>();

    [NonSerialized] public int ActualDir = 0;
    [NonSerialized] public int Consecutive_Straight_Pieces;

    //0: Centro
    //1: Izq
    //2: Der

    private void Update()
    {
        pieceIndex = UnityEngine.Random.Range(0, Pieces.Count);

        int index = UnityEngine.Random.Range(0, Pieces[pieceIndex].ListPieces.Count);

        CurrentPiece = Pieces[pieceIndex].ListPieces[index];
        
    }
}
