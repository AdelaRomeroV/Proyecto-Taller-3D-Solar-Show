using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    ControlDeVida life;
    Turbo turbo;
    Animator anim;
    float direccion;

    private void Start()
    {
        life = GetComponent<ControlDeVida>();
        turbo = GetComponent<Turbo>();
        anim = GetComponentInChildren<Animator>();
        direccion = 0.5f;
    }

    private void Update()
    {
        Drive();
        SideKick();
        
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

    void SideKick()
    {
        anim.SetBool("RightAtk", turbo.RightAtack);
        anim.SetBool("LeftAtk", turbo.LeftAtack);
    }

    void GetDamage()
    {
        if (life.GetDamage)
        {
            anim.Play("GetDamage");
        }
    }
}
