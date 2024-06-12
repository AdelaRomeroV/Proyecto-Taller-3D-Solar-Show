using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    ControlDeVida life;
    Mov mov;
    Turbo turbo;
    public Animator anim;
    float direccion;

    [Header("Particulas")]
    [SerializeField] GameObject TurboEffect;
    [SerializeField] GameObject Daños;
    [SerializeField] GameObject Humo;
    [SerializeField] GameObject Explosiones;
    [SerializeField] GameObject ExplosionGrande;

    [SerializeField] List<Transform> Posiciones;

    private void Start()
    {
        life = GetComponent<ControlDeVida>();
        mov = GetComponent<Mov>();
        turbo = GetComponent<Turbo>();
        direccion = 0.5f;

    }

    private void Update()
    {
        Drive();
        Freno();
        Turbo();
        SideKick();
        Dead();
        
    }
    private void FixedUpdate()
    {
        GetDamage();
    }

    void Drive()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput == 1 || horizontalInput == -1)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Instantiate(Humo, Posiciones[1].position, Posiciones[1].rotation);

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

    void Freno()
    {
        if (Input.GetKey(KeyCode.S)) 
        {
            if (mov.velocidadActual > mov.velocidadMaxima * 0.6f)
            {
                Instantiate(Humo, Posiciones[1].position, Posiciones[1].rotation);
            }
        }
    }

    void Turbo()
    {
        if (turbo.TurboActive)
        {
            Instantiate(TurboEffect, Posiciones[0].position, Posiciones[0].rotation);
        }
    }

    void SideKick()
    {
        anim.SetBool("RightAtk", turbo.RightAtacking);
        anim.SetBool("LeftAtk", turbo.LeftAttaking);
    }

    void GetDamage()
    {
        if(life != null)
        {
            if (life.GetDamage)
            {
                GameObject obj = Instantiate(Explosiones, transform.position, transform.rotation);
                obj.transform.SetParent(transform);
                anim.Play("GetDamage");
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(life == null)
        {
            if (collision.collider.CompareTag("Pared"))
            {
                GameObject obj = Instantiate(Explosiones, transform.position, transform.rotation);
                anim.Play("GetDamage");
            }
        }
        
    }

    void Dead()
    {
        if(turbo.CurrentEnergy <= 0)
        {
            Instantiate(ExplosionGrande, transform.position, transform.rotation);
        }
    }
}
