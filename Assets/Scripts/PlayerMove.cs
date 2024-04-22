using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [NonSerialized] public Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    public float velocidadeMapa, vel;

    public bool podeMover = true;
    public bool podeClicar = false;

    public string input()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            return "Cima";
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            return "Baixo";
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            return "Frente";
        }

        return null;
    }


/*
    public delegate void PertoDeObstaculo(int d);
    public static event PertoDeObstaculo obstaculos;
*/

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (podeMover)
        {
            rb.AddForce(velocidadeMapa * Vector3.right, ForceMode.Force);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && podeMover)
        {
            if (podeClicar)
            {
                rb.AddForce(vel * Vector3.right, ForceMode.Impulse);
            }
        }
        if (rb.velocity.x > 2f)
        {
            rb.velocity = Vector3.right * 2f;
        }
    }

    public void Animacao(int d)
    {
        switch (d)
        {
            case 1:
                anim.Play("Cima");
                break;
            
            case -1:
                anim.Play("Baixo");
                break;
        }
    }

    public void TempoEMove(bool b)
    {
        podeMover = b;
    }

}
