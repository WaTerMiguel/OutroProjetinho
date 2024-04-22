using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }
    public void PodeClicar()
    {
        player.podeClicar = true;
    }

    public void NaoPodeClicar()
    {
        player.podeClicar = false;
    }
}
