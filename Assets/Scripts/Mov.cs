using System;
using System.Collections;
using UnityEngine;

public class Mov : MonoBehaviour
{
    [Header("Scripts")]
    Turbo turbo;
    Animator anim;
    
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

    private Color colorPreparacionBoost = Color.yellow;
    private Color colorListoBoost = Color.red;

    [SerializeField] Rigidbody rb;
    public float velocidadActual = 0f;
    public bool estaDerrapando = false;
    public bool boostActivado = false;
    public float tiempoBoostPresionado = 0f;

    [SerializeField] Renderer cocheRenderer;
    private Color colorOriginal;

    float direccion;

    void Start()
    {
        turbo = GetComponent<Turbo>();
        anim = GetComponentInChildren<Animator>();

        colorOriginal = cocheRenderer.material.color;
        direccion = 0.5f;
        if(velocidadActual < 0.1) { velocidadActual = 0; }
    }

    private void Update()
    {
        Animations();
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
            ColorChanges();
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
            velocidadActual += aceleracion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocidadActual -= fuerzaFreno * Time.deltaTime;
        }
        else
        {
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
    }

    void GestionarGiro()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float modificadorGiroDrift = estaDerrapando ? 1.5f : 1f;
        float anguloGiro = horizontalInput * velocidadGiro * modificadorGiroDrift;

        if (estaDerrapando && !turbo.TurboActivo)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.D))
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
                else if (Input.GetKey(KeyCode.A))
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
            }
            else
            {
                anguloGiro *= 1.1f;
            }
        }

        transform.Rotate(Vector3.up, anguloGiro);

        if (Mathf.Abs(horizontalInput) > umbralDrift && velocidadActual > 0)
        {
            estaDerrapando = true;
        }
        else
        {
            estaDerrapando = false;
        }
    }

    void GestionarBoost()
    {
        if (estaDerrapando && Input.GetKey(KeyCode.LeftShift))
        {
            tiempoBoostPresionado += Time.deltaTime;
            if (tiempoBoostPresionado >= tiempoBoostNecesario)
            {
                turbo.reload = true;
                boostActivado = true;
            }
        }
        else
        {
            turbo.reload = false;
            if (boostActivado)
            {
                velocidadMaxima += aumentoVelocidadBoost;
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
        if (turbo.TurboActivo && !estaDerrapando)
        {
            velocidadMaxima += aumentoVelocidadBoost * 2;
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

    void ReiniciarVelocidad()
    {
        boostActivado = false;
        StartCoroutine(ReducirVelocidadMaxima());
    }

    IEnumerator ReducirVelocidadMaxima()
    {
        while (velocidadMaxima > 50f)
        {
            float reduccionVelocidad = aumentoVelocidadBoost * Time.deltaTime * 0.5f;

            velocidadMaxima -= reduccionVelocidad;

            velocidadMaxima = Mathf.Max(velocidadMaxima, 50f);

            yield return null;
        }

        velocidadMaxima = 50f;
    }

    void ColorChanges()
    {
        if (boostActivado || turbo.TurboActivo)
        {
            cocheRenderer.material.color = colorListoBoost;
        }
        else if (tiempoBoostPresionado > 0)
        {
            cocheRenderer.material.color = colorPreparacionBoost;
        }
        else
        {
            cocheRenderer.material.color = colorOriginal;
        }
    }

    void Animations()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput == 1 || horizontalInput == -1)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (horizontalInput == 1)
                {
                    direccion = Mathf.Lerp(direccion, 1f, Time.deltaTime * 3);
                }
                else if (horizontalInput == -1)
                {
                    direccion = Mathf.Lerp(direccion, 0f, Time.deltaTime * 3);
                }
            }
            else
            {
                if (horizontalInput == 1)
                {
                    direccion = Mathf.Lerp(direccion, 0.7f, Time.deltaTime * 3);
                }
                else if (horizontalInput == -1)
                {
                    direccion = Mathf.Lerp(direccion, 0.3f, Time.deltaTime * 3);
                }
            }

        }
        else
        {
            direccion = Mathf.Lerp(direccion, 0.5f, Time.deltaTime * 5);
        }

        anim.SetFloat("Direction", direccion);
    }
}
