using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesControllerTutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> Naves = new List<GameObject>();
    [SerializeField] List<Transform> PlayerPos = new List<Transform>();

    [SerializeField] Turbo turbo;
    public int navesDestruidas = 0;
}
