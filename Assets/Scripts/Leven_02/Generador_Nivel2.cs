using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Nivel2 : GeneradorDePista
{
    [SerializeField] GameObject LineaDeMeta;

    List<GameObject> Pistas = new List<GameObject>();
    Controlador_Nivel2 controlador;

    bool CanGoStraight;

    private void Start()
    {
        controlador = GameObject.Find("Controlador").GetComponent<Controlador_Nivel2>();
        Pistas = controlador.Pistas;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { Jugador = playerObject.GetComponent<Transform>(); }



        StartCoroutine(StartDelay());
    }

    public override void GenerarPista(GameObject Pista)
    {
        Instantiate(Pista, transform.position, transform.rotation);
        controlador.PistasGeneradas++;
    }

    void Spawner()
    {
        if (controlador.PistasGeneradas < controlador.MaxPistas)
        {

            switch (controlador.ActualDir)
            {
                case -1:
                    {
                        int random = Random.Range(0, Pistas.Count);

                        
                        if (random == 2 || random == 3) //Diagonal derecha
                        {
                            Spawn_DiagonalPiece(Pistas[random], 1);
                        }
                        else if (CanGoStraight)
                        {
                            if (random != 4 || random != 5) Spawn_StraighPiece(Pistas[random]);
                            else Spawner();

                        }
                        else
                        {
                            Spawner();
                        }

                        break;
                    }
                case 0:
                    {
                        int random = Random.Range(0, Pistas.Count);

                        if (random == 2 || random == 3) //Diagonal derecha
                        {
                            Spawn_DiagonalPiece(Pistas[random], 1);
                        }
                        else if (random == 4 || random == 5) //Diagonal izquierda
                        {
                            Spawn_DiagonalPiece(Pistas[random], -1);
                        }
                        else if(CanGoStraight)
                        {
                            Spawn_StraighPiece(Pistas[random]);
                        }
                        else
                        {
                            Spawner();
                        }

                        break;
                    }
                case 1:
                    {
                        int random = Random.Range(0, Pistas.Count);

                        if (random == 4 || random == 5) //Diagonal izquierda
                        {
                            Spawn_DiagonalPiece(Pistas[random], -1);
                        }
                        else if (CanGoStraight)
                        {
                            if (random != 2 || random != 3) Spawn_StraighPiece(Pistas[random]);
                            else Spawner();
                            
                        }
                        else
                        {
                            Spawner();

                        }
                        break;
                    }
            }
        }
        else if (controlador.PistasGeneradas == controlador.MaxPistas)
        {
            GenerarPista(LineaDeMeta);
        }

    }

    void Spawn_StraighPiece(GameObject obj)
    {
        GenerarPista(obj);
        controlador.Consecutive_Straight_Pieces++;
    }

    void Spawn_DiagonalPiece(GameObject obj, int valor)
    {
        controlador.Consecutive_Straight_Pieces = 0;
        controlador.ActualDir += valor;
        GenerarPista(obj);
    }

    IEnumerator StartDelay()
    {
        if (controlador.Consecutive_Straight_Pieces < 3)
        {
            CanGoStraight = true;
        }
        else
        {
            CanGoStraight = false;
        }

        yield return new WaitForSecondsRealtime(0.05f);

        Spawner();

        Destroy(this);
    }


}
