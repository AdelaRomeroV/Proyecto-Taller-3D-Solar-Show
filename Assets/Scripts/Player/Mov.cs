using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    [Header("Scripts")]
    Turbo turbo;
    
    [Header("Movimiento")]
    public float aceleracion = 5f;
    public float velocidadMaxima = 10f;
    public float fuerzaFreno = 5f;
    public bool onStun = false;

    [Header("Giro")]
    public float velocidadGiro = 2f;
    public float umbralDrift = 0.5f;

    [Header("Drift")]
    public float fuerzaBoostDrift = 10f;
    public float tiempoBoostNecesario = 2f;
    public float aumentoVelocidadBoost = 5f;

    [SerializeField] public Rigidbody rb;
    public float velocidadActual = 0f;
    public bool estaGirando = false;
    public bool boostActivado = false;
    public float tiempoBoostPresionado = 0f;
    public bool boostOn;
    [SerializeField] Renderer cocheRenderer;

    [Header("Audio")]
    public AudioClip audioAcelerador;
    public AudioSource audioSource;

    [Header("Queues")]
    Queue<KeyCode> RightDrift_Buffer = new Queue<KeyCode>();
    Queue<KeyCode> LeftDrift_Buffer = new Queue<KeyCode>();
    public bool RightDrifting = false;
    public bool LeftDrifting = false;
    public bool Drifiting = false;

    void Start()
    {
        turbo = GetComponent<Turbo>();
    }

    private void Update()
    {
         if(velocidadActual < 0.5f && !Input.GetKey(KeyCode.S)) { velocidadActual = 0; }

         InputsBuffer();
    }

    void FixedUpdate()
    {
        if (!onStun)
        {
            GestionarAceleracion();
            GestionarGiro();
            GestionarBoost();
            AplicarVelocidad();
            GestionarTurbo();
        }
    }

    public void OffStun()
    {
        onStun = false;
    }

    void GestionarAceleracion()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!turbo.TurboActive)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = audioAcelerador;
                    audioSource.Play();
                }
            }
            

            velocidadActual += aceleracion * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            velocidadActual -= fuerzaFreno * Time.deltaTime;
        }
        else
        {
            if (audioSource.isPlaying && audioSource.clip == audioAcelerador)
            {
                audioSource.Stop();
            }

            if (velocidadActual > 0)
            {
                velocidadActual -= fuerzaFreno * Time.deltaTime * 0.75f;
            }
            else if (velocidadActual < 0)
            {
                velocidadActual += fuerzaFreno * Time.deltaTime * 0.75f;
            }
        }
        velocidadActual = Mathf.Clamp(velocidadActual, -velocidadMaxima, velocidadMaxima);
        audioSource.volume = Mathf.Abs(velocidadActual) / velocidadMaxima; //dependera el sonido de la velocidad actual que tenga, asi iniciara de menor a mayor sonido
    }

    void GestionarGiro()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float modificadorGiroDrift = estaGirando ? 1.5f : 1f;
        float anguloGiro = horizontalInput * velocidadGiro * modificadorGiroDrift;

        if (estaGirando && !turbo.TurboActive)
        {
            if (Drifiting)
            {
                if (LeftDrifting)
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        anguloGiro *= 0.2f;
                    }
                    else if (horizontalInput < 0 && velocidadActual > 0)
                    {
                        anguloGiro *= 1.5f;
                    }
                }
                else if (RightDrifting)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        anguloGiro *= 0.2f;
                    }
                    else if (horizontalInput < 0 && velocidadActual > 0)
                    {
                        anguloGiro *= 1.5f;
                    }
                }
            }
            else
            {
                anguloGiro *= 1.1f;
            }
        }

        transform.Rotate(Vector3.up, anguloGiro);

        if (Mathf.Abs(horizontalInput) > umbralDrift && velocidadActual > 0)
        {
            estaGirando = true;
        }
        else
        {
            estaGirando = false;
        }
    }

    void GestionarBoost()
    {
        if (estaGirando)
        {
            tiempoBoostPresionado += Time.deltaTime;
            if (tiempoBoostPresionado >= tiempoBoostNecesario)
            {
                turbo.Charging = true;
                boostActivado = true;
            }
        }
        else
        {
            turbo.Charging = false;
            if (boostActivado && !boostOn)
            {
                velocidadMaxima = 75;
                velocidadActual += fuerzaBoostDrift * Time.deltaTime;
                tiempoBoostPresionado = 0f;
                Invoke("ReiniciarVelocidad", 0.5f);
            }
            else
            {
                if (tiempoBoostPresionado > 0)
                {
                    tiempoBoostPresionado = 0f;
                }
            }
        }
    }

    void GestionarTurbo()
    {
        if (turbo.TurboActive && !estaGirando && !boostOn)
        {
            velocidadMaxima = 150;
            velocidadActual += fuerzaBoostDrift * Time.deltaTime;
            tiempoBoostPresionado = 0f;
            Invoke("ReiniciarVelocidad", 0.5f);
        }
    }

    void AplicarVelocidad()
    {
        
        Vector3 velocidad = transform.forward * velocidadActual;
        rb.velocity = new Vector3(velocidad.x, rb.velocity.y, velocidad.z);

        if (!boostActivado)
        {
            ReiniciarVelocidad();
        }
    }

    public void ReiniciarVelocidad()
    {
        boostActivado = false;
        boostOn = false;
        StartCoroutine(ReducirVelocidadMaxima());
    }

    IEnumerator ReducirVelocidadMaxima()
    {
        while (velocidadMaxima > 50f)
        {
            if (velocidadMaxima <= 120)
            {
                float reduccionVelocidad = aumentoVelocidadBoost * Time.deltaTime * 0.1f;
                velocidadMaxima -= reduccionVelocidad;
            }
            else 
            { 
                float reduccionVelocidad = aumentoVelocidadBoost * Time.deltaTime * 0.5f;
                velocidadMaxima -= reduccionVelocidad;
            }                   
            if (velocidadMaxima <= 50) { velocidadMaxima = 50f; }
            yield return null;
        }

    }

    public void Boost()
    {
        velocidadMaxima = 100;
        velocidadActual += fuerzaBoostDrift * Time.deltaTime;
        tiempoBoostPresionado = 0f;
        boostOn = true;
    }
    

    void InputsBuffer()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightDrift_Buffer.Enqueue(KeyCode.D);
            Invoke("RightDequeue", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftDrift_Buffer.Enqueue(KeyCode.A);
            Invoke("LeftDequeue", 0.5f);
        }


        if (RightDrift_Buffer.Count >= 2 && !LeftDrifting)
        {
            RightDrifting = true;
            Drifiting = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            RightDrifting = false;
            Drifiting = false;
        }


        if (LeftDrift_Buffer.Count >= 2 && !RightDrifting)
        {
            LeftDrifting = true;
            Drifiting = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            LeftDrifting = false;
            Drifiting = false;
        }
    }

    void LeftDequeue()
    {
        LeftDrift_Buffer.Dequeue();
    }

    void RightDequeue()
    {
        RightDrift_Buffer.Dequeue();
    }

}
