using System.Collections;
using UnityEngine;

public class Generador_Nivel2 : GeneradorDePista
{
    Controlador_Nivel2 controlador;

    bool CanGoStraight;

    private void Awake()
    {
        controlador = GameObject.Find("Controlador").GetComponent<Controlador_Nivel2>();
        //Pistas = controlador.Pistas;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { Jugador = playerObject.GetComponent<Transform>(); }



        StartCoroutine(StartDelay());
    }

    public override void GenerarPista(GameObject Pista)
    {
        Instantiate(Pista, transform.position, transform.rotation);
        controlador.PistasGeneradas++;
    }

    void MainSpawner()
    {
        if (controlador.PistasGeneradas < controlador.MaxPistas)
        {

            switch (controlador.ActualDir)
            {
                case -1:
                    {
                        SpawnerIsLeft();
                        break;
                    }
                case 0:
                    {
                        SpawnerIsZero();
                        break;
                    }
                case 1:
                    {
                        SpawnerIsRight();
                        break;
                    }
            }
        }
        else if (controlador.PistasGeneradas == controlador.MaxPistas)
        {
            GenerarPista(controlador.MetaFinal);
        }

    }

    void SpawnerIsZero()
    {
        if (controlador.pieceIndex == 1) // izquierda
        {
            Spawn_DiagonalPiece(controlador.CurrentPiece, -1);
        }
        else if (controlador.pieceIndex == 2) // derecha
        {
            Spawn_DiagonalPiece(controlador.CurrentPiece, 1);
        }
        else if (CanGoStraight)
        {
            Spawn_StraighPiece(controlador.CurrentPiece);
        }
        else
        {
            ReScript();
        }
    }

    void SpawnerIsLeft()
    {
        if (controlador.pieceIndex == 2) //Derecha
        {
            Spawn_DiagonalPiece(controlador.CurrentPiece, 1);
        }
        else if (CanGoStraight && controlador.pieceIndex == 0) //Puede ir recto y es una pieza recta
        {
            Spawn_StraighPiece(controlador.CurrentPiece);
        }
        else if(controlador.pieceIndex == 1)
        {
            ReScript();
        }
    }

    void SpawnerIsRight()
    {
        if (controlador.pieceIndex == 1) //Izquierda
        {
            Spawn_DiagonalPiece(controlador.CurrentPiece, -1);
        }
        else if (CanGoStraight && controlador.pieceIndex == 0) //Puede ir recto y es una pieza recta
        {
            Spawn_StraighPiece(controlador.CurrentPiece);
        }
        else if (controlador.pieceIndex == 2 )
        {
            ReScript();
        }
    }

    void ReScript()
    {
        gameObject.AddComponent<Generador_Nivel2>();
        Destroy(this);
    }
    void Spawn_StraighPiece(GameObject obj)
    {
        GenerarPista(obj);
        controlador.Consecutive_Straight_Pieces++;
        Destroy(this);
    }

    void Spawn_DiagonalPiece(GameObject obj, int valor)
    {
        controlador.Consecutive_Straight_Pieces = 0;
        controlador.ActualDir += valor;
        GenerarPista(obj);
        Destroy(this);
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

        MainSpawner();
    }

}
