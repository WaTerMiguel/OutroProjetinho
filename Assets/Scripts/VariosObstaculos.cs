using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariosObstaculos : MonoBehaviour
{
    private PlayerMove player;
    private Rigidbody rb;
    [SerializeField] private float distanciaParaCadaObstaculo;
    private Animator anim;
    [SerializeField] private int quantosObstaculos;
    [SerializeField] private bool seraAleatorio = true;
    [SerializeField] private string[] qualKeys;
    [SerializeField] private GameObject[] tiposDeObstaculos;

    private GameObject[] obstaculosSpawnados;
    private int qualObstaculoAgora = 0;

    [SerializeField] private float speed = -15f;


    private void Awake() 
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMove>();
        }
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void Resetar()
    {
        qualObstaculoAgora = 0;
        if (seraAleatorio)
        {
            quantosObstaculos = Random.Range(2,6);
            obstaculosSpawnados = new GameObject[quantosObstaculos];
            qualKeys = new string[quantosObstaculos];
        
        for(int i = 0; i < quantosObstaculos; i++)
        {
            switch (Random.Range(0,2))
            {
                case 0:
                    qualKeys[i] = "Cima";

                    if (i == 0)
                    {
                        obstaculosSpawnados[0] = Instantiate(tiposDeObstaculos[0], transform.position, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        obstaculosSpawnados[i] = Instantiate(tiposDeObstaculos[0], obstaculosSpawnados[i -1].transform.position - Vector3.right * distanciaParaCadaObstaculo, Quaternion.identity, this.transform);
                    }
                    break;
                
                case 1:
                    qualKeys[i] = "Baixo";

                    if (i == 0)
                    {
                        obstaculosSpawnados[0] = Instantiate(tiposDeObstaculos[1], transform.position, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        obstaculosSpawnados[i] = Instantiate(tiposDeObstaculos[1], obstaculosSpawnados[i -1].transform.position - Vector3.right * distanciaParaCadaObstaculo, Quaternion.identity, this.transform);
                    }
                    break;
            }
        }
        }
        else
        {
            obstaculosSpawnados = new GameObject[qualKeys.Length];
            quantosObstaculos = qualKeys.Length;
            for (int i = 0; i < qualKeys.Length ; i++)
            {
                switch(qualKeys[i])
                {
                    case "Cima":

                    if (i == 0)
                    {
                        obstaculosSpawnados[0] = Instantiate(tiposDeObstaculos[0], transform.position, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        obstaculosSpawnados[i] = Instantiate(tiposDeObstaculos[0], obstaculosSpawnados[i -1].transform.position - Vector3.right * distanciaParaCadaObstaculo, Quaternion.identity, this.transform);
                    }
                    break;
                
                case "Baixo":

                    if (i == 0)
                    {
                        obstaculosSpawnados[0] = Instantiate(tiposDeObstaculos[1], transform.position, Quaternion.identity, this.transform);
                    }
                    else
                    {
                        obstaculosSpawnados[i] = Instantiate(tiposDeObstaculos[1], obstaculosSpawnados[i -1].transform.position - Vector3.right * distanciaParaCadaObstaculo, Quaternion.identity, this.transform);
                    }
                    break;
                }
            }
        }
        rb.velocity = Vector3.right * speed * -1;
    }

    public void Deletar()
    {
        foreach(GameObject obs in obstaculosSpawnados)
        {
            Destroy(obs);
        }

        StartCoroutine(Recomecar(Random.Range(0f,6f)));
    }

    IEnumerator Recomecar(float tempo)
    {
        yield return new WaitForSecondsRealtime(tempo);
        anim.Play("Obstaculos");
    }

    private void Update()
    {
        float d = Vector3.Distance(obstaculosSpawnados[qualObstaculoAgora].transform.position, player.gameObject.transform.position);
        if (d < 5f)
        {
            if ((d - 2.5f) / 2.5f < 0.1f)
            {
                //Time.timeScale = 0.1f;
                player.rb.velocity = Vector3.zero;
                Time.timeScale = Mathf.Lerp(1, 0.1f, 3);
            }
            else
            {
                player.rb.velocity = Vector3.zero;
                Time.timeScale = Mathf.Lerp(1, (d - 2.5f) / 2.5f, 3);
            }

            if (qualKeys[qualObstaculoAgora] == player.input())
            {
                switch (qualKeys[qualObstaculoAgora])
                {
                    case "Cima":
                        player.Animacao(1);
                        break;

                    case "Baixo":
                        player.Animacao(-1);
                        break;
                }
            }


            if (obstaculosSpawnados[qualObstaculoAgora].transform.position.x < player.transform.position.x)
            {
                if(qualObstaculoAgora < obstaculosSpawnados.Length - 1)
                {
                    qualObstaculoAgora++;
                }
            }

            /*
            if(player.velocidadeMapa < -15f)
            {
                player.velocidadeMapa = -15f;
            }
            */


        }
    }

}
