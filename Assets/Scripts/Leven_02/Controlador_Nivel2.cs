using System;
using System.Collections.Generic;
using UnityEngine;


public class Controlador_Nivel2 : MonoBehaviour
{
    public int MaxPistas = 10;
    public int PistasGeneradas = 0;

    public List<GameObject> Pistas = new List<GameObject>();
    public List<GameObject> Hazards = new List<GameObject>();

    [NonSerialized] public int ActualDir = 0;
    [NonSerialized] public int Consecutive_Straight_Pieces;
}
