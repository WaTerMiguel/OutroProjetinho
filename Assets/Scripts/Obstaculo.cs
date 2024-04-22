using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public static PlayerMove player;

    [SerializeField] string qualKey;
    [SerializeField] int qualLado;

    private bool jaChegou = false;
    
    private void Awake() 
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMove>();
        }
    }

    void Start()
    {
        switch (qualLado)
        {
            case 1:
                qualKey = "Cima";
                break;

            case -1:
                qualKey = "Baixo";
                break;
        }
    }

    private void Update() 
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        if (distancia < 8f)
        {
            if (jaChegou == false)
            {
                /*
                StartCoroutine(TempoDiminuir());
                */
                player.TempoEMove(false);
                if ((distancia - 4f) / 4 > 0.1f)
                {
                    Time.timeScale = (distancia - 4f) / 4;
                }
                else
                {
                    Time.timeScale = 0.1f;
                }
                 if (player.input() == qualKey)
                {
            
                    StopAllCoroutines();
                    StartCoroutine(TempoVoltar());
                    jaChegou = true;
                    player.Animacao(qualLado);
                }

            }
        }

        if (jaChegou)
        {
            if (player.transform.position.x < transform.position.x)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 1)
                {
                    Time.timeScale = Vector3.Distance(transform.position, player.transform.position);
                }
            }
        }

        transform.position -= Vector3.right * 10f * Time.deltaTime;
    }

/*
    IEnumerator TempoDiminuir()
    {
        while(Time.timeScale > 0.1f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.fixedUnscaledDeltaTime);
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime / 50f);
        }
    }
    */

    IEnumerator TempoVoltar()
    {
        while(Time.timeScale < 1f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.fixedUnscaledDeltaTime);
            yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            if (Time.timeScale > 0.9f)
            {
                Time.timeScale = 1f;
            }
        }
    }

}
