using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenario : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] public float distance;
    private Vector3 startPosition;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        rb.velocity = Vector3.right * speed * -1;
    }

    private void Update() 
    {
        if (Vector3.Distance(startPosition, transform.position) > distance)
        {
            transform.position = startPosition;
        }
    }
}
